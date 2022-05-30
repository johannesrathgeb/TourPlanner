using System.Collections.Generic;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    internal class ClearSearchedToursCommand : BaseCommand
    {


        public MainWindowVM MainWindowVM { get; set; }

        public ClearSearchedToursCommand(MainWindowVM mainwindowvm)
        {
            MainWindowVM = mainwindowvm;
        }


        public override void Execute(object? parameter)
        {
            MainWindowVM.Tours.Clear();
            IEnumerable<Tour> tourlist = TourFactory.GetInstance().GetItems(); 

            foreach (Tour tour in tourlist)
            {
                MainWindowVM.Tours.Add(tour);
                tour.Tourlogs = TourFactory.GetInstance().GetTourlogsByIdFromDB(tour.Id);
            }
        }
    }
}
