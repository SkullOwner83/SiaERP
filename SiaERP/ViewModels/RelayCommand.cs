using System;
using System.Windows.Input;

namespace SiaERP.ViewModels
{
	internal class RelayCommand : ICommand
	{
		//Define delegates from store mehtods
		private readonly Action<object> _execute;
		private readonly Predicate<object> _canExecute;

		//Set the delegates parameters of constructor to delegates properties of class
		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		//Call first constructor with execute parameter only
		public RelayCommand(Action<object> execute) : this(execute, null)
		{
			
		}

		//Return if can execute from delegate result
		public bool CanExecute(object? parameter)
		{
			return _canExecute == null ? true : _canExecute(parameter);
		}

		//Execute method of constructor from delegates
		public void Execute(object? parameter)
		{
			_execute(parameter);
		}

		//Manage commands when occur that affect whether or not the command should execute
		public event EventHandler? CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}
	}
}
