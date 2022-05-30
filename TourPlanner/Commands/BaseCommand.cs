using System;
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
