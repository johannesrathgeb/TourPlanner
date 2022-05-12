using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    internal class OpenEditLogsDialogCommand : BaseCommand
    {

        public MainWindowVM MainWindowVM { get; }

        public OpenEditLogsDialogCommand(MainWindowVM vm)
        {
            MainWindowVM = vm;
        }

        public override bool CanExecute(object? parameter)
        {
            if (MainWindowVM.SelectedTour != null && MainWindowVM.SelectedLogIndex != -1 && base.CanExecute(parameter))
            {
                return true;
            }
            return false;
        }


        public override void Execute(object? parameter)
        {
            LogsCreationDialog cd = new LogsCreationDialog(MainWindowVM, 1);
            cd.Show();
        }

    }
}
