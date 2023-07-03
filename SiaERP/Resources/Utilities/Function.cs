using Models;
using SiaERP.ViewModels;
using System;
using System.Reflection;

namespace SiaERP.Resources.Utilities
{
    public class Function
    {
        public static void CloneObject(Customer Source, Customer Destination)
        {
            Type ObjectType = typeof(Customer);
            PropertyInfo[] Properties = ObjectType.GetProperties();

            foreach (PropertyInfo Property in Properties)
            {
                if (Property.CanWrite)
                {
                    object? Value = Property.GetValue(Source);
                    Property.SetValue(Destination, Value);
                }
            }
        }
    }
}
