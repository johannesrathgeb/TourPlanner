using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer;
using System.Windows;
using System.Collections.ObjectModel;
using TourPlanner.Models;
using TourPlanner.ViewModels;
using TourPlanner.Logging;

namespace TourPlanner.Commands
{
    public class GeneratePDFCommand : BaseCommand
    {

        public ObservableCollection<Tour> Tours { get; set; }
        public int Type { get; set; }
        public MainWindowVM MainWindowVM { get; set; }

        public GeneratePDFCommand(ObservableCollection<Tour> tourlist, int type, MainWindowVM mainwindowvm)
        {
            Tours = tourlist;
            Type = type; 
            MainWindowVM = mainwindowvm;
        }

        public override void Execute(object? parameter)
        {
            Pdfreport report = new Pdfreport();

            if(Type == 0)
            {
                if(MainWindowVM.SelectedTour == null)
                {
                    MessageBox.Show("Select a Tour first!", "No tour selected", MessageBoxButton.OK, MessageBoxImage.Error);
                    LoggerFactory.GetLogger().Warn("Error while creating tour report - User didn't select a tour");
                } else
                {
                    report.TourReport(MainWindowVM.SelectedTour);
                    MessageBox.Show("Report successfully generated!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } else
            {
                report.SummarizedReport(Tours);
                MessageBox.Show("Report successfully generated!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
