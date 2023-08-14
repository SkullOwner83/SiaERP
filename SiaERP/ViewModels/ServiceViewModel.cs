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
		//Define property class
		private SqlServiceQuery ServiceQuery;
        private SqlCustomerQuery CustomerQuery;
        private ObservableCollection<Service> _ListServices;
        private ObservableCollection<Customer> _ListCustomers;
        private ObservableCollection<ServiceStatus> _ListStatus;
        private Service? _SelectedService;
        private Customer? _SelectedCustomer;
        private ServiceStatus? _SelectedStatus;
        private Service? _AuxiliarService;

        private bool _EnableEdition = false;
        private string Action = "None";

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

        public ObservableCollection<ServiceStatus> ListStatus
        {
            get => _ListStatus;
            set
            {
                _ListStatus = value;
                OnPropertyChanged(nameof(ListStatus));
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

				//Search in list services the corresponding to the customer and status by his id and select it
				if (SelectedService != null && EnableEdition == false)
				{
					SelectedCustomer = ListCustomers.FirstOrDefault(p => p.Id == SelectedService.IdCustomer);
					SelectedStatus = ListStatus.FirstOrDefault(p => p.Id == SelectedService.Status);
				}
			}
        }

        public Customer? SelectedCustomer
        {
            get => _SelectedCustomer;
            set
            {
                _SelectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));

				//Assigns the selected customer to the services currently being edited
				if (SelectedCustomer != null && AuxiliarService != null && EnableEdition == true)
				{
					AuxiliarService.IdCustomer = SelectedCustomer.Id;
                    OnPropertyChanged(nameof(AuxiliarService));
				}
			}
        }

        public ServiceStatus? SelectedStatus
        {
            get => _SelectedStatus;
            set
            {
                _SelectedStatus = value;
                OnPropertyChanged(nameof(SelectedStatus));

				//Assigns the selected status to the services currently being edited
				if (SelectedStatus != null && AuxiliarService != null && EnableEdition == true)
				{
					AuxiliarService.Status = SelectedStatus.Id;
				}
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
        #endregion

        #region Define commands
        public ICommand CmdCRUD { get; }
        public ICommand CmdEdition { get; }
        #endregion

        //Constructor method
        public ServiceViewModel()
        {
            CmdCRUD = new ViewModelCommand(CRUDExecute, CRUDCanExecute);
            CmdEdition = new ViewModelCommand(EditionExecute, EditionCanExecute);

            ServiceQuery = new SqlServiceQuery();
            ListServices = new ObservableCollection<Service>();
            ListServices = ServiceQuery.Read();

            CustomerQuery = new SqlCustomerQuery();
            ListCustomers = new ObservableCollection<Customer>();
            ListCustomers = CustomerQuery.Read();

            ListStatus = DataManagement.ListServiceStatus();
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

        //Database query management execute command
        private void CRUDExecute(object Parameter)
        {
            string? ButtonName = Parameter as string;

            switch (ButtonName)
            {
                //Create new register
                case "Create":
                    EnableEdition = true;
                    SelectedCustomer = null;
                    SelectedStatus = null;
                    Filter = string.Empty;
                    Action = "Create";

                    //Clear the auxiliar services, get last id in database and call property to update view
                    AuxiliarService = CloneObject(null);
                    AuxiliarService.Id = ServiceQuery.LastId() + 1;
                    OnPropertyChanged(nameof(AuxiliarService));

                    SelectedStatus = ListStatus.FirstOrDefault(p => p.Id == AuxiliarService.Status);
					break;

                //Update data of register in database
                case "Update":
                    EnableEdition = true;
                    TabControlCollapsed = false;
                    Filter = string.Empty;
                    Action = "Update";
                    break;

                //Delete register of data grid and database
                case "Delete":
                    MessageBoxResult Result = MessageBox.Show($"¿Estás seguro que deseas eliminar el cliente {SelectedService.CustomerName}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (Result == MessageBoxResult.Yes)
                    {
                        ServiceQuery.Delete(SelectedService);
                        ListServices.Remove(SelectedService);
                        AuxiliarService = null;
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
                    if (SelectedService != null && EnableEdition == false)
                        CanExecute = true;
                    else
                        CanExecute = false;
                    break;
            }

            return CanExecute;
        }

        //Edition actions on editable object execute command
        private void EditionExecute(object Parameter)
        {
            string? ButtonName = Parameter as string;
            bool CanExecute = false;

            switch(ButtonName)
            {
                //Acept action execute
                case "Acept":
                    if (Action == "Create")
                    {
                        ServiceQuery.Create(AuxiliarService);
                    }
                    else if (Action == "Update")
                    {
                        ServiceQuery.Update(AuxiliarService);
                    }

                    ListServices.Clear();
                    ListServices = ServiceQuery.Read();
                    EnableEdition = false;
                    break;

                //Cancel action execute
                case "Cancel":
                    EnableEdition = false;
                    AuxiliarService = null;
                    SelectedCustomer = null;
                    SelectedStatus = null;
                    Action = "None";
                    break;
            }
        }

        //Edition actions on editable object  can execute command
        private bool EditionCanExecute(object Parameter)
        {
            if (EnableEdition == true)
                return true;
            else
                return false;
        }

        private void FilterExecute(object Parameter)
        {
            ListCustomers.Clear();
            CustomerQuery.Read(Filter);
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
