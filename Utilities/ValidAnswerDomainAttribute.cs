using System.ComponentModel.DataAnnotations;
using System;

namespace lab3.Utilities
{
    public class ValidAnswerDomainAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (Convert.ToInt32(value) >= -20 && Convert.ToInt32(value) <= 20);
        }
    }
}