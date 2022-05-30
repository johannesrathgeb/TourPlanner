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
            cd.ShowDialog();
        }


    }
}
