namespace TourPlanner.Commands
{
    public class CancelLogCommand : BaseCommand
    {
        public TourPlanner.LogsCreationDialog LogsCreationDialog { get; set; }

        public CancelLogCommand(TourPlanner.LogsCreationDialog logscreationdialog)
        {
            LogsCreationDialog = logscreationdialog;
        }

        public override void Execute(object? parameter)
        {
            LogsCreationDialog.Close(); 
        }
    }
}
