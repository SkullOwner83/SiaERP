using System;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Input;
using SiaERP.Views;
using SiaERP.Data;
using System.Net;

namespace SiaERP.ViewModels
{
	internal class LogInViewModel : ViewModelBase
	{
		//Define private variables
		private string _UserName;
		private string _Password;
		private SQLUserQuery Querys;

		//Define public properties
		public string UserName
		{ 
			get => _UserName; 
			set
			{
				_UserName = value;
				OnPropertyChanged(nameof(UserName));
			}
		}

		public string Password 
		{ 
			get => _Password; 
			set
			{
				_Password = value;
				OnPropertyChanged(nameof(Password));
			}
		}

		//Define commands
		public ICommand LogInCommand { get; }

		//Contructor method 
		public LogInViewModel()
		{
			//Instantiate commands
			Querys = new SQLUserQuery();
			LogInCommand = new ViewModelCommand(LogInExecuteCommand, LogInCanExecuteCommand);
		}

		private bool LogInCanExecuteCommand(object obj)
		{
			if (string.IsNullOrEmpty(UserName) || UserName.Length < 3 ||
				Password == null || Password.Length < 3)
				return false;
			else
				return true;
		}

		private void LogInExecuteCommand(object obj)
		{
			bool ValidUser = Querys.AuthenticateUser(new NetworkCredential(UserName, Password));

			if (ValidUser == true)
			{
				var CurrentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
				var NewWindow = new Main();
				NewWindow.Show();

				if (CurrentWindow != null)
				{
					CurrentWindow.Close();
				}
			}
			else
			{
				MessageBox.Show("Error! Nombre de usuario o contraseña invalidos.");
			}
		}
	}
}
