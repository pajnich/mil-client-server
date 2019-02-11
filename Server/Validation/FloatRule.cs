using System;
using System.Globalization;
using System.Windows.Controls;

namespace Server
{
    public class FloatRule : ValidationRule
    {
        public FloatRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            float enteredNumber = 0;

            try
            {
                if (((string)value).Length > 0)
                    enteredNumber = Single.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            return ValidationResult.ValidResult;
        }
    }
}
