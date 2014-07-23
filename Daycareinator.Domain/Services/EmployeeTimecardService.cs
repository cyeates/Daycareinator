using Daycareinator.Data;
using Daycareinator.Data.Entities;
using Daycareinator.Domain.Dtos;
using Daycareinator.Domain.Extensions;
using EfficientlyLazy.Crypto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain.Services
{
    public interface IEmployeeTimecardService
    {
        TimecardDto GetTimecard(int clientId, DateTime weekOf);
        ValidationResult Save(List<Employee> employees, int clientId, DateTime date);
        ValidationResult Submit(DateTime date, int clientId);
    }
    public class EmployeeTimecardService : IEmployeeTimecardService
    {
        private IUnitOfWork _uow;
        private ICryptoEngine _cryptography;
        public EmployeeTimecardService(IUnitOfWork uow, ICryptoEngine cryptography)
        {
            _uow = uow;
            _cryptography = cryptography;
        }

        public TimecardDto GetTimecard(int clientId, DateTime weekOf)
        {
            bool isTimecardClosed = IsTimecardClosed(weekOf, clientId);

            int delta = DayOfWeek.Monday - weekOf.DayOfWeek;
            DateTime monday = weekOf.GetFirstDayOfWeek().Date;
            DateTime sunday = monday.AddDays(6).Date;

            var employees = from e in _uow.Employees.Find(e => e.ClientId == clientId)
                            orderby e.FirstName
                            select new EmployeeDto
                                {
                                    EmployeeId = e.EmployeeId,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    RegularPayRate = e.PayRate,
                                    Last4Ssn = GetLast4Ssn(e.Ssn),
                                    TimecardEntries = from tc in e.TimecardEntries.Where(t => t.Date >= monday && t.Date <= sunday)
                                                      select new TimecardEntryDto
                                                      {
                                                          Date = tc.Date,
                                                          Hours = tc.Hours
                                                      }
                                };

            return new TimecardDto
            {
                Employees = employees,
                IsTimecardClosed = isTimecardClosed
            };
        }

        private string GetLast4Ssn(string encryptedSsn)
        {

            if (!String.IsNullOrEmpty(encryptedSsn))
            {
                var decrypted = _cryptography.Decrypt(encryptedSsn);
                return decrypted.Substring(decrypted.Length - 4);
            }

            return String.Empty;
        }

        private bool IsTimecardClosed(DateTime weekOf, int clientId)
        {
            var date = weekOf.GetFirstDayOfWeek().Date;
            var submission = _uow.TimecardSubmissions.FirstOrDefault(t => t.ClientId == clientId && t.FirstDateOfTimecard == date);
            return submission != null;
        }

        public ValidationResult Save(List<Employee> employees, int clientId, DateTime date)
        {

            if (IsTimecardClosed(date, clientId))
                return ValidationResult.ErrorMessage(string.Format("This time card has already been submitted. If you need to make changes please call {0}.", ConfigurationManager.AppSettings["ContactPhone"]));


            var employeeEntities = _uow.Employees.Find(e => e.ClientId == clientId);
            foreach (var employee in employees)
            {
                var employeeToUpdate = employeeEntities.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
                if (employeeToUpdate != null)
                {
                    foreach (var entry in employee.TimecardEntries)
                    {
                        var entryToUpdate = employeeToUpdate.TimecardEntries.FirstOrDefault(e => e.Date.Date == entry.Date.Date);
                        if (entryToUpdate != null)
                        {
                            entryToUpdate.Hours = entry.Hours;
                        }
                        else
                        {
                            employeeToUpdate.TimecardEntries.Add(new TimecardEntry
                            {
                                Date = entry.Date.Date,
                                Hours = entry.Hours
                            });
                        }
                    }
                }



            }

            try
            {
                _uow.Commit();
                return ValidationResult.SuccessMessage("The time cards were saved successfully.");
            }
            catch
            {
                return ValidationResult.ErrorMessage("An error occured while saving the time cards.");
            }
        }

        public ValidationResult Submit(DateTime date, int clientId)
        {
            
            var client = _uow.Clients.FirstOrDefault(c => c.ClientId == clientId);
            var timecard = GetTimecard(clientId, date);
            string startDate = date.GetFirstDayOfWeek().ToShortDateString();
            string endDate = date.GetFirstDayOfWeek().AddDays(6).ToShortDateString();
            string csvContent = GetCSVContent(timecard, client, date);

            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            {
                writer.Write(csvContent);
                writer.Flush();
                memoryStream.Seek(0, SeekOrigin.Begin);
                var attachment = new Attachment(memoryStream, new ContentType());
                
                attachment.Name = string.Format("{0}_{1}_{2}.csv", client.Name.Replace(" ", "-"), startDate, endDate);

            
            var email = new Daycareinator.Domain.Notifications.Email
            {
                Recipients = new List<string>{ConfigurationManager.AppSettings["ContactFormToAddress"]},
                Subject = String.Format("Time Card submitted for {0} for the week of {1} - {2}", client.Name, startDate, endDate),
                Attachments = new List<Attachment>{attachment},
                CC = new List<String>{"chad.yeates@gmail.com"}
                
            };


            try
            {
                _uow.Commit();
                email.Send();

                _uow.TimecardSubmissions.Add(new TimecardSubmission { FirstDateOfTimecard = date.GetFirstDayOfWeek().Date, ClientId = clientId, DateSubmitted = DateTime.Now });
                _uow.Commit(); //only insert submission record if the actual submission was successful.
                return ValidationResult.SuccessMessage("The time card was submitted successfully.");
            }
            catch
            {
                return ValidationResult.ErrorMessage("An error occured while submitting the time card.");
            }
          }
        }


        private string GetCSVContent(TimecardDto timecard, Client client, DateTime weekOf)
        {
            var sb = new StringBuilder();
            sb.AppendLine("First Name,Last Name,Total Hours,Marital Status,Allowances,Notes");
            var employeeIds = timecard.Employees.Select(e => e.EmployeeId).ToArray();
            var employees = _uow.Employees.Find(e => e.ClientId == client.ClientId && employeeIds.Contains(e.EmployeeId)).ToList();
            foreach (var employeeTimecard in timecard.Employees)
            {
                var employee = employees.FirstOrDefault(e => e.EmployeeId == employeeTimecard.EmployeeId);
                sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", employee.FirstName, employee.LastName, employeeTimecard.TotalHours, employee.MaritalStatus, employee.Allowances, employee.Notes));
            }

            return sb.ToString();
        }
        
    }


}
