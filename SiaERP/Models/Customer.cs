using System;

namespace Models
{
    public class Customer
    {
		//Customer fields
        private int id;
        private int type;
        private string name;
        private string rfc;
        private string phonenumber;
        private string email;
        private string adress;
        private string city;
        private string state;
        private string country;
        private string postalcode;
        private string taxregime;
		private DateTime registerdate;

		//Customer Properties
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		public int Type
		{
			get { return type; }
			set { type = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string RFC
		{
			get { return rfc; }
			set { rfc = value; }
		}

		public string PhoneNumber
		{
			get { return phonenumber; }
			set { phonenumber = value; }
		}

		public string Email
		{
			get { return email; }
			set { email = value; }
		}

		public string Adress
		{
			get { return adress; }
			set { adress = value; }
		}

		public string City
		{
			get { return city; }
			set { city = value; }
		}

		public string State
		{
			get { return state; }
			set { state = value; }
		}

		public string Country
		{
			get { return country; }
			set { country = value; }
		}

		public string PostalCode
		{
			get { return postalcode; }
			set { postalcode = value; }
		}

		public string TaxRegime
		{
			get { return taxregime; }
			set { taxregime = value; }
		}

		public DateTime RegisterDate
		{
			get { return registerdate; }
			set { registerdate = value; }
		}
    }
}
