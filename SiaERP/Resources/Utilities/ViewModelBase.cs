using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SiaERP.Resources.Utilities
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        //Define property class
        public event PropertyChangedEventHandler? PropertyChanged;
        private bool _TabControlCollapsed = false;
        private double _TabControlWidth = 400;
        private string? _Filter;

        #region Property encapsulation
        public bool TabControlCollapsed
        {
            get => _TabControlCollapsed;
            set
            {
                _TabControlCollapsed = value;
                OnPropertyChanged(nameof(TabControlCollapsed));
            }
        }

        public double TabControlWidth
        {
            get => _TabControlWidth;
            set
            {
                _TabControlWidth = value;
                OnPropertyChanged(nameof(TabControlWidth));
            }
        }

        public string? Filter
        {
            get => _Filter;
            set
            {
                _Filter = value;
                OnPropertyChanged(nameof(Filter));
            }
        }
        #endregion

        //Define commands
        public ICommand CmdClearFilter { get; }

        //Constructor method
        public ViewModelBase()
        {
            CmdClearFilter = new ViewModelCommand(ClearFilter);
        }

        //Clear text of filter
        public void ClearFilter(object Parameter)
        {
            Filter = string.Empty;
        }

        //Generate event when one property is changed for
        protected virtual void OnPropertyChanged(string PropertyName)
        {
            //Call property changed event of this class to inherited class
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
