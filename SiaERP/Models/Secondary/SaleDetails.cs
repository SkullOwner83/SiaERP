namespace SiaERP.Models
{
    public class SaleDetails
    {
        //Define class properties
        private int _Id;
        private int _Sale;
        private int _Product;
        private int _Quantity;
        private string _ProductName;
        private decimal _Price;
        private decimal _Discount;

        //Encapsulate properties
        public int Id
        {
            get => _Id;
            set => _Id = value;
        }

        public int Sale
        {
            get => _Sale;
            set => _Sale = value;
        }

        public int Product
        {
            get => _Product;
            set => _Product = value;
        }

        public int Quantity
        {
            get => _Quantity;
            set => _Quantity = value;
        }

        public string ProductName
        { 
            get => _ProductName;
            set => _ProductName = value; 
        }

        public decimal Price
        {
            get => _Price;
            set => _Price = value;
        }

        public decimal Discount
        {
            get => _Discount;
            set => _Discount = value;
        }        
    }
}
