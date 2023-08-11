using System;

namespace SiaERP.Models
{
	public class Service : IDisposable
	{
        //Service fields
        private int _idService;
		private int _idCustomer;
		private string _CustomerName;
        private DateTime _AdmissionDate;
        private DateTime _DeliveryDate;
        private int _Status;
        private int _Product;
        private string? _Diagnostic;
		private string? _ProblemOrService;

        //Service Properties
        public int Id
        { 
			get => _idService; 
			set => _idService = value; 
		}

		public int IdCustomer 
		{ 
			get => _idCustomer; 
			set => _idCustomer = value; 
		}

		public string CustomerName
        {
			get => _CustomerName; 
			set => _CustomerName = value;
		}

		public DateTime AdmissionDate 
		{ 
			get => _AdmissionDate; 
			set => _AdmissionDate = value; 
		}

		public DateTime DeliveryDate 
		{ 
			get => _DeliveryDate; 
			set => _DeliveryDate = value; 
		}

		public int Status 
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

		//Constructor method
		public Service()
		{
			AdmissionDate = DateTime.Now;
			Status = 1;
        }

        public void Dispose()
        {
            
        }

		internal Customer? FirstOrDefault(Func<object, bool> value)
		{
			throw new NotImplementedException();
		}
	}
}
