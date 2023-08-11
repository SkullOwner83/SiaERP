using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace SiaERP.Resources.Utilities
{
    public class KeepRatio : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double totalWidth)
            {
                return totalWidth / 3;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

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
            if ((Value is bool isChecked && isChecked == true) && int.TryParse(Parameter.ToString(), out int OptionValue))
            {
                return OptionValue;
            }

            return Binding.DoNothing;
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

            //Return the original value if not is string or is null
            return Value;
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

            //Return the original value if not is string or is null
            return Value;
        }
    }

    //Insert the currency symbol to valyue if is read only
    public class CurrencyFormat : IValueConverter
    {
        public object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            if (Value is decimal Price)
            {
                return $"${Price}";
            }

            return Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    //Show or collapse control depending of control width
    public class WidthToShow : IValueConverter
    {
        public object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            if (Value is double CurrentWidth && int.TryParse(Parameter.ToString(), out int RequeridWidth))
            {
                //Check if the width is greater than or equal that parameter value
                return CurrentWidth >= RequeridWidth ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            throw new NotImplementedException();
        }
    }

    //Keep one column roportion
    public class KeepProportion : IValueConverter
    {
        //Divide the current width of control by the proportion of the parameter value
        public object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            if (Value is double CurrentWidth && int.TryParse(Parameter.ToString(), out int Proportion))
            {
                return CurrentWidth / Proportion;
            }
            return 0;
        }

        public object ConvertBack(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            throw new NotImplementedException();
        }
    }

    //?????
    public class BooleanToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isVisible && isVisible == false)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

