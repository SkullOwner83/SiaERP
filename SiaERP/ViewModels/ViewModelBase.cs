using System.ComponentModel;

namespace SiaERP.ViewModels
{
	internal abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		//Generate event when one property is changed for
		protected virtual void OnPropertyChanged(string PropertyName)
		{
			//Call property changed event of this class to inherited class
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
		}
	}
}
