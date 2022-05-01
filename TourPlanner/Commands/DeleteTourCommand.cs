using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using TourPlanner.ViewModels; 

namespace TourPlanner.Commands
{
    public class DeleteTourCommand : BaseCommand
    {

        public MainWindowVM MainWindowVM { get; set; }

        public DeleteTourCommand(MainWindowVM mainwindowvm)
        {
            MainWindowVM = mainwindowvm;
        }

        public override bool CanExecute(object? parameter)
        {
            if(MainWindowVM.SelectedTour != null && base.CanExecute(parameter))
            {
                return true;
            }
            return false;
        }


        public override void Execute(object? parameter)
        {
            MainWindowVM.DeleteTour(MainWindowVM.SelectedIndex);
            MainWindowVM.SelectedTour = null;
        }
    }
}
