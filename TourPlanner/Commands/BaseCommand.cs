using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TourPlanner.Commands
{
	public abstract class BaseCommand : ICommand
    {
		protected Func<object, bool>? CanExecuteAction;

		public event EventHandler? CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		public virtual bool CanExecute(object? parameter)
		{
			return CanExecuteAction is null || CanExecute(parameter);
		}

		public abstract void Execute(object? parameter);
	}
}
