using AutoMapper;
using Daycareinator.Domain.Dtos;
using Daycareinator.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daycareinator.Models.Timecards
{
    public class TimecardsModelBuilder
    {
        public TimecardsModel Build(TimecardDto timecard, DateTime weekOf)
        {
           var dates = GetTimecardDates(weekOf);
            return new TimecardsModel 
            { 
                Dates = GetDateModels(dates),
                EmployeeTimecards = MapEmployeeTimecards(timecard.Employees, dates),
                IsTimecardClosed = timecard.IsTimecardClosed
            };
            
        }

        private IEnumerable<DateModel> GetDateModels(IEnumerable<DateTime> dates)
        {
            foreach (var date in dates)
            {
                yield return new DateModel{Date = date};
            }
        }

        private IEnumerable<EmployeeTimecard> MapEmployeeTimecards(IEnumerable<EmployeeDto> timecardDtos, IEnumerable<DateTime> dates)
        {
            var timecards = new List<EmployeeTimecard>();
            foreach (var dto in timecardDtos)
            {
                timecards.Add(new EmployeeTimecard
                {
                    EmployeeId = dto.EmployeeId,
                    Name = string.Format("{0} {1}", dto.FirstName, dto.LastName),
                    Last4Ssn = dto.Last4Ssn,
                    RegularPayRate = dto.RegularPayRate,
                    OtPayRate = dto.OTPayRate,
                    TimecardEntries = GetTimecardEntries(dto.TimecardEntries, dates), //dto.TimecardEntries.OrderBy(t => t.Date).Select(t => t.Hours).ToList(),
                    TotalHours = dto.TotalHours,
                    OtHours = dto.OtHours,
                    RegularHours = dto.RegularHours,
                    GrossPay = dto.GrossPay


                });
            }

            return timecards;
        }

        private IEnumerable<TimecardEntry> GetTimecardEntries(IEnumerable<TimecardEntryDto> dto, IEnumerable<DateTime> dates)
        {
            var entries = new List<TimecardEntry>();//dto.OrderBy(t => t.Date).Select(t => t.Hours).ToList();
            foreach (var date in dates)
            {
                var entry = dto.FirstOrDefault(e => e.Date.Date == date.Date);
                if (entry == null)
                {
                    entries.Add(new TimecardEntry { Date = date.Date, Hours = null });
                }
                else
                {
                    entries.Add(new TimecardEntry { Date = date.Date, Hours = entry.Hours });
                }
            }

            return entries.OrderBy(t => t.Date).ToList();

        }

        private IEnumerable<DateTime> GetTimecardDates(DateTime weekOf)
        {
            DateTime startDate = weekOf.GetFirstDayOfWeek();
            var dates = new List<DateTime> { startDate};
            for (int i = 1; i <= 6; i++)
            {
                dates.Add(startDate.AddDays(i));
            }


            return dates;
        }
    }
}