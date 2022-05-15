using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    public class ImportToursCommand : BaseCommand
    {

        MainWindowVM MainWindowVM { get; set; }

        public ImportToursCommand(MainWindowVM mv)
        {
            MainWindowVM = mv;
        }


        public override async void Execute(object? parameter)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.txt)|*.txt";
            bool? res = ofd.ShowDialog();
            
            if (res == true)
            {
                string filepath = ofd.FileName;

                ProcessFileData pfd = new ProcessFileData();
                ObservableCollection<Tour> tourlist = await pfd.ImportTours(filepath);

                foreach (Tour tour in tourlist)
                {
                    MainWindowVM.Tours.Add(tour);
                }
                MessageBox.Show("Tours have successfully been imported!", "Tours imported", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
