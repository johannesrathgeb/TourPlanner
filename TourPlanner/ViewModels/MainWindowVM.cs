using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        Tour newTour = new Tour();
        private ObservableCollection<Tour> tours = new ObservableCollection<Tour>();
        private ITourFactory tourFactory;
        public event EventHandler OpenDialog;

        public string ToursList { get; set; }
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
        public ObservableCollection<Tour> Tours 
        { 
            get => tours;
            set
            {
                if(tours != value)
                {
                    tours = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        public DelegateCommand AddCommand { get; set; }
        public ICommand OpenDialogCommand { get; init; }

        public MainWindowVM()
        {
            //Liste mit Werten befüllen aus BusinessLayer
            this.tourFactory = TourFactory.GetInstance();
            Tours = new ObservableCollection<Tour>();
            foreach(Tour tour in this.tourFactory.GetItems())
            {
                Tours.Add(tour);
            }

            this.OpenDialogCommand = new DelegateCommand((o) =>
            {
                this.OpenDialog?.Invoke(this, EventArgs.Empty);
            });

            this.AddCommand = new DelegateCommand(
                //(o) => !String.IsNullOrEmpty(NewTour.Name),
                (o) =>
                {
                    this.Tours.Add(NewTour);
                    NewTour = new Tour();
                });
        }
    }
}
