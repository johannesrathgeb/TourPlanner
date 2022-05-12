using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    public class OpenLogsDialogCommand : BaseCommand
    {
        public MainWindowVM MainWindowVM { get; }

        public OpenLogsDialogCommand(MainWindowVM vm)
        {
            MainWindowVM = vm;
        }

        public override bool CanExecute(object? parameter)
        {
            if (MainWindowVM.SelectedTour != null && base.CanExecute(parameter))
            {
                return true;
            }
            return false;
        }
        public override void Execute(object? parameter)
        {
            LogsCreationDialog cd = new LogsCreationDialog(MainWindowVM, 0);
            cd.Show();
        }

    }
}
