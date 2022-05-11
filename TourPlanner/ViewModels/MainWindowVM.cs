using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.Commands;
using TourPlanner.DataAccessLayer;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TourPlanner.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {

        Database db = Database.getInstance();

        private ObservableCollection<Tour> tours = new ObservableCollection<Tour>();
        private ITourFactory tourFactory;
        public ICommand OpenDialogCommand { get; }
        public ICommand DeleteTourCommand { get; }
        public ICommand OpenEditDialogCommand { get; }

        public byte[]? RouteImageSource { get; set; }


        private string? filepath;
        public string? Filepath
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
        public string InformationVisible
        {
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
        public string RouteVisible
        {
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
            if (value == true)
            {
                InformationVisible = "Visible";
                RouteVisible = "Hidden";
            }
            else
            {
                InformationVisible = "Hidden";
                RouteVisible = "Visible";
            }
        }




        private int selectedIndex = -1;
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

                    if (SelectedTour != null)
                    {
                        Filepath = Path.GetFullPath($"../../../../img/img{SelectedTour.Id}.png");
                        using (var stream = File.Open(Filepath, FileMode.Open))
                        {
                            RouteImageSource = ReadFully(stream);
                        }
                        RaisePropertyChangedEvent(nameof(RouteImageSource));
                    }
                    else
                    {
                        RouteImageSource = null;
                        RaisePropertyChangedEvent(nameof(RouteImageSource));
                    }
                }
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
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
                if (tours != value)
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

            if (Tours.Count != 0)
            {
                SelectedIndex = 0; 
                SelectedTour = Tours[0];
            }

            OpenDialogCommand = new OpenDialogCommand(this);

            DeleteTourCommand = new DeleteTourCommand(this);

            OpenEditDialogCommand = new OpenEditDialogCommand(this);

            DescriptionChecked = true;

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
            if (val != -1)
            {
                return Tours[val];
            }
            else return null;
        }
    }
}
