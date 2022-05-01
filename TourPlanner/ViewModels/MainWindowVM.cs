using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.Commands;
using TourPlanner.DataAccessLayer;

namespace TourPlanner.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        private ObservableCollection<Tour> tours = new ObservableCollection<Tour>();
        private ITourFactory tourFactory;
        public ICommand OpenDialogCommand { get; }
        public ICommand DeleteTourCommand { get; }

        private string filepath;
        public string Filepath
        {
            get => filepath;
            set
            {
                if (filepath != value)
                {
                    filepath = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        private string informationVisible;
        private string routeVisible; 
        public string InformationVisible {
            get => informationVisible;
            set
            {
                if (informationVisible != value)
                {
                    informationVisible = value;
                    RaisePropertyChangedEvent();
                }
            }
        }
        public string RouteVisible {
            get => routeVisible;
            set
            {
                if (routeVisible != value)
                {
                    routeVisible = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        private bool descriptionChecked;

        public bool DescriptionChecked
        {
            get => descriptionChecked;
            set 
            {
                if (descriptionChecked != value)
                {
                    descriptionChecked = value;
                    RaisePropertyChangedEvent();
                    ChangeVisibilityValues(value); 
                }
            }
        }

        public void ChangeVisibilityValues(bool value)
        {
            if(value == true)
            {
                InformationVisible = "Visible";
                RouteVisible = "Hidden";
            } else
            {
                InformationVisible = "Hidden";
                RouteVisible = "Visible";
            }
        }




        private int selectedIndex; 
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;
                    RaisePropertyChangedEvent();

                    SelectedTour = ChangeTourSelection(value);

                    Filepath = $"C:\\Users\\flole\\Documents\\FH\\4. Semester\\SWEN2\\TourPlanner Github project\\img\\img{SelectedIndex}.png";

                }
            }
        }

        private Tour? selectedTour; 

        public Tour? SelectedTour 
        {
            get => selectedTour;
            set
            {
                if (selectedTour != value)
                {
                    selectedTour = value;
                    RaisePropertyChangedEvent();
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



        public MainWindowVM()
        {
            
            this.tourFactory = TourFactory.GetInstance();
            Tours = new ObservableCollection<Tour>();
            foreach (Tour tour in this.tourFactory.GetItems())
            {
                Tours.Add(tour);
            }
            AddTour(new Tour("Tour6", 6, "description", "Salzburg", "Wien", TransportType.Car, "350km", "3h"));
            
            if(Tours.Count != 0)
            {
                SelectedTour = Tours[0];
            }

            OpenDialogCommand = new OpenDialogCommand(this);

            DeleteTourCommand = new DeleteTourCommand(this);

            DescriptionChecked = true;

            RESTRequest restrequest = new RESTRequest();
            restrequest.DirectionsRequest();
            //restrequest.StaticmapRequest();

        }
        public void AddTour(Tour tour)
        {
            Tours.Add(tour);
            
        }
        public void DeleteTour(int index)
        {
            Tours.RemoveAt(index);
        }
        public Tour ChangeTourSelection(int val)
        {
            return Tours[val];
        }

    }
}
