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
using System.Windows.Shapes;
using TourPlanner.ViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaktionslogik für CreationDialogue.xaml
    /// </summary>
    public partial class CreationDialogue : Window
    {
        public CreationDialogue(MainWindowVM viewmodel, int mode)
        {

            DataContext = new CreationDialogVM(viewmodel, this, mode);
            InitializeComponent();
            //var vm = this.DataContext as CreationDialogueViewModel;
        }
    }
}
