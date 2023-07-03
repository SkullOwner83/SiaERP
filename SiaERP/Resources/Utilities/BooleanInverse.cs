using System;
using System.Globalization;
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

        public object ConvertBack(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IntToBoolean : IValueConverter
    {
        public object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            //Check if input value and the parameter are int value and return true if they match
            if (Value is int SelectedValue && Parameter is int OptionValue)
            {
                return SelectedValue == OptionValue;
            }
            
            return false;
        }

        public object ConvertBack(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            if (Value is bool isChecked && isChecked && Parameter is int OptionValue)
            {
                return OptionValue;
            }

            return Binding.DoNothing;
        }
    }
}
