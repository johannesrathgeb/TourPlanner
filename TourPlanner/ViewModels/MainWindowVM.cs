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
        private ObservableCollection<Tour> tours = new ObservableCollection<Tour>();
        private ITourFactory tourFactory;
        public ICommand OpenDialogCommand { get; }
        public ICommand DeleteTourCommand { get; }
        public ICommand OpenEditDialogCommand { get; }
        public ICommand GenerateTourPDFCommand { get; }
        public ICommand GenerateSummarizePDFCommand { get; }
        public ICommand ImportToursCommand { get; }
        public ICommand ExportToursCommand { get; }
        public ICommand GetSearchedToursCommand { get; }
        public ICommand ClearSearchedToursCommand { get; }
        public ICommand TourViewUpdateCommand { get; }
        public ICommand SelectEnglishCommand { get; }
        public ICommand SelectGermanCommand { get; }
        public ICommand SelectAustrianCommand { get; }

        public byte[]? RouteImageSource { get; set; }


        public TourViewVM TourVM { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                RaisePropertyChangedEvent();
            }
        }

        private string? searchText;
        public string? SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
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
                    TourVM.updateSelectedIndex(selectedIndex);
                    RaisePropertyChangedEvent();

                    SelectedTour = ChangeTourSelection(value);
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
                    if(selectedTour != null)
                    {
                        TourVM.updateSelectedTour(selectedTour);
                    }
                    
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
                    TourVM.updateTourList(tours);
                    RaisePropertyChangedEvent();
                }
            }
        }

        public MainWindowVM()
        {
            //Load TourVM to load TourData in MainView from TourView
            TourVM = new TourViewVM();
            CurrentView = TourVM;

            this.tourFactory = TourFactory.GetInstance();
            Tours = new ObservableCollection<Tour>();
            CalcComputedAttributes cc = new CalcComputedAttributes(); 
            foreach (Tour tour in this.tourFactory.GetItems())
            {
                Tours.Add(tour);
                tour.Tourlogs = this.tourFactory.GetTourlogsByIdFromDB(tour.Id);

                tour.Popularity = cc.CalcPopularity(tour);
                tour.ChildFriendliness = cc.CalcChildFriendliness(tour);
            }

            if (Tours.Count != 0)
            {
                SelectedIndex = 0; 
                SelectedTour = Tours[0];
            }

            OpenDialogCommand = new OpenDialogCommand(this);

            DeleteTourCommand = new DeleteTourCommand(this);

            OpenEditDialogCommand = new OpenEditDialogCommand(this);

            GenerateTourPDFCommand = new GeneratePDFCommand(Tours, 0, this);

            GenerateSummarizePDFCommand = new GeneratePDFCommand(Tours, 1, this);

            ImportToursCommand = new ImportToursCommand(this);

            ExportToursCommand = new ExportToursCommand(this);

            GetSearchedToursCommand = new GetSearchedToursCommand(this);

            ClearSearchedToursCommand = new ClearSearchedToursCommand(this);

            TourViewUpdateCommand = new TourViewUpdateCommand(this);

            SelectEnglishCommand = new SelectLanguageCommand(this, "en");

            SelectGermanCommand = new SelectLanguageCommand(this, "de");

            SelectAustrianCommand = new SelectLanguageCommand(this, "at");

            SelectEnglishCommand.Execute(this);

        }
        public void AddTour(Tour tour)
        {
            Tours.Add(tour);
        }
        public void DeleteTour(int index)
        {
            Tours.RemoveAt(index);
        }

        public void DeleteTourlog(int tourindex, int logindex)
        {
            Tours[tourindex].Tourlogs.RemoveAt(logindex);
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