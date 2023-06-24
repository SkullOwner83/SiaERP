using SiaERP.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiaERP.Resources.Utilities;
using SiaERP.Views;
using Models;

namespace SiaERP.ViewModels
{
    internal class MainViewModel : ViewModelBase
	{
        //Define private variables
        private ViewModelBase _CurrentView;

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

        //Define commands
        public ICommand CmdShowCustomersView { get; }

        //Contructor method
        public MainViewModel()
		{
			CmdShowCustomersView = new ViewModelCommand(ShowCustomersViewExecuteCommand);
        }

        private void ShowCustomersViewExecuteCommand(object obj) => CurrentView = new CustomerViewModel();
    }
}
