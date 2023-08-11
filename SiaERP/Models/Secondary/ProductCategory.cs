using System;

namespace SiaERP.Models
{
	internal class ProductCategory : IDisposable
	{
		private int _Id;
		private string _CategoryName;

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

		public void Dispose()
		{

		}
	}
}
