using SiaERP.Models;
using System.Windows.Input;
using SiaERP.Resources.Utilities;

namespace SiaERP.ViewModels
{
    internal class MainViewModel : ViewModelBase
	{
        //Define private variables
        private User _CurrentUser;
        private ViewModelBase _CurrentView;
        private int _LateralMenuSize = 48;
        private bool EnableEdition;

        #region Property encapsulation
        public User CurrentUser 
        { 
            get => _CurrentUser;
            set
            {
                _CurrentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

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
        #endregion

        #region Define commands
        public ICommand CmdShowView { get; }
        #endregion

        //Contructor method
        public MainViewModel()
		{
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
    }
}
