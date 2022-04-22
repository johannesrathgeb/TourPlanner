using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TourPlanner.ViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //DataContext = new MainWindowViewModel();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = (MainWindowVM)DataContext;
            ((MainWindowVM)DataContext).OpenDialog += (s, ev) =>
              {
                  CreationDialogue dialog = new CreationDialogue();
                  if (dialog.ShowDialog() == true)
                  {
                    var dialogViewModel = (CreationDialogVM)       dialog.DataContext;
                    vm.NewTour.Name = dialogViewModel.NewTour.Name;
                    vm.AddCommand.Execute(vm.NewTour);
                  }
              };
        }
    }                   
}                       
                        