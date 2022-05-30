using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    public class OpenLogsDialogCommand : BaseCommand
    {
        public TourViewVM TourViewVM { get; }

        public OpenLogsDialogCommand(TourViewVM vm)
        {
            TourViewVM = vm;
        }

        public override bool CanExecute(object? parameter)
        {
            if (TourViewVM.SelectedTour != null && base.CanExecute(parameter))
            {
                return true;
            }
            return false;
        }
        public override void Execute(object? parameter)
        {
            LogsCreationDialog cd = new LogsCreationDialog(TourViewVM, 0);
            cd.ShowDialog();
        }

    }
}
