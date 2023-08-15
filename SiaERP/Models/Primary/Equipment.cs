using System;

namespace SiaERP.Models
{
    public class Equipment
    {
        //Define class properties
        private int _Id;
        private int _Owner;
        private string _Brand;
        private string _Model;
        private string _SerialNumber;
        private string _Color;
        private DateTime _RegisterDate;

        //Encapsulate properties
        public int Id
        {
            get => _Id;
            set => _Id = value;
        }

        public int Owner
        {
            get => _Owner;
            set => _Owner = value; 
        }

        public string Brand
        {
            get => _Brand;
            set => _Brand = value;
        }

        public string Model
        {
            get => _Model;
            set => _Model = value;
        }

        public string SerialNumber
        {
            get => _SerialNumber;
            set => _SerialNumber = value;
        }

        public string Color
        {
            get => _Color;
            set => _Color = value; 
        }

        public DateTime RegisterDate
        {
            get => _RegisterDate;
            set => _RegisterDate = value;
        }

        //Contructor method
        public Equipment()
        {
            RegisterDate = DateTime.Now;
        }
    }
}
