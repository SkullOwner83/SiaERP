using SiaERP.Resources.Utilities;
using System.Collections.ObjectModel;
using SiaERP.Data;
using System.Windows.Input;
using System.Windows;
using Models;

using System.Runtime.Serialization;
using System.Reflection;
using System;

namespace SiaERP.ViewModels
{
    internal class CustomerViewModel : ViewModelBase
	{
        //Define private variables
        private SqlCustomerQuery Query;
        private ObservableCollection<Customer> listcustomers;
        private Customer selectedcustomer;
        private Customer auxiliarcustomer;
        private bool _EnableEdition = false;
        private string Action = "None";
        private string _Filter;

        //Define public properties
        public ObservableCollection<Customer> ListCustomers 
        { 
            get => listcustomers;
            set
            {
                listcustomers = value;
                OnPropertyChanged(nameof(ListCustomers));
            }
        }

        public Customer SelectedCustomer 
        {
            get => selectedcustomer;
            set
            {
                selectedcustomer = value;
                SelectionItemChanged();
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }

		public Customer AuxiliarCustomer 
        { 
            get => auxiliarcustomer;
            set
            {
                auxiliarcustomer = value;
                OnPropertyChanged(nameof(AuxiliarCustomer));
            }
        }

        public bool EnableEdition 
        { 
            get => _EnableEdition;
            set
            {
                _EnableEdition = value;
                OnPropertyChanged(nameof(EnableEdition));
            }
        }

        public string Filter 
        { 
            get => _Filter;
            set
            {
                _Filter = value;
                OnPropertyChanged(nameof(Filter));
            }
        }

        //Define commands
        public ICommand CmdNew { get; }
        public ICommand CmdDelete { get; }
        public ICommand CmdModify { get; }
        public ICommand CmdPrint { get; }
        public ICommand CmdAcept { get; }
        public ICommand CmdCancel { get; }
        public ICommand CmdFilter { get; }

        //Constructor method
        public CustomerViewModel()
        {
            Query = new SqlCustomerQuery();
            CmdNew = new ViewModelCommand(CreateCustomerExecute, CreateCustomerCanExecute);
            CmdDelete = new ViewModelCommand(DeleteCustomerExecute, DeleteCustomerCanExecute);
            CmdModify = new ViewModelCommand(UpdateCustomerExecute, DeleteCustomerCanExecute);
            CmdPrint = new ViewModelCommand(UpdateCustomerExecute, DeleteCustomerCanExecute);
            CmdAcept = new ViewModelCommand(AceptActionExecute, ActionCanExecute);
            CmdCancel = new ViewModelCommand(CancelActionExecute, ActionCanExecute);
            CmdFilter = new ViewModelCommand(FilterExecute);

            ListCustomers = new ObservableCollection<Customer>();
            ListCustomers = Query.Read();

            AuxiliarCustomer = new Customer();
        }

        private void FilterExecute(object obj)
        {

            ListCustomers.Clear();
            Query.Read(Filter);
        }

        //Set properties of selected item in the form
        private void SelectionItemChanged()
		{
            if (EnableEdition == false)
            {
                CloneObject(SelectedCustomer, AuxiliarCustomer);
            }
		}

        //Create new customer register
		private void CreateCustomerExecute(object obj)
        {
            EnableEdition = true;
            Filter = string.Empty;
            Action = "Create";

            using (Customer NewCustomer = new Customer() { Type = 1, RegisterDate = DateTime.Now})
            {
                CloneObject(NewCustomer, AuxiliarCustomer);
            }
        }

        private void UpdateCustomerExecute(object obj)
        {
            EnableEdition = true;
            Filter = string.Empty;
            Action = "Update";
        }

        //Delete customer of data grid and data base
        private void DeleteCustomerExecute(object obj)
        {
            MessageBoxResult Result = MessageBox.Show($"¿Estás seguro que deseas eliminar el cliente {SelectedCustomer.Name}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (Result == MessageBoxResult.Yes)
            {
                Query.Delete(SelectedCustomer);
                ListCustomers.Remove(SelectedCustomer);
                AuxiliarCustomer = SelectedCustomer;
            }
        }

        private void AceptActionExecute(object obj)
        {
            if (Action == "Create")
            {
                Query.Create(AuxiliarCustomer);
            }
            else if (Action == "Update")
            {
                Query.Update(AuxiliarCustomer);
            }

            ListCustomers.Clear();
            ListCustomers = Query.Read();
            EnableEdition = false;
        }

        private void CancelActionExecute(object obj)
        {
            EnableEdition = false;
            Action = "None";
        }

        private bool CreateCustomerCanExecute(object obj)
        {
            if (EnableEdition == false)
                return true;
            else
                return false;
        }

        private bool DeleteCustomerCanExecute(object obj)
        {
            if (SelectedCustomer != null && EnableEdition == false)
                return true;
            else
                return false;
        }

        private bool ActionCanExecute(object obj)
        {
            if (EnableEdition == true)
                return true;
            else
                return false;
        }

        public void CloneObject(Customer Source, Customer Destination)
        {
            //Get all properties of object and set the values to the clone
            PropertyInfo[] Properties = typeof(Customer).GetProperties();

            foreach (PropertyInfo Property in Properties)
            {
                if (Property.CanWrite)
                {
                    object? Value = Property.GetValue(Source);
                    Property.SetValue(Destination, Value);
                }
            }

            //OnPropertyChanged(nameof(AuxiliarCustomer));
            //Asignarse su propio valor para llamar al setter, ya que Property.SetValue no lo hace
            AuxiliarCustomer = AuxiliarCustomer;
        }
    }
}
