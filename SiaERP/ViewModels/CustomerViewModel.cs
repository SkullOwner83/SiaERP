using System.Collections.ObjectModel;
using SiaERP.Resources.Utilities;
using System.Reflection;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;
using SiaERP.Data;
using SiaERP.Models;
using System;
using System.Linq;

namespace SiaERP.ViewModels
{
    internal class CustomerViewModel : ViewModelBase
	{
        //Define property class
        private SqlCustomerQuery CustomerQuery;
        private ObservableCollection<Customer>? _Listcustomers;
        private ObservableCollection<TaxRegime> _ListTaxRegime;
        private Customer? _SelectedCustomer;
        private TaxRegime? _SelectedTaxRegime;
        private Customer? _AuxiliarCustomer;
        private ImageSource? _CustomerImage;

        private string? _Filter;
        private bool _EnableEdition = false;
        private string Action = "None";

        #region Property encapsulation
        public ObservableCollection<Customer>? ListCustomers 
        { 
            get => _Listcustomers;
            set
            {
                _Listcustomers = value;
                OnPropertyChanged(nameof(ListCustomers));
            }
        }

        public ObservableCollection<TaxRegime> ListTaxRegime
        {
            get => _ListTaxRegime;
            set
            {
                _ListTaxRegime = value;
                OnPropertyChanged(nameof(ListTaxRegime));
            }
        }

        public Customer? SelectedCustomer 
        {
            get => _SelectedCustomer;
            set
            {
                _SelectedCustomer = value;
                SelectionItemChanged();
                OnPropertyChanged(nameof(SelectedCustomer));

                //Search in list categories the corresponding to the product by his id and select it
                if (SelectedCustomer != null && EnableEdition == false)
                {
                    SelectedTaxRegime = ListTaxRegime.FirstOrDefault(p => p.Id == SelectedCustomer.TaxRegime);
                }
            }
        }

        public TaxRegime? SelectedTaxRegime
        {
            get => _SelectedTaxRegime;
            set
            {
                _SelectedTaxRegime = value;
                OnPropertyChanged(nameof(SelectedTaxRegime));

                //Assigns the selected category to the product currently being edited
                if (SelectedTaxRegime != null && AuxiliarCustomer != null && EnableEdition == true)
                {
                    AuxiliarCustomer.TaxRegime = SelectedTaxRegime.Id;
                }
            }
        }

        public Customer? AuxiliarCustomer 
        { 
            get => _AuxiliarCustomer;
            set
            {
                _AuxiliarCustomer = value;
                OnPropertyChanged(nameof(AuxiliarCustomer));
            }
        } 

        public ImageSource? CustomerImage
        {
            get => _CustomerImage;
            set
            {
                _CustomerImage = value;
                OnPropertyChanged(nameof(CustomerImage));
            }
        }

        public string? Filter 
        { 
            get => _Filter;
            set
            {
                _Filter = value;
                OnPropertyChanged(nameof(Filter));
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
        #endregion

        #region Define commands
        public ICommand CmdCRUD { get; }      
        public ICommand CmdAcept { get; }
        public ICommand CmdCancel { get; }
        public ICommand CmdFilter { get; }
        public ICommand CmdLoadImage { get; }
        public ICommand CmdUploadTaxDocument { get; }
        #endregion

        //Constructor method
        public CustomerViewModel()
        {
            CmdCRUD = new ViewModelCommand(CRUDExecute, CRUDCanExecute);
            CmdAcept = new ViewModelCommand(AceptActionExecute, ActionCanExecute);
            CmdCancel = new ViewModelCommand(CancelActionExecute, ActionCanExecute);
            CmdFilter = new ViewModelCommand(FilterExecute);
            CmdLoadImage = new ViewModelCommand(LoadImageExecute, ActionCanExecute);

            CustomerQuery = new SqlCustomerQuery();
            ListCustomers = new ObservableCollection<Customer>();
            ListCustomers = CustomerQuery.Read();

            ListTaxRegime = DataManagement.ListTaxRegime();
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

                //Clone the object selected and assign it to currently being edited
                AuxiliarCustomer = CloneObject(SelectedCustomer);
            }
        }

        //Database query management execute command
        private void CRUDExecute(object Parameter)
        {
            string? ButtonName = Parameter as string;

            switch (ButtonName)
            {
                //Create new register
                case "Create":
                    EnableEdition = true;
                    SelectedTaxRegime = null;
                    Filter = string.Empty;
                    Action = "Create";

                    //Clear the auxiliar customer, get last id in database and call property to update view
                    AuxiliarCustomer = CloneObject(null);
                    AuxiliarCustomer.Id = CustomerQuery.LastId() + 1;
                    OnPropertyChanged(nameof(AuxiliarCustomer));
                    break;

                //Update data of register in database
                case "Update":
                    EnableEdition = true;
                    Filter = string.Empty;
                    Action = "Update";
                    break;

                //Delete register of data grid and database
                case "Delete":
                    MessageBoxResult Result = MessageBox.Show($"¿Estás seguro que deseas eliminar el cliente {SelectedCustomer.Name}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (Result == MessageBoxResult.Yes)
                    {
                        CustomerQuery.Delete(SelectedCustomer);
                        ListCustomers.Remove(SelectedCustomer);
                        AuxiliarCustomer = null;
                    }
                    break;
            }
        }

        //Database query management can execute command
        private bool CRUDCanExecute(object Parameter)
        {
            string? ButtonName = Parameter as string;
            bool CanExecute = false;

            switch (ButtonName)
            {
                //Enable button if edition is false
                case "Create":
                    if (EnableEdition == false)
                        CanExecute = true;
                    else
                        CanExecute = false;
                    break;

                //Enable buttons if there is an object selected in datagrid
                case "Update":
                case "Delete":
                case "Print":
                    if (SelectedCustomer != null && EnableEdition == false)
                        CanExecute = true;
                    else
                        CanExecute = false;
                    break;
            }

            return CanExecute;
        }

        private void FilterExecute(object obj)
        {
            ListCustomers.Clear();
            CustomerQuery.Read(Filter);
        }

        private void AceptActionExecute(object obj)
        {
            if (Action == "Create")
            {
                CustomerQuery.Create(AuxiliarCustomer);
            }
            else if (Action == "Update")
            {
                CustomerQuery.Update(AuxiliarCustomer);
            }

            ListCustomers.Clear();
            ListCustomers = CustomerQuery.Read();
            EnableEdition = false;
        }

        private void CancelActionExecute(object obj)
        {
            EnableEdition = false;
            AuxiliarCustomer = null;
            SelectedTaxRegime = null;
            Action = "None";
        }

        private bool ActionCanExecute(object obj)
        {
            if (EnableEdition == true)
                return true;
            else
                return false;
        }

        //Load customer image from file explorer
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
