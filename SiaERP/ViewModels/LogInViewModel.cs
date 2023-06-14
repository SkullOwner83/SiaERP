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

namespace SiaERP.ViewModels
{
	internal class LogInViewModel : ViewModelBase
	{
		//Define private variables
		private string _UserName = "";
		private string _Password = "";
		private string DatabaseLocation;
		private SQLUserQuery Querys;
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

		//Contructor method 
		public LogInViewModel()
		{
			//Instantiate commands
			Querys = new SQLUserQuery();
			CsvQuerys = new CsvUserQuery();
			CmdLogIn = new ViewModelCommand(LogInExecuteCommand, LogInCanExecuteCommand);
			CmdCancel = new ViewModelCommand(CancelExecuteCommand);

			//Check if can establish connection with database
			SqlDatabaseConnection Connection = new SqlDatabaseConnection();
			DatabaseLocation = Connection.GetConnection() != null ? "MySql" : "Local";
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
			//Validate credentials in database location for login 
			if (DatabaseLocation == "MySql")
			{
				ValidedUser = Querys.AuthenticateUser(new NetworkCredential(UserName, Password));
			}
			else
			{

				ValidedUser = CsvQuerys.AuthenticateUser(new NetworkCredential(UserName, Password));
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

		private void CancelExecuteCommand(object obj)
		{
			SystemSounds.Exclamation.Play();
			Application.Current.Shutdown();
		}
	}
}
