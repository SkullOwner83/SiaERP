using System;
using System.Windows.Media.Imaging;

namespace SiaERP.Models
{
    public class Customer
    {
        //Define class properties
        private int _Id;
        private int _Type;
        private string _Name;
        private string? _RFC;
        private string? _PhoneNumber;
        private string? _Email;
        private string? _Adress;
        private string? _City;
        private string? _State;
        private string? _Country;
        private string? _PostalCode;
        private int _Taxregime;
		private DateTime _Registerdate;
        private BitmapImage? _Image;
        private bool _Trusted;
		private Byte[] _TaxDocument;


        //Encapsulate properties
        public int Id
		{
			get { return _Id; }
			set { _Id = value;}
		}

		public int Type
		{
			get { return _Type; }
			set { _Type = value; }
		}

		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		public string? RFC
		{
			get { return _RFC; }
			set { _RFC = value; }
		}

		public string? PhoneNumber
		{
			get { return _PhoneNumber; }
			set { _PhoneNumber = value; }
		}

		public string? Email
		{
			get { return _Email; }
			set { _Email = value; }
		}

		public string? Adress
		{
			get { return _Adress; }
			set { _Adress = value; }
		}

		public string? City
		{
			get { return _City; }
			set { _City = value; }
		}

		public string? State
		{
			get { return _State; }
			set { _State = value; }
		}

		public string? Country
		{
			get { return _Country; }
			set { _Country = value; }
		}

		public string? PostalCode
		{
			get { return _PostalCode; }
			set { _PostalCode = value; }
		}

		public int TaxRegime
		{
			get { return _Taxregime; }
			set { _Taxregime = value; }
		}

		public DateTime RegisterDate
		{
			get { return _Registerdate; }
			set { _Registerdate = value; }
		}

        public bool Trusted 
		{ 
			get => _Trusted; 
			set => _Trusted = value; 
		}

        public BitmapImage? Image
		{ 
			get => _Image; 
			set => _Image = value; 
		}

        public byte[] TaxDocument
		{ 
			get => _TaxDocument; 
			set => _TaxDocument = value; 
		}

        //Constructor method
        public Customer()
		{
            Type = 1;
			RegisterDate = DateTime.Now;
		}
    }
}
