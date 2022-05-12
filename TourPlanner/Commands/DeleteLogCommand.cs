using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer;
using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    internal class DeleteLogCommand : BaseCommand
    {

        public MainWindowVM MainWindowVM { get; set; }

        public DeleteLogCommand(MainWindowVM mainwindowvm)
        {
            MainWindowVM = mainwindowvm;
        }
        public override bool CanExecute(object? parameter)
        {
            if (MainWindowVM.SelectedTour != null && MainWindowVM.SelectedLogIndex != -1 && base.CanExecute(parameter))
            {
                return true;
            }
            return false;
        }

        public override void Execute(object? parameter)
        {
            TourFactory.GetInstance().DeleteTourlogFromDB(MainWindowVM.SelectedTour.Tourlogs[MainWindowVM.SelectedLogIndex].Id);
            MainWindowVM.DeleteTourlog(MainWindowVM.SelectedIndex, MainWindowVM.SelectedLogIndex);
        }
    }
}
