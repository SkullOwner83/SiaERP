namespace SiaERP.Models
{
	public class Service
	{
		//Define private varibales
		private int _Folio;
        private int _Customer;
        private string _Name;
        private string _AdmissionDate;
        private string? _DeliveryDate;
        private string _Status;
        private int _Product;
        private string? _Diagnostic;
		private string? _ProblemOrService;

		//Define public properties
		public int Folio 
		{ 
			get => _Folio; 
			set => _Folio = value; 
		}

		public int Customer 
		{ 
			get => _Customer; 
			set => _Customer = value; 
		}

		public string Name 
		{ 
			get => _Name; 
			set => _Name = value;
		}

		public string AdmissionDate 
		{ 
			get => _AdmissionDate; 
			set => _AdmissionDate = value; 
		}

		public string? DeliveryDate 
		{ 
			get => _DeliveryDate; 
			set => _DeliveryDate = value; 
		}

		public string Status 
		{ 
			get => _Status; 
			set => _Status = value; 
		}

		public int Product 
		{ 
			get => _Product; 
			set => _Product = value; 
		}

		public string? Diagnostic 
		{ 
			get => _Diagnostic; 
			set => _Diagnostic = value; 
		}

		public string? ProblemOrService 
		{ 
			get => _ProblemOrService; 
			set => _ProblemOrService = value; 
		}
	}
}
