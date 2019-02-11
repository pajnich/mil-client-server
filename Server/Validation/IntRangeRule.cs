using System;
using System.Globalization;
using System.Windows.Controls;

namespace Server
{
    public class IntRangeRule : ValidationRule
    {
        private int _min;
        private int _max;

        public IntRangeRule()
        {
        }

        public int Min {
            get { return _min; }
            set { _min = value; }
        }

        public int Max {
            get { return _max; }
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = 0;

            try
            {
                if (((string)value).Length > 0)
                    age = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((age < Min) || (age > Max))
            {
                return new ValidationResult(false, "Please enter a number in the range: " + Min + " - " + Max + ".");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
