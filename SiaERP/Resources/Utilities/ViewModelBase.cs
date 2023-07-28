using System.ComponentModel;

namespace SiaERP.Resources.Utilities
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private bool _TabControlCollapsed = false;
        private int _TabControlColumn = 3;

        public int TabControlColumn
        {
            get => _TabControlColumn;
            set
            {
                _TabControlColumn = value;
                OnPropertyChanged(nameof(TabControlColumn));
            }
        }

        public bool TabControlCollapsed
        {
            get => _TabControlCollapsed;
            set
            {
                _TabControlCollapsed = value;
                OnPropertyChanged(nameof(TabControlCollapsed));
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
