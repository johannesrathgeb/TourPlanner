namespace TourPlanner.Commands
{
    public class CancelTourCommand : BaseCommand
    {
        public TourPlanner.CreationDialogue CreationDialog { get; set; }

        public CancelTourCommand(TourPlanner.CreationDialogue creationdialog)
        {
            CreationDialog = creationdialog;
        }

        public override void Execute(object? parameter)
        {
            CreationDialog.Close(); 
        }
    }
}
