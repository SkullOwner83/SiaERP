namespace SiaERP.Models
{
	internal class Product
	{
		//Define private varibales
		private int _Id;
		private string _ProductType;
		private string _Catergory;
		private int _Supplier;
		private string _Name;
		private string? _Description;
		private byte[] _Image;
		private string? _Code;
		private decimal _BuyPrice;
		private decimal _SalePrice;
		private int _Stock;
		private int _MinStock;

		//Define public properties
		public int Id
		{
			get => _Id; 
			set => _Id = value; 
		}

		public string ProductType
		{ 
			get => _ProductType; 
			set => _ProductType = value; 
		}

		public string Catergory
		{ 
			get => _Catergory; 
			set => _Catergory = value; 
		}

		public int Supplier 
		{ 
			get => _Supplier; 
			set => _Supplier = value;
		}

		public string Name 
		{ 
			get => _Name; 
			set => _Name = value; 
		}

		public string? Description 
		{ 
			get => _Description; 
			set => _Description = value;
		}

		public byte[] Image
		{ 
			get => _Image; 
			set => _Image = value; 
		}

		public string? Code 
		{ 
			get => _Code; 
			set => _Code = value; 
		}

		public decimal BuyPrice 
		{ 
			get => _BuyPrice; 
			set => _BuyPrice = value; 
		}

		public decimal SalePrice 
		{ 
			get => _SalePrice; 
			set => _SalePrice = value; 
		}
		public int Stock 
		{ 
			get => _Stock; 
			set => _Stock = value; 
		}

		public int MinStock 
		{ 
			get => _MinStock; 
			set => _MinStock = value; 
		}
	}
}
