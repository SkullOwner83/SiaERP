using System.Collections.ObjectModel;
using SiaERP.Resources.Utilities;
using SiaERP.Models;
using System.Reflection;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows;
using System;
using System.Windows.Media;
using SiaERP.Data;
using System.Linq;

namespace SiaERP.ViewModels
{
    internal class ProductsViewModel : ViewModelBase
    {
        private SqlProductsQuery ProductsQuery;
        private SqlProductCategoryQuery CategoryQuery;
        private ObservableCollection<Product> _ListProducts;
        private ObservableCollection<ProductCategory> _ListCategories;
        private Product _SelectedProduct;
        private ProductCategory _SelectedCategory;
        private Product? _AuxiliarProduct;
        private ImageSource _ProductImage;
        private bool _EnableEdition = false;
        private string Action = "None";
        private string _Filter;

        #region Property encapsulation
        public ObservableCollection<Product> ListProducts
        { 
            get => _ListProducts;
            set
            {
                _ListProducts = value;
                OnPropertyChanged(nameof(ListProducts));
            }
        }

        public ObservableCollection<ProductCategory> ListCategories
        {
            get => _ListCategories;
            set
            {
                _ListCategories = value;
                OnPropertyChanged(nameof(ListCategories));
            }
        }

        public Product SelectedProduct
        { 
            get => _SelectedProduct;
            set
            {
                _SelectedProduct = value;
                SelectionItemChanged();
                OnPropertyChanged(nameof(SelectedProduct));

                if (SelectedProduct != null)
                {
                    SelectedCategory = ListCategories.FirstOrDefault(c => c.Id == SelectedProduct.Category);
                }
            }
        }

        public ProductCategory SelectedCategory
        {
            get => _SelectedCategory;
            set
            {
                _SelectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));

                if (SelectedCategory != null && AuxiliarProduct != null)
                {
                    AuxiliarProduct.Category = SelectedCategory.Id;
                }
            }
        }

        public Product? AuxiliarProduct
        {
            get => _AuxiliarProduct;
            set
            {
                _AuxiliarProduct = value;
                OnPropertyChanged(nameof(AuxiliarProduct));
            }
        }

        public ImageSource ProductImage
        {
            get => _ProductImage;
            set
            {
                _ProductImage = value;
                OnPropertyChanged(nameof(ProductImage));
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
        public ICommand CmdFilter { get; }
        public ICommand CmdLoadImage { get; }
        #endregion

        public ProductsViewModel()
        {
            CmdNew = new ViewModelCommand(CreateProductExecute, CreateProductCanExecute);
            CmdDelete = new ViewModelCommand(DeleteProductExecute, DeleteProductCanExecute);
            CmdModify = new ViewModelCommand(UpdateProductExecute, DeleteProductCanExecute);
            CmdPrint = new ViewModelCommand(UpdateProductExecute, DeleteProductCanExecute);
            CmdAcept = new ViewModelCommand(AceptActionExecute, ActionCanExecute);
            CmdCancel = new ViewModelCommand(CancelActionExecute, ActionCanExecute);
            CmdFilter = new ViewModelCommand(FilterExecute);
            CmdLoadImage = new ViewModelCommand(LoadImageExecute, ActionCanExecute);

            ProductsQuery = new SqlProductsQuery();
            ListProducts = new ObservableCollection<Product>();
            ListProducts = ProductsQuery.Read();

            CategoryQuery = new SqlProductCategoryQuery();
            ListCategories = new ObservableCollection<ProductCategory>();
            ListCategories = CategoryQuery.Read();
        }

        private void FilterExecute(object obj)
        {
            ListProducts.Clear();
            ProductsQuery.Read(Filter);
        }

        //Set properties of selected item in the form
        private void SelectionItemChanged()
        {
            if (EnableEdition == false)
            {
                if (AuxiliarProduct == null)
                {
                    AuxiliarProduct = new Product();
                }

                AuxiliarProduct = CloneObject(SelectedProduct);
            }
        }

        //Create new customer register
        private void CreateProductExecute(object obj)
        {
            EnableEdition = true;
            Filter = string.Empty;
            Action = "Create";

            //Clear the auxiliar customer, get last id in database and call property to update view
            AuxiliarProduct = CloneObject(null);
            AuxiliarProduct.Id = ProductsQuery.LastId() + 1;
            OnPropertyChanged(nameof(AuxiliarProduct));
        }

        //Update data of customer in database
        private void UpdateProductExecute(object obj)
        {
            EnableEdition = true;
            Filter = string.Empty;
            Action = "Update";
        }

        //Delete customer of data grid and data base
        private void DeleteProductExecute(object obj)
        {
            MessageBoxResult Result = MessageBox.Show($"¿Estás seguro que deseas eliminar el producto {SelectedProduct.Name}?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (Result == MessageBoxResult.Yes)
            {
                ProductsQuery.Delete(SelectedProduct);
                ListProducts.Remove(SelectedProduct);
                AuxiliarProduct = null;
            }
        }

        private void AceptActionExecute(object obj)
        {
            if (Action == "Create")
            {
                ProductsQuery.Create(AuxiliarProduct);
            }
            else if (Action == "Update")
            {
                ProductsQuery.Update(AuxiliarProduct);
            }

            ListProducts.Clear();
            ListProducts = ProductsQuery.Read();
            EnableEdition = false;
        }

        private void CancelActionExecute(object obj)
        {
            EnableEdition = false;
            AuxiliarProduct = null;
            Action = "None";
        }

        private bool CreateProductCanExecute(object obj)
        {
            if (EnableEdition == false)
                return true;
            else
                return false;
        }

        private bool DeleteProductCanExecute(object obj)
        {
            if (SelectedProduct != null && EnableEdition == false)
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

        //Load customer image from file explorer
        private void LoadImageExecute(object obj)
        {
            OpenFileDialog LoadFile = new OpenFileDialog();
            LoadFile.Filter = "Image Files (*.jpg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (LoadFile.ShowDialog() == true)
            {
                AuxiliarProduct.Image = new BitmapImage(new Uri(LoadFile.FileName));
                OnPropertyChanged(nameof(AuxiliarProduct));
            }
        }

        //Clone properties from one instance to another instance
        public Product CloneObject(Product? Source)
        {
            //Create new instances of source and destination, if the parameter is null
            using (Product ObjectSource = Source ?? new Product())
            {
                using (Product ObejctDestination = new Product())
                {
                    //Get all properties of object and set the values to the clone
                    PropertyInfo[] Properties = typeof(Product).GetProperties();

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
