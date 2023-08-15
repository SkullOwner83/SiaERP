using System;
using System.Windows.Media.Imaging;

namespace SiaERP.Models
{
	internal class Product
	{
		//Define private varibales
		private int _Id;
		private int _Type;
		private int _Category;
		private int _Supplier;
		private string _Name;
		private string? _Brand;
		private string? _Description;
		private string? _CategoryName;
		private string? _Code;
		private decimal _BuyPrice;
		private decimal _SalePrice;
		private int _Stock;
		private int _MinStock;
		private BitmapImage? _Image;

        //Encapsulate properties
        public int Id
		{
			get => _Id; 
			set => _Id = value; 
		}

		public int Type
		{ 
			get => _Type; 
			set => _Type = value; 
		}

		public int Category
		{ 
			get => _Category; 
			set => _Category = value; 
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

        public string? Brand
        {
            get => _Brand;
            set => _Brand = value;
        }

        public string? Description 
		{ 
			get => _Description; 
			set => _Description = value;
		}

        public string? CategoryName
		{
			get => _CategoryName;
			set => _CategoryName = value;
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
        public BitmapImage? Image
        {
            get => _Image;
            set => _Image = value;
        }

		//Constructor method
        public Product()
		{
			Type = 1;
			BuyPrice = 0;
			SalePrice = 0;
			Stock = 0;
			MinStock = 10;
		}
    }
}
