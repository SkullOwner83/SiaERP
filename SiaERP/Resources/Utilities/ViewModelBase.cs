using System.ComponentModel;
using System.Windows;

namespace SiaERP.Resources.Utilities
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private bool _TabControlCollapsed = false;
        private int _TabControlWidth = 400;

        public bool TabControlCollapsed
        {
            get => _TabControlCollapsed;
            set
            {
                _TabControlCollapsed = value;
                OnPropertyChanged(nameof(TabControlCollapsed));
            }
        }

        public int TabControlWidth
        {
            get => _TabControlWidth;
            set
            {
                _TabControlWidth = value;
                OnPropertyChanged(nameof(TabControlWidth));
            }
        }

        //Generate event when one property is changed for
        protected virtual void OnPropertyChanged(string PropertyName)
        {
            //Call property changed event of this class to inherited class
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
