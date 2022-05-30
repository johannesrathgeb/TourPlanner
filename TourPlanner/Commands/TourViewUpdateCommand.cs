using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    internal class TourViewUpdateCommand : BaseCommand
    {
        public TourPlanner.ViewModels.MainWindowVM MainWindowVM { get; set; }

        public TourViewUpdateCommand(MainWindowVM mainwindowvm)
        {
            MainWindowVM = mainwindowvm;
        }

        public override void Execute(object? parameter)
        {
            MainWindowVM.CurrentView = MainWindowVM.TourVM;
        }
    }
}
