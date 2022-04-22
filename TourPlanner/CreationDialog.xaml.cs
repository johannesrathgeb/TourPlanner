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
        public CreationDialogue()
        {
            InitializeComponent();
            //var vm = this.DataContext as CreationDialogueViewModel;
            var vm = (CreationDialogVM)this.DataContext;
            vm.Ok+=(o, e) => this.DialogResult = true;
            vm.Cancel+=(o,e) => this.DialogResult = false;
        }
    }
}
