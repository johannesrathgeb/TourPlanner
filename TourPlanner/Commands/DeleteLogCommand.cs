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

        public TourViewVM TourViewVM { get; set; }

        public DeleteLogCommand(TourViewVM tourwindowvm)
        {
            TourViewVM = tourwindowvm;
        }
        public override bool CanExecute(object? parameter)
        {
            if (TourViewVM.SelectedTour != null && TourViewVM.SelectedLogIndex != -1 && base.CanExecute(parameter))
            {
                return true;
            }
            return false;
        }

        public override void Execute(object? parameter)
        {
            CalcComputedAttributes cc = new CalcComputedAttributes();

            TourFactory.GetInstance().DeleteTourlogFromDB(TourViewVM.SelectedTour.Tourlogs[TourViewVM.SelectedLogIndex].Id);
            TourViewVM.DeleteTourlog(TourViewVM.SelectedIndex, TourViewVM.SelectedLogIndex);

            TourViewVM.SelectedTour.Popularity = cc.CalcPopularity(TourViewVM.SelectedTour);
            TourViewVM.SelectedTour.ChildFriendliness = cc.CalcChildFriendliness(TourViewVM.SelectedTour);
            
        }
    }
}
