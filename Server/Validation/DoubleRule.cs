using System;
using System.Globalization;
using System.Windows.Controls;

namespace Server
{
    public class DoubleRule : ValidationRule
    {
        public DoubleRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double enteredNumber = 0;

            try
            {
                if (((string)value).Length > 0)
                    enteredNumber = Double.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            return ValidationResult.ValidResult;
        }
    }
}
