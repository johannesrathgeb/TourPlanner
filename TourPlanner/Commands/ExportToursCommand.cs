using Microsoft.Win32;
using System.Text.Json;
using System.Windows;
using TourPlanner.BusinessLayer;
using TourPlanner.ViewModels;

namespace TourPlanner.Commands
{
    internal class ExportToursCommand : BaseCommand
    {

        MainWindowVM MainWindowVM { get; set; }

        public ExportToursCommand(MainWindowVM mv)
        {
            MainWindowVM = mv;
        }

        public override void Execute(object? parameter)
        {
            string tourcontent = JsonSerializer.Serialize(MainWindowVM.Tours);

            ProcessFileData pfd = new ProcessFileData();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "txt files (*.txt)|*.txt";
            bool? res = sfd.ShowDialog();

            if(res == true)
            {
                pfd.ExportTours(tourcontent, sfd); 
                MessageBox.Show("Tours have successfully been exported!", "Tours exported", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
