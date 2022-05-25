using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            DateTime s;
            CalcComputedAttributes cc = new CalcComputedAttributes(); 
            Tourlog tourlog; 

            if (!int.TryParse(LogsCreationDialogVM.NewLogTotaltime, out _))
            {
                MessageBox.Show("Enter a valid number in the [Total Time] field", "Not a number", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }
            //var dateFormats = new[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy", "dd.M.yyyy", "dd-M.yyyy", "dd/M/yyyy", "d.MM.yyyy", "d-MM-yyyy", "d/MM/YYYY", "d.M.yyyy", "d-M-yyyy", "d/M/yyyy" };
            if (!DateTime.TryParseExact(LogsCreationDialogVM.NewLogDate,"dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out s))
            {
                MessageBox.Show("Please enter a valid date! [dd.MM.yyyy]", "Invalid Date Format", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Mode == 0)
            {
                tourlog = TourFactory.GetInstance().AddTourlogToDB(MainWindowVM.SelectedTour.Id, LogsCreationDialogVM.NewLogDate, LogsCreationDialogVM.NewLogComment, LogsCreationDialogVM.NewLogDifficulty + 1, LogsCreationDialogVM.NewLogTotaltime, LogsCreationDialogVM.NewLogRating + 1);
                MainWindowVM.SelectedTour.Tourlogs.Add(tourlog);
            } else
            {
                tourlog = new Tourlog(MainWindowVM.SelectedTour.Tourlogs[MainWindowVM.SelectedLogIndex].Id, LogsCreationDialogVM.NewLogDate, LogsCreationDialogVM.NewLogComment, LogsCreationDialogVM.NewLogDifficulty + 1, LogsCreationDialogVM.NewLogTotaltime, LogsCreationDialogVM.NewLogRating + 1);
                tourlog = TourFactory.GetInstance().UpdateTourlogInDB(tourlog);
                MainWindowVM.SelectedTour.Tourlogs[MainWindowVM.SelectedLogIndex] = tourlog; 
            }

            MainWindowVM.SelectedTour.Popularity = cc.CalcPopularity(MainWindowVM.SelectedTour);
            MainWindowVM.SelectedTour.ChildFriendliness = cc.CalcChildFriendliness(MainWindowVM.SelectedTour);

            LogsCreationDialog.Close();
        }
    }
}
