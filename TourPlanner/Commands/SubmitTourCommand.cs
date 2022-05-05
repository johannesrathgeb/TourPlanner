using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.ViewModels;
using TourPlanner.Models;
using TourPlanner.BusinessLayer;
using System.Windows; 

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


        public override async void Execute(object? parameter)
        {
            ProcessRestData prd = new ProcessRestData();
            
            Tour tour = new Tour(CreationDialogVM.NewTourName, CreationDialogVM.NewTourDescription, CreationDialogVM.NewTourFrom, CreationDialogVM.NewTourTo, CreationDialogVM.NewTourTransportType);

            tour = await prd.HandleMapRequest(tour);

            if(tour == null)
            {
                MessageBox.Show("At least one location does not exist or you entered the same location twice!","Invalid Location(s)",MessageBoxButton.OK,MessageBoxImage.Warning);
            } else
            {
                MainWindowVM.AddTour(tour);
                CreationDialog.Close();
            }
        }
    }
}