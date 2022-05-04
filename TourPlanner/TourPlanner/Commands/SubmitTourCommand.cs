using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.ViewModels;
using TourPlanner.Models;
using TourPlanner.DataAccessLayer;
using TourPlanner.BusinessLayer; 

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
            Database db = Database.getInstance();
            RESTRequest restrequest = new RESTRequest();

            bool result = await restrequest.DirectionsRequest(CreationDialogVM.NewTourFrom, CreationDialogVM.NewTourTo);

            var tour = TourFactory.GetInstance().AddTourToDB(CreationDialogVM.NewTourName, CreationDialogVM.NewTourDescription, CreationDialogVM.NewTourFrom, CreationDialogVM.NewTourTo, CreationDialogVM.NewTourTransportType, restrequest.Distance, restrequest.Time); 

            MainWindowVM.AddTour(tour);

            result = await restrequest.StaticmapRequest(tour.Id);

            CreationDialog.Close();
            
        }

    }
}
