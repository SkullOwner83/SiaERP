using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace SiaERP.Resources.Utilities
{
    //Invert the value of a boolean
    class BooleanInverse : IValueConverter
    {
        public object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            //Check if input value is a boolean for return his inverse
            if (Value is bool BoolValue)
            {
                return !BoolValue;
            }

            return Value;
        }

        //There's no conversion back for this method
        public object ConvertBack(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            throw new NotImplementedException();
        }
    }

    //Convert int value to boolean by the value pamarameter
    public class IntToBoolean : IValueConverter
    {
        public object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            //Check if input value if control and the parameter are int value and return true in the view if they match
            if (Value is int SelectedValue && int.TryParse(Parameter.ToString(), out int OptionValue))
            {
                return SelectedValue == OptionValue;
            }
            
            return false;
        }

        public object ConvertBack(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            //Return the parameter in view model when changing value control
            if (Value is bool isChecked && isChecked && int.TryParse(Parameter.ToString(), out int OptionValue))
            {
                return OptionValue;
            }

            return Binding.DoNothing;
        }
    }

    //Keep a squared size ratio of the control based on his height
    public class WidthToHeight : IValueConverter
    {
        public object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            if (Value is double Height)
            {
                return Height;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            throw new NotImplementedException();
        }
    }

    //Phone number string format
    public class PhoneNumberForamt : IValueConverter
    {
        public object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            if (Value is string PhoneNumber)
            {
                //Delete empty spaces to format string
                PhoneNumber = Regex.Replace(PhoneNumber, @"\s", "");

                //Split the phone number string in groups
                if (PhoneNumber.Length > 3)
                {
                    PhoneNumber = PhoneNumber.Insert(3, " ");
                }

                if (PhoneNumber.Length > 7)
                {
                    PhoneNumber = PhoneNumber.Insert(7, " ");
                }

                if (PhoneNumber.Length > 12)
                {
                    PhoneNumber = PhoneNumber.Replace(" ", "");
                }

                return PhoneNumber;
            }

            //Don't do any conversion if the value is not a string
            return Binding.DoNothing;
        }

        public object ConvertBack(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            //Delete empty spaces and values other than a numeric value
            if (Value is string PhoneNumber)
            {
                PhoneNumber = Regex.Replace(PhoneNumber, @"\s", "");
                PhoneNumber = Regex.Replace(PhoneNumber, "[^0-9]", "");
                return PhoneNumber;
            }

            //Don't do any conversion if the value is not a string
            return Binding.DoNothing;
        }
    }
}
