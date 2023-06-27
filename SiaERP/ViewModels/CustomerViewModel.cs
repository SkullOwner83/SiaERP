using SiaERP.Resources.Utilities;
using System.Collections.ObjectModel;
using SiaERP.Data;
using Models;
using System.Windows.Input;
using System;
using System.Windows;

namespace SiaERP.ViewModels
{
    internal class CustomerViewModel : ViewModelBase
	{
        //Define private variables
        private SqlCustomerQuery Query;
        private ObservableCollection<Customer> listcustomers;
        private Customer selectedcustomer;

        //Define public properties
        public ObservableCollection<Customer> ListCustomers 
        { 
            get => listcustomers;
            set
            {
                listcustomers = value;
                OnPropertyChanged(nameof(listcustomers));
            }
        }

        public Customer SelectedCustomer 
        {
            get => selectedcustomer;
            set
            {
                selectedcustomer = value;
                OnPropertyChanged(nameof(selectedcustomer));
            }
        }

        //Define commands
        public ICommand CmdNew { get; }
        public ICommand CmdDelete { get; }
        public ICommand CmdModify { get; }

        //Constructor method
        public CustomerViewModel()
		{
			SqlCustomerQuery Query = new SqlCustomerQuery();
            CmdDelete = new ViewModelCommand(DeleteCustomerExecute);

            ListCustomers = new ObservableCollection<Customer>();
			ListCustomers = Query.Get();
        }

        private void DeleteCustomerExecute(object obj)
        {
            if (SelectedCustomer != null)
            {
                MessageBoxResult Result = MessageBox.Show($"¿Estás seguro que deseas eliminar el cliente {SelectedCustomer.Name}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {
                    //Query.Delete(SelectedCustomer);
                    ListCustomers.Remove(SelectedCustomer);
                }
            }
        }
    }
}
