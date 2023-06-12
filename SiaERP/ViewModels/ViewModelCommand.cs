using System;
using System.Windows.Input;

namespace SiaERP.ViewModels
{
	internal class ViewModelCommand : ICommand
	{
		//Define delegates from store mehtods
		private readonly Action<object> _execute;
		private readonly Predicate<object> _canExecute;

		//Set the delegates parameters of constructor to delegates properties of class
		public ViewModelCommand(Action<object> execute, Predicate<object> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		//Call first constructor with execute parameter only
		public ViewModelCommand(Action<object> execute) : this(execute, null)
		{
			
		}

		//Enabling or disabling if can execute their comman and delegate in view model
		public bool CanExecute(object? parameter)
		{
			return _canExecute == null ? true : _canExecute(parameter);
		}

		//Execute command and delegate the method in view model
		public void Execute(object? parameter)
		{
			_execute(parameter);
		}

		//Manage commands when occur that affect whether or not the command should execute
		public event EventHandler? CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
	}
}
