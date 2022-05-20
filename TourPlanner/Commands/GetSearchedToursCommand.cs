using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    public class GetSearchedToursCommand : BaseCommand
    {
        public MainWindowVM MainWindowVM { get; set; }

        public GetSearchedToursCommand(MainWindowVM mainwindowvm)
        {
            MainWindowVM = mainwindowvm; 
        }

        public override void Execute(object? parameter)
        {

            if(MainWindowVM.SearchText != null)
            {
                MainWindowVM.Tours.Clear();
                IEnumerable<Tour> tourlist = TourFactory.GetInstance().SearchInDB(MainWindowVM.SearchText);

                foreach (Tour tour in tourlist)
                {
                    MainWindowVM.Tours.Add(tour);
                    tour.Tourlogs = TourFactory.GetInstance().GetTourlogsByIdFromDB(tour.Id);
                }
            }
        }
    }
}
