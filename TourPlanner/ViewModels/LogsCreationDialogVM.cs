using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Commands;

namespace TourPlanner.ViewModels
{
    public class LogsCreationDialogVM : ViewModelBase
    {
        public int Mode { get; set; }
        public ICommand SubmitLog { get; }
        public ICommand CancelLog { get; }
        public int NewTourlogID { get; set; }
        public string NewLogDate { get; set; } = String.Empty;
        public string NewLogComment { get; set; } = String.Empty;
        public int NewLogDifficulty { get; set; }
        public string NewLogTotaltime { get; set; } = String.Empty;
        public int NewLogRating { get; set; }

        public LogsCreationDialogVM(MainWindowVM viewmodel, TourPlanner.LogsCreationDialog logscreationdialog, int mode)
        {
            Mode = mode; 
            SubmitLog = new SubmitLogCommand(viewmodel, this, logscreationdialog, Mode);
            CancelLog = new CancelLogCommand(logscreationdialog);

            if (Mode == 1)
            {
                NewTourlogID = viewmodel.SelectedTour.Tourlogs[viewmodel.SelectedLogIndex].Id; 
                NewLogDate = viewmodel.SelectedTour.Tourlogs[viewmodel.SelectedLogIndex].Date; 
                NewLogComment = viewmodel.SelectedTour.Tourlogs[viewmodel.SelectedLogIndex].Comment; 
                NewLogDifficulty = viewmodel.SelectedTour.Tourlogs[viewmodel.SelectedLogIndex].Difficulty; 
                NewLogTotaltime = viewmodel.SelectedTour.Tourlogs[viewmodel.SelectedLogIndex].Totaltime; 
                NewLogRating = viewmodel.SelectedTour.Tourlogs[viewmodel.SelectedLogIndex].Rating; 
            }

        }


    }
}
