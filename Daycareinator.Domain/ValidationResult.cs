using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }

        public ValidationResult()
        {
            IsValid = true;
        }
        public void AddErrorMessage(string message)
        {
            Message = message;
            IsValid = false;
           
        }

        public void AddSuccessMessage(string message)
        {
            Message = message;
            IsValid = true;
            
        }

        public static ValidationResult ErrorMessage(string message)
        {
            var result = new ValidationResult();
            result.AddErrorMessage(message);
            return result;
        }

        public static ValidationResult SuccessMessage(string message)
        {
            var result = new ValidationResult();
            result.AddSuccessMessage(message);
            return result;
        }
    }
}
