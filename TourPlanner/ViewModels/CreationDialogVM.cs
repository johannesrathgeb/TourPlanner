using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    internal class CreationDialogVM : ViewModelBase
    {
        Tour newTour = new Tour();

        public event EventHandler Ok;
        public event EventHandler Cancel;

        public Tour NewTour
        {
            get => newTour;
            set
            {
                if (newTour != value)
                {
                    newTour = value;
                    RaisePropertyChangedEvent(nameof(NewTour));
                }
            }
        }

        public ICommand OkCommand { get; init; }
        public ICommand CancelCommand { get; init; }

        public CreationDialogVM()
        {
            this.OkCommand = new DelegateCommand(
                //canexecute funktioniert nicht, weil NewTour setter nie aufgerufen wird und deswegen NotifyPropertyChanged nie aufgerufen wird.

                //(o) => !String.IsNullOrEmpty(NewTour.Name),
                (o) => this.Ok?.Invoke(this, EventArgs.Empty));

            this.CancelCommand = new DelegateCommand((o) => this.Cancel?.Invoke(this, EventArgs.Empty));
        }
    }
}