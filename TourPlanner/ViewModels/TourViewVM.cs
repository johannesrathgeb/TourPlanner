using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.Commands;
using System.Collections.ObjectModel;

namespace TourPlanner.ViewModels
{
    public class TourViewVM : ViewModelBase
    {
        public ICommand OpenLogsDialogCommand { get; }
        public ICommand OpenEditLogsDialogCommand { get; }
        public ICommand DeleteLogCommand { get; }


        public byte[]? RouteImageSource { get; set; }
        public byte[]? TransportTypeImage { get; set; }

        private int selectedLogIndex = -1;

        public int SelectedLogIndex
        {
            get => selectedLogIndex;
            set
            {
                if (selectedLogIndex != value)
                {
                    selectedLogIndex = value;
                    RaisePropertyChangedEvent();
                }
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
        
        private ObservableCollection<Tour> tours = new ObservableCollection<Tour>();

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

        public void updateTourList(ObservableCollection<Tour> tourList)
        {
            Tours = tourList;
        }

        public void updateSelectedIndex(int index)
        {
            SelectedIndex = index;
        }

        public void updateSelectedTour(Tour tour)
        {
            SelectedTour = tour;

            if (SelectedTour != null)
            {
                //Load Map image
                Filepath = Path.GetFullPath($"../../../../img/img{SelectedTour.Id}.png");
                using (var stream = File.Open(Filepath, FileMode.Open))
                {
                    RouteImageSource = ReadFully(stream);
                }
                RaisePropertyChangedEvent(nameof(RouteImageSource));

                
                //Load Transport Type image
                Filepath = Path.GetFullPath($"../../../../TourPlanner/Resources/{SelectedTour.TransportType}.png");
                using (var stream = File.Open(Filepath, FileMode.Open))
                {
                    TransportTypeImage = ReadFully(stream);
                }
                RaisePropertyChangedEvent(nameof(TransportTypeImage));

            }
            else
            {
                RouteImageSource = null;
                RaisePropertyChangedEvent(nameof(RouteImageSource));
                TransportTypeImage = null;
                RaisePropertyChangedEvent(nameof(TransportTypeImage));
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

        public TourViewVM()
        {
            OpenLogsDialogCommand = new OpenLogsDialogCommand(this);
            DeleteLogCommand = new DeleteLogCommand(this);
            OpenEditLogsDialogCommand = new OpenEditLogsDialogCommand(this);
        }

        public Tour ChangeTourSelection(int val)
        {
            if (val != -1)
            {
                return Tours[val];
            }
            else return null;
        }

        public void DeleteTourlog(int tourindex, int logindex)
        {
            Tours[tourindex].Tourlogs.RemoveAt(logindex);
        }
    }
}