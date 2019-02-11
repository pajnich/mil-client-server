using System;
using System.Globalization;
using System.Windows.Controls;

namespace Server
{
    public class StringRangeRule : ValidationRule
    {
        private int _minLength;
        private int _maxLength;

        public StringRangeRule()
        {
        }

        public int MinLength {
            get { return _minLength; }
            set { _minLength = value; }
        }

        public int MaxLength {
            get { return _maxLength; }
            set { _maxLength = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string enteredString = "";

            try
            {
                if (((string)value).Length > 0)
                    enteredString = value.ToString();
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((enteredString.Length < MinLength) || (enteredString.Length > MaxLength))
            {
                return new ValidationResult(false, "Please enter a string with length in the range: " + MinLength + " - " + MaxLength + ".");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
