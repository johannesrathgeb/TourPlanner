using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.ViewModels;
using TourPlanner.Models;

namespace TourPlanner.Commands
{
    public class SubmitTourCommand : BaseCommand
    {
        public MainWindowVM MainWindowVM { get; set; }
        public CreationDialogVM CreationDialogVM { get; set; }
        public TourPlanner.CreationDialogue CreationDialog { get; set; }

        public SubmitTourCommand(MainWindowVM mainwindowvm, CreationDialogVM creationdialogvm, TourPlanner.CreationDialogue creationdialog)
        {
            MainWindowVM = mainwindowvm;
            CreationDialogVM = creationdialogvm;
            CreationDialog = creationdialog;
        }

        public override bool CanExecute(object? parameter)
        {
            if (!string.IsNullOrEmpty(CreationDialogVM.NewTourName) &&
                !string.IsNullOrEmpty(CreationDialogVM.NewTourDescription) &&
                !string.IsNullOrEmpty(CreationDialogVM.NewTourFrom) &&
                !string.IsNullOrEmpty(CreationDialogVM.NewTourTo) &&
                base.CanExecute(parameter))
            {
                return true;
            }
            return false; 
        }


        public override void Execute(object? parameter)
        {
            //Tour tour = new Tour(MainWindowVM.NewTour.Name,7, MainWindowVM.NewTour.Dummy);
            Tour tour = new Tour(CreationDialogVM.NewTourName, 7, CreationDialogVM.NewTourDescription, CreationDialogVM.NewTourFrom, CreationDialogVM.NewTourTo, CreationDialogVM.NewTourTransportType ,CreationDialogVM.NewTourDistance, CreationDialogVM.NewTourEstimatedTime);
            MainWindowVM.AddTour(tour);
            CreationDialog.Close();
            
        }
    }
}
