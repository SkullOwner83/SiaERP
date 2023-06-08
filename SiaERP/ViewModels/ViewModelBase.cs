using System.ComponentModel;

namespace SiaERP.ViewModels
{
	internal class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		//Notify to user when one property is changed
		protected virtual void OnPropertyChanged(string PropertyName)
		{
			//Call property changed event of this class to inherited class
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}
	}
}
