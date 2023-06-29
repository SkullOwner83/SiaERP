using SiaERP.Resources.Utilities;
using System.Collections.ObjectModel;
using SiaERP.Data;
using Models;
using System.Windows.Input;
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace SiaERP.ViewModels
{
    internal class CustomerViewModel : ViewModelBase
	{
        //Define private variables
        private SqlCustomerQuery Query;
        private ObservableCollection<Customer> listcustomers;
        private Customer selectedcustomer;
        private bool _EnableEdition = true;

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
		public bool EnableEdition 
        { 
            get => _EnableEdition;
            set
            {
                _EnableEdition = value;
                OnPropertyChanged(nameof(_EnableEdition));
            }
        }

        //Define commands
        public ICommand CmdNew { get; }
        public ICommand CmdDelete { get; }
        public ICommand CmdModify { get; }
        public ICommand CmdPrint { get; }

        //Constructor method
        public CustomerViewModel()
		{
			SqlCustomerQuery Query = new SqlCustomerQuery();
            CmdDelete = new ViewModelCommand(DeleteCustomerExecute, DeleteCustomerCanExecute);
			CmdModify = new ViewModelCommand(ModifyCustomerExecute, DeleteCustomerCanExecute);
			CmdPrint = new ViewModelCommand(ModifyCustomerExecute, DeleteCustomerCanExecute);

            ListCustomers = new ObservableCollection<Customer>();
			ListCustomers = Query.Get();
        }

        private bool DeleteCustomerCanExecute(object obj)
        {
            if (SelectedCustomer != null)
                return true;
            else
                return false;
        }

        private void ModifyCustomerExecute(object obj)
        {
            MessageBox.Show("Hola");
            EnableEdition = !EnableEdition;
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
