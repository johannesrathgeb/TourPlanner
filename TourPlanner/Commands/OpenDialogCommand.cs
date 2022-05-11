using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    public class OpenDialogCommand : BaseCommand
    {
        public MainWindowVM MainWindowVM { get; }

        public OpenDialogCommand(MainWindowVM vm)
        {
            MainWindowVM = vm;
        }

        public override void Execute(object? parameter)
        {
            CreationDialogue cd = new CreationDialogue(MainWindowVM, 0); 
            cd.Show();
        }


    }
}
