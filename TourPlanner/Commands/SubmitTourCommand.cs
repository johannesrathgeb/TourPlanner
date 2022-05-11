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
        public int Mode { get; set; }

        public SubmitTourCommand(MainWindowVM mainwindowvm, CreationDialogVM creationdialogvm, TourPlanner.CreationDialogue creationdialog, int mode)
        {
            MainWindowVM = mainwindowvm;
            CreationDialogVM = creationdialogvm;
            CreationDialog = creationdialog;
            Mode = mode;
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
            Tour tour; 

            if(Mode == 1)
            {
                tour = new Tour(CreationDialogVM.NewTourId, CreationDialogVM.NewTourName, CreationDialogVM.NewTourDescription, CreationDialogVM.NewTourFrom, CreationDialogVM.NewTourTo, CreationDialogVM.NewTourTransportType);
            } else
            {
                tour = new Tour(CreationDialogVM.NewTourName, CreationDialogVM.NewTourDescription, CreationDialogVM.NewTourFrom, CreationDialogVM.NewTourTo, CreationDialogVM.NewTourTransportType);
            }

            tour = await prd.HandleMapRequest(tour, Mode);

            if(tour == null)
            {
                MessageBox.Show("At least one location does not exist or you entered the same location twice!","Invalid Location(s)",MessageBoxButton.OK,MessageBoxImage.Warning);
            } else
            {
                if(Mode == 1)
                {
                    MainWindowVM.Tours[MainWindowVM.SelectedIndex] = tour; 
                } else
                {
                MainWindowVM.AddTour(tour);
                }
                CreationDialog.Close();
            }
        }
    }
}