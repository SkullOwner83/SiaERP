namespace SiaERP.Models
{
	public class Service
	{
		//Services fields
		private int _Folio;
        private int _Customer;
        private string _Name;
        private string _AdmissionDate;
        private string? _DeliveryDate;
        private string _Status;
        private int _Product;
        private string? _Diagnostic;
		private string? _ProblemOrService;

		//Services Properties
		public int Folio
		{
			get { return _Folio; }
			set { _Folio = value; }
		}

		public int Customer
		{
			get { return _Customer; }
			set { _Customer = value; }
		}

		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		public string AdmissionDate
		{
			get { return _AdmissionDate; }
			set { _AdmissionDate = value; }
		}

		public string DeliveryDate
		{
			get { return _DeliveryDate; }
			set { _DeliveryDate = value; }
		}

		public string Status
		{
			get { return _Status; }
			set { _Status = value; }
		}

		public int Product
		{
			get { return _Product; }
			set { _Product = value; }
		}

		public string Diagnostic
		{
			get { return _Diagnostic; }
			set { _Diagnostic = value; }
		}

		public string ProblemOrService
		{
			get { return _ProblemOrService; }
			set { _ProblemOrService = value; }
		}
	}
}
