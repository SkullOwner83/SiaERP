using SiaERP.Resources.Utilities;
using System.Collections.ObjectModel;
using SiaERP.Data;
using Models;

namespace SiaERP.ViewModels
{
    internal class CustomerViewModel : ViewModelBase
	{
        private ObservableCollection<Customer> listCustomers;

        public ObservableCollection<Customer> ListCustomers 
        { 
            get => listCustomers;
            set
            {
                listCustomers = value;
                OnPropertyChanged(nameof(listCustomers));
            }
        }

        public CustomerViewModel()
		{
			SqlCustomerQuery Query = new SqlCustomerQuery();

			ListCustomers = new ObservableCollection<Customer>();
			ListCustomers = Query.Get();
        }

    }
}
