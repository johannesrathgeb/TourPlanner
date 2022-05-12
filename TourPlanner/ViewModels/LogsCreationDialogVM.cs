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
        public string NewLogDate { get; set; } = String.Empty;
        public string NewLogComment { get; set; } = String.Empty;
        public int NewLogDifficulty { get; set; }
        public string NewLogTotaltime { get; set; } = String.Empty;
        public int NewLogRating { get; set; }
        


        public LogsCreationDialogVM(MainWindowVM viewmodel, TourPlanner.LogsCreationDialog logscreationdialog, int mode)
        {
            Mode = mode; 
            SubmitLog = new SubmitLogCommand(viewmodel, this, logscreationdialog, Mode);
        }


    }
}
