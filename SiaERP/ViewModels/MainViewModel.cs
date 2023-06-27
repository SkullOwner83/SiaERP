using SiaERP.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiaERP.Resources.Utilities;
using SiaERP.Views;
using Models;
using System.Security.Cryptography.X509Certificates;

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
        public ICommand CmdShowCustomersView { get; }
        public ICommand CmdExpanLateralMenu { get; }

        //Contructor method
        public MainViewModel()
		{
			CmdShowCustomersView = new ViewModelCommand(ShowCustomersViewExecute);
            CmdExpanLateralMenu = new ViewModelCommand(ExpandLateralMenuExecute);
        }

        private void ExpandLateralMenuExecute(object obj)
        {
            if (LateralMenuSize == 48)
                LateralMenuSize = 200;
            else
                LateralMenuSize = 48;
        }

        private void ShowCustomersViewExecute(object obj) => CurrentView = new CustomerViewModel();
    }
}
