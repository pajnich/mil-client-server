using System;
using System.Globalization;
using System.Windows.Controls;

namespace Server
{
    public class IntRangeRule : ValidationRule
    {
        private int _minValue;
        private int _maxValue;

        public IntRangeRule()
        {
        }

        public int MinValue {
            get { return _minValue; }
            set { _minValue = value; }
        }

        public int MaxValue {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int enteredNumber = 0;

            try
            {
                if (((string)value).Length > 0)
                    enteredNumber = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((enteredNumber < MinValue) || (enteredNumber > MaxValue))
            {
                return new ValidationResult(false, "Please enter a number in the range: " + MinValue + " - " + MaxValue + ".");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
