using SiaERP.Resources.Utilities;
using System.Collections.ObjectModel;
using SiaERP.Data;
using System.Windows.Input;
using System.Windows;
using Models;
using System.Reflection;


using MySql.Data.MySqlClient;
using System;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SiaERP.ViewModels
{
    internal class CustomerViewModel : ViewModelBase
	{
        //Define property class
        private SqlCustomerQuery Query;
        private ObservableCollection<Customer> _Listcustomers;
        private Customer? _Selectedcustomer;
        private Customer? _Auxiliarcustomer;
        private bool _EnableEdition = false;
        private string Action = "None";
        private string _Filter;
        private ImageSource _CustomerImage;

        #region Property encapsulation
        public ObservableCollection<Customer> ListCustomers 
        { 
            get => _Listcustomers;
            set
            {
                _Listcustomers = value;
                OnPropertyChanged(nameof(ListCustomers));
            }
        }

        public Customer? SelectedCustomer 
        {
            get => _Selectedcustomer;
            set
            {
                _Selectedcustomer = value;
                SelectionItemChanged();
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }

		public Customer? AuxiliarCustomer 
        { 
            get => _Auxiliarcustomer;
            set
            {
                _Auxiliarcustomer = value;
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
        
        public ImageSource CustomerImage
        {
            get => _CustomerImage;
            set
            {
                _CustomerImage = value;
                OnPropertyChanged(nameof(CustomerImage));
            }
        }
        #endregion

        #region Define commands
        public ICommand CmdNew { get; }
        public ICommand CmdDelete { get; }
        public ICommand CmdModify { get; }
        public ICommand CmdPrint { get; }
        public ICommand CmdAcept { get; }
        public ICommand CmdCancel { get; }
        public ICommand CmdFilter { get; }
        public ICommand CmdLoadImage { get; }
        public ICommand CmdCollapseColumn { get; }
        #endregion

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
            CmdLoadImage = new ViewModelCommand(LoadImageExecute, ActionCanExecute);
            CmdCollapseColumn = new ViewModelCommand(CollapseColumnExecute);

            ListCustomers = new ObservableCollection<Customer>();
            ListCustomers = Query.Read();
        }

        private void CollapseColumnExecute(object obj)
        {
            
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
                if (AuxiliarCustomer == null)
                {
                    AuxiliarCustomer = new Customer();
                }

                AuxiliarCustomer = CloneObject(SelectedCustomer);
            }
		}

        //Create new customer register
		private void CreateCustomerExecute(object obj)
        {
            EnableEdition = true;
            Filter = string.Empty;
            Action = "Create";

            //Clear the auxiliar customer, get last id in database and call property to update view
            AuxiliarCustomer = CloneObject(null);
            AuxiliarCustomer.Id = Query.LastId() + 1;
            OnPropertyChanged(nameof(AuxiliarCustomer));
        }

        //Update data of customer in database
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
                AuxiliarCustomer = null;
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
            AuxiliarCustomer = null;
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

        private void LoadImageExecute(object obj)
        {
            OpenFileDialog LoadFile = new OpenFileDialog();
            LoadFile.Filter = "Image Files (*.jpg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (LoadFile.ShowDialog() == true)
            {
                AuxiliarCustomer.Image = new BitmapImage(new Uri(LoadFile.FileName));
                OnPropertyChanged(nameof(AuxiliarCustomer));
            }
        }

        //Clone properties from one instance to another instance
        public Customer CloneObject(Customer? Source)
        {
            //Create new instances of source and destination, if the parameter is null
            using (Customer ObjectSource = Source ?? new Customer())
            {
                using (Customer ObejctDestination = new Customer())
                {
                    //Get all properties of object and set the values to the clone
                    PropertyInfo[] Properties = typeof(Customer).GetProperties();

                    foreach (PropertyInfo Property in Properties)
                    {
                        if (Property.CanWrite)
                        {
                            object? Value = Property.GetValue(ObjectSource);
                            Property.SetValue(ObejctDestination, Value);
                        }
                    }

                    return ObejctDestination;
                }
            }
        }
    }
}
