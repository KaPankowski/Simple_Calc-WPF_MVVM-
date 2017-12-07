using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kalkulator_MVVM
{
	class RelayCommand : ICommand
	{
		readonly Action<object> _execute;
		readonly Predicate<object> _canExecute;
		public event EventHandler CanExecuteChanged
		{
			add
			{
				if (_canExecute != null) CommandManager.RequerySuggested += value;
			}
			remove
			{
				if (_canExecute != null) CommandManager.RequerySuggested -= value;
			}
		}

		public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
		{
			if (execute == null) throw new ArgumentNullException("execute");
			_execute = execute;
			_canExecute = canExecute;
		}

		[DebuggerStepThrough]
		public bool CanExecute(object parameter)
		{
			return _canExecute == null ? true : _canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			_execute(parameter);
		}
	}
}
