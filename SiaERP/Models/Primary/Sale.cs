using System;

namespace SiaERP.Models
{
    public class Sale
    {
        //Define private varibales
        private int _Id;
        private int _Customer;
        private int _AttendedBy;
        private DateTime _Date;
        private int _PaymentMethod;
        private decimal _Total;

        //Encapsulate properties
        public int Id
        {
            get => _Id;
            set => _Id = value;
        }

        public int Customer
        {
            get => _Customer;
            set => _Customer = value;
        }

        public int AttendedBy
        {
            get => _AttendedBy;
            set => _AttendedBy = value;
        }

        public DateTime Date
        {
            get => _Date;
            set => _Date = value;
        }

        public int PaymentMethod
        {
            get => _PaymentMethod;
            set => _PaymentMethod = value;
        }

        public decimal Total
        {
            get => _Total;
            set => _Total = value;
        }

        //Constructor method
        public Sale()
        {
            Date = DateTime.Now;
            PaymentMethod = 1;
        }
    }
}
