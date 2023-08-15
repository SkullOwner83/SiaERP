using System;

namespace SiaERP.Models
{
	internal class ProductCategory
	{
        //Define class properties
        private int _Id;
		private string _CategoryName;

        //Encapsulate properties
        public int Id
		{ 
			get => _Id; 
			set => _Id = value;
		}

		public string CategoryName
		{
			get => _CategoryName; 
			set => _CategoryName = value;
		}
	}
}
