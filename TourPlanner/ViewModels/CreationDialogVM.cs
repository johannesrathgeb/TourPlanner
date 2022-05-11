using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Commands;
using TourPlanner.Models;
using TourPlanner.ViewModels; 

namespace TourPlanner.ViewModels
{
    public class CreationDialogVM : ViewModelBase
    {
        public int Mode { get; set; }   
        public ICommand SubmitTour { get; }
        public ICommand CancelTour { get; }
        public int NewTourId { get; set; }  
        public string? NewTourName { get; set; }
        public string? NewTourDescription { get; set; }
        public string? NewTourFrom { get; set; }
        public string? NewTourTo { get; set; }
        public TransportType NewTourTransportType { get; set; }

        public CreationDialogVM(MainWindowVM viewmodel, TourPlanner.CreationDialogue creationdialog, int mode)
        {
            Mode = mode; 
            SubmitTour = new SubmitTourCommand(viewmodel, this, creationdialog, Mode);
            CancelTour = new CancelTourCommand(creationdialog);

            if(mode == 1)
            {
                NewTourId = viewmodel.SelectedTour.Id; 
                NewTourName = viewmodel.SelectedTour.Name; 
                NewTourDescription = viewmodel.SelectedTour.TourDescription; 
                NewTourFrom = viewmodel.SelectedTour.From; 
                NewTourTo = viewmodel.SelectedTour.To; 
                NewTourTransportType = viewmodel.SelectedTour.TransportType; 
            }
        }
    }
}