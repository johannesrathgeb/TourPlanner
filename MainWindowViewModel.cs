using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner
{
    public class MainWindowViewModel : INotifyPropertyChanged, INotifyPropertyChanging
    {
        Tour newTour = new Tour();
        private ObservableCollection<Tour> tours = new ObservableCollection<Tour>();

        public ObservableCollection<Tour> Tours 
        { 
            get => tours;
            set
            {
                if(tours != value)
                {
                    tours = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        public Tour NewTour
        {
            get => newTour;
            set
            {
                if(newTour != value)
                {
                    newTour = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public DelegateCommand AddCommand { get; set; }

        public MainWindowViewModel()
        {
            this.AddCommand = new DelegateCommand(
                //(o) => !String.IsNullOrEmpty(NewTour.Name),
                (o) =>
                {
                    this.Tours.Add(NewTour);
                    NewTour = new Tour();
                });
        }

        public string ToursList { get; set; }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        
        /*
        protected void NotifyPropertyChanging([CallerMemberName] string propertyName = "")
        {
            
            if (!string.IsNullOrEmpty(propertyName))
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(nameof(propertyName)));
            }
            

            var handler = PropertyChanging;
            if(handler != null)
            {
                handler(this, new PropertyChangingEventArgs(propertyName));
            }

        }
        */


        /*
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        */


    }
}
