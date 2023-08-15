using SiaERP.Data;
using SiaERP.Models;
using SiaERP.Models.Secondary;
using SiaERP.Resources.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace SiaERP.ViewModels
{
    internal class SalesViewModel : ViewModelBase
    {
        #region Define property class
        private SqlProductsQuery ProductsQuery;
        private ObservableCollection<Product>? _ListProducts;
        private Product? _SelectedProduct;

        private SqlCustomerQuery CustomerQuery;
        private ObservableCollection<Customer>? _ListCustomers;
        private Customer? _SelectedCustomer;

		private ObservableCollection<Sale>? _ListSales;
		private Sale? _AuxiliarSale;

        private ObservableCollection<SaleDetails>? _ListSaleDatails;
        private SaleDetails? _AuxiliarSaleDetails;

		private ObservableCollection<PaymentMethod> _ListPaymentMethods;
		private PaymentMethod? _SelectedPaymentMethod;
		#endregion

		#region Property encapsulation
		public ObservableCollection<Product>? ListProducts
        {
            get => _ListProducts;
            set
            {
                _ListProducts = value;
                OnPropertyChanged(nameof(ListProducts));
            }
        }

        public ObservableCollection<Customer>? ListCustomers
        {
            get => _ListCustomers;
            set
            {
                _ListCustomers = value;
                OnPropertyChanged(nameof(ListCustomers));
            }
        }

        public Product? SelectedProduct
        {
            get => _SelectedProduct;
            set
            {
                _SelectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));

                //Assigns the product values to the sale details currently being edited
                if (SelectedProduct != null && AuxiliarSaleDetails != null && EnableEdition == true)
                {
                    AuxiliarSaleDetails.Product = SelectedProduct.Id;
					AuxiliarSaleDetails.ProductName = SelectedProduct.Name;
					AuxiliarSaleDetails.Quantity = 1;
					AuxiliarSaleDetails.Price = SelectedProduct.SalePrice;
					OnPropertyChanged(nameof(AuxiliarSaleDetails));
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
            }
        }

        public ObservableCollection<Sale>? ListSales
		{
			get => _ListSales;
			set
			{
				_ListSales = value;
				OnPropertyChanged(nameof(ListSales));
			}
		}

        public Sale? AuxiliarSale
		{
			get => _AuxiliarSale;
			set
			{
				_AuxiliarSale = value;
				OnPropertyChanged(nameof(AuxiliarSale));
			}
		}

        public SaleDetails? AuxiliarSaleDetails
        {
            get => _AuxiliarSaleDetails;
            set
            {
				_AuxiliarSaleDetails = value;
                OnPropertyChanged(nameof(AuxiliarSaleDetails));
            }
        }

        public ObservableCollection<SaleDetails>? ListSaleDatails
        {
            get => _ListSaleDatails;
            set
            {
                _ListSaleDatails = value;
                OnPropertyChanged(nameof(ListSaleDatails));
            }
        }

		public ObservableCollection<PaymentMethod> ListPaymentMethods
		{
			get => _ListPaymentMethods;
			set
			{
				_ListPaymentMethods = value;
				OnPropertyChanged(nameof(ListPaymentMethods));
			}
		}

		public PaymentMethod? SelectedPaymentMethod
		{
			get => _SelectedPaymentMethod;
			set
			{
				_SelectedPaymentMethod = value;
				OnPropertyChanged(nameof(SelectedPaymentMethod));
			}
		}
		#endregion

		#region Define commands
		public ICommand CmdCRUD { get; }
        public ICommand CmdAddProduct { get; }
		public ICommand CmdEdition { get; }        
        #endregion

        //Constructor method
        public SalesViewModel()
        {
			CmdCRUD = new ViewModelCommand(CRUDExecute, CRUDCanExecute);
			CmdEdition = new ViewModelCommand(EditionExecute, EditionCanExecute);
			CmdAddProduct = new ViewModelCommand(AddProductExecute);

            ProductsQuery = new SqlProductsQuery();
            ListProducts = new ObservableCollection<Product>();
            ListProducts = ProductsQuery.Read();

            CustomerQuery = new SqlCustomerQuery();
            ListCustomers = new ObservableCollection<Customer>();
            ListCustomers = CustomerQuery.Read();

			ListSaleDatails = new ObservableCollection<SaleDetails>();
            ListPaymentMethods = DataManagement.ListPaymentMethod();
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
                    SelectedProduct = null;
					Filter = string.Empty;
					Action = "Create";

					//Clear the auxiliar object, get last id in database and call property to update view
					AuxiliarSale = new Sale();
					AuxiliarSaleDetails = new SaleDetails();
                    //AuxiliarSale.Id = ProductsQuery.LastId() + 1;
                    OnPropertyChanged(nameof(AuxiliarSaleDetails));
					break;

				//Update data of register in database
				case "Update":
					EnableEdition = true;
					Filter = string.Empty;
					Action = "Update";
					break;

				//Delete register of data grid and database
				case "Delete":
					MessageBoxResult Result = MessageBox.Show($"¿Estás seguro que deseas eliminar el producto {SelectedProduct.Name}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

					if (Result == MessageBoxResult.Yes)
					{
						//ProductsQuery.Delete(SelectedProduct);
						//ListProducts.Remove(SelectedProduct);
						//AuxiliarProduct = null;
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

                /*
				//Enable buttons if there is an object selected in datagrid
				case "Update":
				case "Delete":
				case "Print":
					if (SelectedProduct != null && EnableEdition == false)
						CanExecute = true;
					else
						CanExecute = false;
					break;
                */
			}

			return CanExecute;
		}

		//Edition actions on editable object execute command
		private void EditionExecute(object Parameter)
		{
			string? ButtonName = Parameter as string;
			bool CanExecute = false;

			switch (ButtonName)
			{
				//Acept action execute
				case "Acept":
					/*
					if (Action == "Create")
					{
						ProductsQuery.Create(AuxiliarProduct);
					}
					else if (Action == "Update")
					{
						ProductsQuery.Update(AuxiliarProduct);
					}
					*/

					//ListProducts.Clear();
					//ListProducts = ProductsQuery.Read();
					EnableEdition = false;
					break;

				//Cancel action execute
				case "Cancel":
					EnableEdition = false;
					AuxiliarSale = null;
					AuxiliarSaleDetails = null;
					SelectedProduct = null;
					SelectedCustomer = null;
					SelectedPaymentMethod = null;
					Action = "None";
					break;
			}
		}

		private void AddProductExecute(object obj)
		{
			ListSaleDatails.Add(AuxiliarSaleDetails);
			AuxiliarSaleDetails = new SaleDetails();
			SelectedProduct = null;
			OnPropertyChanged(nameof(ListSaleDatails));
		}
    }
}
