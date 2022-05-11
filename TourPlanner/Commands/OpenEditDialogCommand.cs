using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    internal class OpenEditDialogCommand : BaseCommand
    {
        public MainWindowVM MainWindowVM { get; }

        public OpenEditDialogCommand(MainWindowVM vm)
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
            CreationDialogue cd = new CreationDialogue(MainWindowVM, 1);
            cd.Show();
        }

    }
}
