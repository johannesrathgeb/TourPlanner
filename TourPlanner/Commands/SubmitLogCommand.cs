using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    public class SubmitLogCommand : BaseCommand
    {
        public MainWindowVM MainWindowVM { get; set; }
        public LogsCreationDialogVM LogsCreationDialogVM { get; set; }
        public TourPlanner.LogsCreationDialog LogsCreationDialog { get; set; }
        public int Mode { get; set; }

        public SubmitLogCommand(MainWindowVM mainwindowvm, LogsCreationDialogVM logscreationdialogvm, TourPlanner.LogsCreationDialog logscreationdialog, int mode)
        {
            MainWindowVM = mainwindowvm;
            LogsCreationDialogVM = logscreationdialogvm;
            LogsCreationDialog = logscreationdialog;
            Mode = mode;
        }

        public override bool CanExecute(object? parameter)
        {
            if (!string.IsNullOrEmpty(LogsCreationDialogVM.NewLogDate) &&
                !string.IsNullOrEmpty(LogsCreationDialogVM.NewLogComment) &&
                !string.IsNullOrEmpty(LogsCreationDialogVM.NewLogTotaltime) &&
                base.CanExecute(parameter))
            {
                return true;
            }
            return false;
        }

        public override void Execute(object? parameter)
        {
            Tourlog tourlog; 

            if(Mode == 0)
            {
                tourlog = TourFactory.GetInstance().AddTourlogToDB(MainWindowVM.SelectedTour.Id, LogsCreationDialogVM.NewLogDate, LogsCreationDialogVM.NewLogComment, LogsCreationDialogVM.NewLogDifficulty + 1, LogsCreationDialogVM.NewLogTotaltime, LogsCreationDialogVM.NewLogRating + 1);
                MainWindowVM.SelectedTour.Tourlogs.Add(tourlog);
            } else
            {
                tourlog = new Tourlog(MainWindowVM.SelectedTour.Tourlogs[MainWindowVM.SelectedLogIndex].Id, LogsCreationDialogVM.NewLogDate, LogsCreationDialogVM.NewLogComment, LogsCreationDialogVM.NewLogDifficulty + 1, LogsCreationDialogVM.NewLogTotaltime, LogsCreationDialogVM.NewLogRating + 1);
                tourlog = TourFactory.GetInstance().UpdateTourlogInDB(tourlog);
                MainWindowVM.SelectedTour.Tourlogs[MainWindowVM.SelectedLogIndex] = tourlog; 
            }
            LogsCreationDialog.Close();
        }
    }
}
