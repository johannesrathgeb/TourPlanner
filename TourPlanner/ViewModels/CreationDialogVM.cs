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

namespace TourPlanner.ViewModels
{
    public class CreationDialogVM : ViewModelBase
    {

        public ICommand SubmitTour { get; }
        public ICommand CancelTour { get; }

        public string? NewTourName { get; set; }
        public string? NewTourDescription { get; set; }
        public string? NewTourFrom { get; set; }
        public string? NewTourTo { get; set; }
        public TransportType NewTourTransportType { get; set; }
        public string? NewTourDistance { get; set; }
        public string? NewTourEstimatedTime { get; set; }

        public CreationDialogVM(MainWindowVM viewmodel, TourPlanner.CreationDialogue creationdialog)
        {
            SubmitTour = new SubmitTourCommand(viewmodel, this, creationdialog);
            CancelTour = new CancelTourCommand(creationdialog);
        }


    }
}