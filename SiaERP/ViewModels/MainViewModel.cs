using System;
using System.Windows.Input;
using SiaERP.Resources.Utilities;

namespace SiaERP.ViewModels
{
    internal class MainViewModel : ViewModelBase
	{
        //Define private variables
        private ViewModelBase _CurrentView;
        private int _LateralMenuSize = 48;

        //Define public properties
        public ViewModelBase CurrentView 
		{ 
			get => _CurrentView;

			set
			{
				_CurrentView = value;
				OnPropertyChanged(nameof(CurrentView));
			}
		}
        public int LateralMenuSize
        {
            get => _LateralMenuSize;
            set
            {
                _LateralMenuSize = value;
                OnPropertyChanged(nameof(LateralMenuSize));
            }
        }

        //Define commands
        public ICommand CmdShowView { get; }
        public ICommand CmdExpanLateralMenu { get; }

        //Contructor method
        public MainViewModel()
		{
            CmdExpanLateralMenu = new ViewModelCommand(ExpandLateralMenuExecute);
            CmdShowView = new ViewModelCommand(ShowViewCommand);
        }

        //Show views in the interface
        private void ShowViewCommand(object Parameter)
        {
            string? ButtonName = Parameter as string;

            switch(ButtonName)
            {
                case "Customers": CurrentView = new CustomerViewModel();  break;
                case "Services": CurrentView = new ServiceViewModel(); break;
                case "Products": CurrentView = new ProductsViewModel();  break;
            }
        }

        private void ExpandLateralMenuExecute(object obj)
        {
            
        }
    }
}
