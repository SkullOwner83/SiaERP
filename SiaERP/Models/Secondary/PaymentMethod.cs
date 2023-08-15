namespace SiaERP.Models.Secondary
{
	public class PaymentMethod
	{
		//Define private varibales
		private int _Id;
		private string _PaymentName;

		//Define public properties
		public int Id { get => _Id; }
		public string PaymentName { get => _PaymentName; }

		//Constructor method
		public PaymentMethod(int id)
		{
			_Id = id;
            _PaymentName = id switch
			{
				1 => "Efectivo",
				2 => "Tarjeta de debito",
				3 => "Tarjeta de credito",
				4 => "Transferencia",
                _ => throw new System.NotImplementedException()
            };
		}
	}
}
