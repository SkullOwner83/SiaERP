using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SiaERP.Views;

namespace SiaERP
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		//Overwrite start up method for open window in a personalized way
		protected override void OnStartup(StartupEventArgs e)
		{
			MainWindow = new Main();
			MainWindow.Show();
			base.OnStartup(e);
		}
	}
}
