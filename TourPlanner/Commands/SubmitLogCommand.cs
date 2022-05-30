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
        public TourViewVM TourViewVM { get; set; }
        public LogsCreationDialogVM LogsCreationDialogVM { get; set; }
        public TourPlanner.LogsCreationDialog LogsCreationDialog { get; set; }
        public int Mode { get; set; }

        public SubmitLogCommand(TourViewVM tourwindowvm, LogsCreationDialogVM logscreationdialogvm, TourPlanner.LogsCreationDialog logscreationdialog, int mode)
        {
            TourViewVM = tourwindowvm;
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

            string logDate = LogsCreationDialogVM.NewLogDate;
            string[] textSplit = logDate.Split(" ");
            logDate = textSplit[0];
            var dateFormats = new[] { "dd/MM/yyyy", "d/MM/yyyy", "dd/M/yyyy", "d/M/yyyy"};
            if (!DateTime.TryParseExact(logDate, dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out s))
            {
                MessageBox.Show("Please enter a valid date! [dd/MM/yyyy]", "Invalid Date Format", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Mode == 0)
            {
                tourlog = TourFactory.GetInstance().AddTourlogToDB(TourViewVM.SelectedTour.Id, logDate, LogsCreationDialogVM.NewLogComment, LogsCreationDialogVM.NewLogDifficulty + 1, LogsCreationDialogVM.NewLogTotaltime, LogsCreationDialogVM.NewLogRating + 1);
                TourViewVM.SelectedTour.Tourlogs.Add(tourlog);
            } else
            {
                tourlog = new Tourlog(TourViewVM.SelectedTour.Tourlogs[TourViewVM.SelectedLogIndex].Id, logDate, LogsCreationDialogVM.NewLogComment, LogsCreationDialogVM.NewLogDifficulty + 1, LogsCreationDialogVM.NewLogTotaltime, LogsCreationDialogVM.NewLogRating + 1);
                tourlog = TourFactory.GetInstance().UpdateTourlogInDB(tourlog);
                TourViewVM.SelectedTour.Tourlogs[TourViewVM.SelectedLogIndex] = tourlog; 
            }

            TourViewVM.SelectedTour.Popularity = cc.CalcPopularity(TourViewVM.SelectedTour);
            TourViewVM.SelectedTour.ChildFriendliness = cc.CalcChildFriendliness(TourViewVM.SelectedTour);

            LogsCreationDialog.Close();
        }
    }
}
