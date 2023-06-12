using System.Windows;
using System.Windows.Controls;
using SiaERP.ViewModels;

namespace SiaERP.Views
{
	/// <summary>
	/// Lógica de interacción para LogInView.xaml
	/// </summary>
	public partial class LogInView : Window
	{
		public LogInView()
		{
			InitializeComponent();
		}

		private void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			DragMove();
        }
    }
}
