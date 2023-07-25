using SiaERP.Models;
using SiaERP.Data;
using SiaERP.Resources.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace SiaERP.ViewModels
{
    internal class ServiceViewModel : ViewModelBase
    {
        private SqlCustomerQuery CustomerQuery;
        private ObservableCollection<Service> _ListServices;
        private ObservableCollection<Customer> _ListCustomers;
        private Service? _SelectedService;
        private Customer? _SelectedCustomer;
        private Service? _AuxiliarService;
        private bool _EnableEdition = false;
        private string Action = "None";
        private string _Filter;

        #region Property encapsulation
        public ObservableCollection<Service> ListServices
        {
            get => _ListServices;
            set
            {
                _ListServices = value;
                OnPropertyChanged(nameof(ListServices));
            }
        }

        public ObservableCollection<Customer> ListCustomers
        {
            get => _ListCustomers;
            set
            {
                _ListCustomers = value;
                OnPropertyChanged(nameof(ListCustomers));
            }
        }

        public Service? SelectedService
        {
            get => _SelectedService;
            set
            {
                _SelectedService = value;
                SelectionItemChanged();
                OnPropertyChanged(nameof(SelectedService));
            }
        }

        public Customer? SelectedCustomer
        {
            get => _SelectedCustomer;
            set
            {
                _SelectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }

        public Service? AuxiliarService
        {
            get => _AuxiliarService;
            set
            {
                _AuxiliarService = value;
                OnPropertyChanged(nameof(AuxiliarService));
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
        #endregion

        #region Define commands
        public ICommand CmdNew { get; }
        public ICommand CmdDelete { get; }
        public ICommand CmdModify { get; }
        public ICommand CmdPrint { get; }
        public ICommand CmdAcept { get; }
        public ICommand CmdCancel { get; }
        #endregion

        //Constructor method
        public ServiceViewModel()
        {
            ListServices = new ObservableCollection<Service>();
            ListCustomers = new ObservableCollection<Customer>();
            CmdNew = new ViewModelCommand(CreateServiceExecute, CreateServiceCanExecute);
            CmdDelete = new ViewModelCommand(DeleteServiceExecute, DeleteServiceCanExecute);
            CmdModify = new ViewModelCommand(UpdateServiceExecute, DeleteServiceCanExecute);
            CmdPrint = new ViewModelCommand(UpdateServiceExecute, DeleteServiceCanExecute);
            CmdAcept = new ViewModelCommand(AceptActionExecute, ActionCanExecute);
            CmdCancel = new ViewModelCommand(CancelActionExecute, ActionCanExecute);

            CustomerQuery = new SqlCustomerQuery();
            ListCustomers = CustomerQuery.Read();
        }

        //Set properties of selected item in the form
        private void SelectionItemChanged()
        {
            if (EnableEdition == false)
            {
                if (AuxiliarService == null)
                {
                    AuxiliarService = new Service();
                }

                AuxiliarService = CloneObject(SelectedService);
            }
        }

        //Create new customer register
        private void CreateServiceExecute(object obj)
        {
            EnableEdition = true;
            Filter = string.Empty;
            Action = "Create";

            //Clear the auxiliar customer, get last id in database and call property to update view
            AuxiliarService = CloneObject(null);
            //AuxiliarService.Folio = Query.LastId() + 1;
            OnPropertyChanged(nameof(AuxiliarService));
        }

        //Update data of customer in database
        private void UpdateServiceExecute(object obj)
        {
            EnableEdition = true;
            Filter = string.Empty;
            Action = "Update";
        }

        //Delete service of data grid and data base
        private void DeleteServiceExecute(object obj)
        {
            MessageBoxResult Result = MessageBox.Show($"¿Estás seguro que deseas eliminar el cliente {SelectedService.Name}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (Result == MessageBoxResult.Yes)
            {
                //Query.Delete(SelectedService);
                ListServices.Remove(SelectedService);
                AuxiliarService = null;
            }
        }

        private void AceptActionExecute(object obj)
        {
            if (Action == "Create")
            {
                //Query.Create(AuxiliarCustomer);
            }
            else if (Action == "Update")
            {
                //Query.Update(AuxiliarCustomer);
            }

            ListServices.Clear();
            //ListServices = Query.Read();
            EnableEdition = false;
        }

        private bool CreateServiceCanExecute(object obj)
        {
            if (EnableEdition == false)
                return true;
            else
                return false;
        }

        private void CancelActionExecute(object obj)
        {
            EnableEdition = false;
            AuxiliarService = null;
            Action = "None";
        }

        private bool ActionCanExecute(object obj)
        {
            if (EnableEdition == true)
                return true;
            else
                return false;
        }

        private bool DeleteServiceCanExecute(object obj)
        {
            if (SelectedService != null && EnableEdition == false)
                return true;
            else
                return false;
        }

        //Clone properties from one instance to another instance
        public Service CloneObject(Service? Source)
        {
            //Create new instances of source and destination, if the parameter is null
            using (Service ObjectSource = Source ?? new Service())
            {
                using (Service ObejctDestination = new Service())
                {
                    //Get all properties of object and set the values to the clone
                    PropertyInfo[] Properties = typeof(Service).GetProperties();

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
