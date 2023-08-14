using System.Collections.ObjectModel;
using SiaERP.Resources.Utilities;
using System.Reflection;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;
using SiaERP.Models;
using SiaERP.Data;
using System.Linq;
using System;

namespace SiaERP.ViewModels
{
    internal class ProductsViewModel : ViewModelBase
    {
        //Define property class
        private SqlProductsQuery ProductsQuery;
        private SqlProductCategoryQuery CategoryQuery;
        private ObservableCollection<Product>? _ListProducts;
        private ObservableCollection<ProductCategory>? _ListCategories;
        private Product? _SelectedProduct;
        private ProductCategory? _SelectedCategory;
        private Product? _AuxiliarProduct;
        private ImageSource? _ProductImage;

        private bool _EnableEdition = false;
        private string Action = "None";

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

        public ObservableCollection<ProductCategory>? ListCategories
        {
            get => _ListCategories;
            set
            {
                _ListCategories = value;
                OnPropertyChanged(nameof(ListCategories));
            }
        }

        public Product? SelectedProduct
        { 
            get => _SelectedProduct;
            set
            {
                _SelectedProduct = value;
                SelectionItemChanged();
                OnPropertyChanged(nameof(SelectedProduct));

                //Search in list categories the corresponding to the product by his id and select it
                if (SelectedProduct != null && EnableEdition == false)
                {
                    SelectedCategory = ListCategories.FirstOrDefault(p => p.Id == SelectedProduct.Category);
                }
            }
        }

        public ProductCategory? SelectedCategory
        {
            get => _SelectedCategory;
            set
            {
                _SelectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));

                //Assigns the selected category to the product currently being edited
                if (SelectedCategory != null && AuxiliarProduct != null && EnableEdition == true)
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

        public ImageSource? ProductImage
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
        #endregion

        #region Define commands
        public ICommand CmdCRUD { get; }
        public ICommand CmdEdition { get; }
        public ICommand CmdFilter { get; }
        public ICommand CmdLoadImage { get; }
        #endregion

        //Constructor method
        public ProductsViewModel()
        {
            CmdCRUD = new ViewModelCommand(CRUDExecute, CRUDCanExecute);
            CmdEdition = new ViewModelCommand(EditionExecute, EditionCanExecute);
            CmdFilter = new ViewModelCommand(FilterExecute);
            CmdLoadImage = new ViewModelCommand(LoadImageExecute, EditionCanExecute);

            ProductsQuery = new SqlProductsQuery();
            ListProducts = new ObservableCollection<Product>();
            ListProducts = ProductsQuery.Read();

            CategoryQuery = new SqlProductCategoryQuery();
            ListCategories = new ObservableCollection<ProductCategory>();
            ListCategories = CategoryQuery.Read();
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

                //Clone the object selected and assign it to currently being edited
                AuxiliarProduct = CloneObject(SelectedProduct);
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
                    SelectedCategory = null;
                    Filter = string.Empty;
                    Action = "Create";

                    //Clear the auxiliar object, get last id in database and call property to update view
                    AuxiliarProduct = CloneObject(null);
                    AuxiliarProduct.Id = ProductsQuery.LastId() + 1;
                    OnPropertyChanged(nameof(AuxiliarProduct));
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
                        ProductsQuery.Delete(SelectedProduct);
                        ListProducts.Remove(SelectedProduct);
                        AuxiliarProduct = null;
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
                case "Update": case "Delete": case "Print":
                    if (SelectedProduct != null && EnableEdition == false)
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
                        ProductsQuery.Create(AuxiliarProduct);
                    }
                    else if (Action == "Update")
                    {
                        ProductsQuery.Update(AuxiliarProduct);
                    }

                    ListProducts.Clear();
                    ListProducts = ProductsQuery.Read();
                    EnableEdition = false;
                    break;

                //Cancel action execute
                case "Cancel":
                    EnableEdition = false;
                    AuxiliarProduct = null;
                    SelectedCategory = null;
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
            ListProducts.Clear();
            ProductsQuery.Read(Filter);
        }

        //Load customer image from file explorer
        private void LoadImageExecute(object Parameter)
        {
            OpenFileDialog LoadFile = new OpenFileDialog();
            LoadFile.Filter = "Image Files (*.jpg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (LoadFile.ShowDialog() == true && AuxiliarProduct != null)
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
