using System;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Input;
using SiaERP.Views;
using SiaERP.Data;
using System.Net;
using System.Media;
using MySql.Data.MySqlClient;
using SiaERP.Resources.Utilities;

namespace SiaERP.ViewModels
{
    internal class LogInViewModel : ViewModelBase
	{
		//Define private variables
		private string _UserName = "";
		private string _Password = "";
		private string DatabaseLocation;
		private SqlUserQuery Querys;
		private CsvUserQuery CsvQuerys;
		private bool ValidedUser;

		//Define public properties
		#region
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
		#endregion

		//Define commands
		public ICommand CmdLogIn { get; }
		public ICommand CmdCancel { get; }
		public ICommand CmdEnterKey { get; }

		//Contructor method 
		public LogInViewModel()
		{
			//Instantiate commands
			Querys = new SqlUserQuery();
			CsvQuerys = new CsvUserQuery();
			CmdLogIn = new ViewModelCommand(LogInExecute, LogInCanExecute);
			CmdCancel = new ViewModelCommand(CancelExecute);
			CmdEnterKey = new ViewModelCommand(EnterKeyExecute);
		}

		private bool LogInCanExecute(object obj)
		{
			if (string.IsNullOrEmpty(UserName) || UserName.Length < 3 ||
				Password == null || Password.Length < 3)
				return false;
			else
				return true;
		}

		//Validate credentials in database for login 
		private void LogInExecute(object obj)
		{
			//Check location of database for query	
			if (DatabaseLocation == "MySql")
			{
				ValidedUser = Querys.AuthenticateUser(new NetworkCredential(UserName.Trim(), Password));
			}
			else
			{

				ValidedUser = CsvQuerys.AuthenticateUser(new NetworkCredential(UserName.Trim(), Password));
			}

			//Open the main window if user is validated
			if (ValidedUser == true)
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

		//Cancel and application exit
		private void CancelExecute(object obj)
		{
			SystemSounds.Exclamation.Play();
			Application.Current.Shutdown();
		}

		private void EnterKeyExecute(object obj)
		{
			TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
			UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

			if (elementWithFocus != null)
			{
				elementWithFocus.MoveFocus(request);
			}
			else
			{
				MessageBox.Show("Hola");
			}
		}
	}
}
