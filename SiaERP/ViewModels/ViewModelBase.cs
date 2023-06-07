using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace SiaERP.ViewModels
{
	//Property change notification interface
    public abstract class ViewModelBase : INotifyPropertyChanging, INotifyPropertyChanged
    {
		public event PropertyChangingEventHandler PropertyChanging;
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanging([CallerMemberName] string PropertyName = "")
		{
			PropertyChanging(this, new PropertyChangingEventArgs(PropertyName));
		}

		protected void OnPropertyChanged([CallerMemberName] string PropertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
			}
		}
    }
}
