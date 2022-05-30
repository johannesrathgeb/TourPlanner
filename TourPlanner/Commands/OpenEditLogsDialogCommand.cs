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

        public TourViewVM TourViewVM { get; }

        public OpenEditLogsDialogCommand(TourViewVM vm)
        {
            TourViewVM = vm;
        }

        public override bool CanExecute(object? parameter)
        {
            if (TourViewVM.SelectedTour != null && TourViewVM.SelectedLogIndex != -1 && base.CanExecute(parameter))
            {
                return true;
            }
            return false;
        }


        public override void Execute(object? parameter)
        {
            LogsCreationDialog cd = new LogsCreationDialog(TourViewVM, 1);
            cd.Show();
        }

    }
}
