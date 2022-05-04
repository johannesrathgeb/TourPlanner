using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer;
using TourPlanner.Models;
using TourPlanner.ViewModels;
using System.IO;

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
            if (MainWindowVM.SelectedTour != null && base.CanExecute(parameter))
            {
                return true;
            }
            return false;
        }


        public override void Execute(object? parameter)
        {
            Database db = Database.getInstance();

            db.DeleteTour(MainWindowVM.SelectedTour.Id);

            if (File.Exists(Path.GetFullPath($"../../../../img/img{MainWindowVM.SelectedTour.Id}.png")))
            {
                File.Delete(Path.GetFullPath($"../../../../img/img{MainWindowVM.SelectedTour.Id}.png"));
            }

            //int tempid = MainWindowVM.SelectedTour.Id;

            //MainWindowVM.SelectedTour = null;
            MainWindowVM.DeleteTour(MainWindowVM.SelectedIndex);
            //MainWindowVM.Filepath = "";

            //MainWindowVM.DescriptionChecked = true;



            //File.Delete($"C:\\Users\\flole\\Documents\\FH\\4. Semester\\SWEN2\\TourPlanner Github Project\\img\\img{tempid}.png");

        }
    }
}