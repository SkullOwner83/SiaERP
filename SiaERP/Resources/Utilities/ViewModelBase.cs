using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace SiaERP.Resources.Utilities
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Define property class
        public event PropertyChangedEventHandler? PropertyChanged;
        private double _TabControlWidth = 400;
        private string? _Filter;
        private bool _EnableEdition = false;
        public string Action = "None";
        #endregion

        #region Property encapsulation
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

        public bool EnableEdition
        {
            get => _EnableEdition;
            set
            {
                _EnableEdition = value;
                OnPropertyChanged(nameof(EnableEdition));
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

        //Edition actions on editable object  can execute command
        public bool EditionCanExecute(object Parameter)
        {
            if (EnableEdition == true)
                return true;
            else
                return false;
        }

        //Clone properties from one instance to another instance
        public T CloneObject<T>(T Source) where T : new()
        {
            T Destination = new T();

            PropertyInfo[] Properties = typeof(T).GetProperties();

            foreach (PropertyInfo Property in Properties)
            {
                if (Property.CanWrite)
                {
                    object? value = Property.GetValue(Source);
                    Property.SetValue(Destination, value);
                }
            }

            return Destination;
        }

        //Generate event when one property is changed for update the view
        protected virtual void OnPropertyChanged(string PropertyName)
        {
            //Call property changed event of this class to inherited class
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
