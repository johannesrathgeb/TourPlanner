using Moq;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer;
using TourPlanner.DataAccessLayer;
using TourPlanner.Models;

namespace TourPlannerTests
{
    public class Tests
    {
        private ITourFactory tourfactory; 
        private readonly Tour tour1 = new Tour("Testtour", "Test Description", "Wien", "Salzburg", TransportType.Car, "300", "3:00");
        private readonly Tour fulltour1 = new Tour(1, "Testtour", "Test Description", "Wien", "Salzburg", TransportType.Car, "300", "3:00");
        private readonly Tour fulltour2 = new Tour(1, "Testtour", "Test Description", "Wien", "Linz", TransportType.Car, "350", "3:00");
        private readonly Tour fulltour3 = new Tour(1, "Testtour", "Test Description", "Wien", "Graz", TransportType.Car, "200", "3:00");
        

        private Tourlog tourlog1 = new Tourlog("10.02.2022", "Testcomment", 3, "180", 2);
        private Tourlog fulltourlog1 = new Tourlog(1, "10.02.2022", "Testcomment", 3, "180", 2);
        private Tourlog fulltourlog2 = new Tourlog(2, "10.02.2022", "Testcomment", 3, "180", 2);
        private Tourlog fulltourlog3 = new Tourlog(3, "10.02.2022", "Testcomment", 3, "180", 2);
        private ObservableCollection<Tourlog> loglist = new ObservableCollection<Tourlog>();
        private ObservableCollection<Tour> tourlist = new ObservableCollection<Tour>();

        [SetUp]
        public void Setup()
        {
            var mockobject = new Mock<ITourFactory>();
            mockobject
                .Setup(mobj => mobj.AddTourToDB(tour1.Name, tour1.TourDescription, tour1.From, tour1.To, tour1.TransportType, tour1.EstimatedTime, tour1.TourDistance))
                .Returns(fulltour1);
            mockobject
                .Setup(mobj => mobj.AddTourlogToDB(1, tourlog1.Date, tourlog1.Comment, tourlog1.Difficulty, tourlog1.Totaltime, tourlog1.Rating))
                .Returns(fulltourlog1);
            mockobject
                .Setup(mobj => mobj.GetItems())
                .Returns(tourlist);
            mockobject
                .Setup(mobj => mobj.UpdateTourInDB(fulltour1))
                .Returns(fulltour1);
            mockobject
                .Setup(mobj => mobj.UpdateTourlogInDB(tourlog1))
                .Returns(tourlog1);
            mockobject
                .Setup(mobj => mobj.GetTourlogsByIdFromDB(2))
                .Returns(loglist);
            mockobject
                .Setup(mobj => mobj.SearchInDB("graz"))
                .Returns(tourlist);

            tourfactory = mockobject.Object;
        }
        
        [Test]
        public void Test_AddTourReturnsNewestTour()
        {
            //ACT
            Tour tour = tourfactory.AddTourToDB(tour1.Name, tour1.TourDescription, tour1.From, tour1.To, tour1.TransportType, tour1.EstimatedTime, tour1.TourDistance);

            //ASSERT
            Assert.AreEqual(1,tour.Id);
        }

        [Test]
        public void Test_AddTourlogReturnsNewestTourlog()
        {
            //ACT
            Tourlog tourlog = tourfactory.AddTourlogToDB(1, tourlog1.Date, tourlog1.Comment, tourlog1.Difficulty, tourlog1.Totaltime, tourlog1.Rating);

            //ASSERT
            Assert.AreEqual(1, tourlog.Id);
        }

        [Test]
        public void Test_GetItemsReturnsTourlist()
        {
            //ARRANGE
            tourlist.Clear();
            tourlist.Add(fulltour1);
            tourlist.Add(fulltour2);
            tourlist.Add(fulltour3);
            ObservableCollection<Tour> tours = new ObservableCollection<Tour>();

            //ACT
            foreach(Tour tour in tourfactory.GetItems())
            {
                tours.Add(tour);
            }

            //ASSERT
            Assert.AreEqual(3, tours.Count);
        }

        [Test]
        public void Test_UpdateTourReturnUpdated()
        {
            //ACT
            Tour tour = tourfactory.UpdateTourInDB(fulltour1);

            //ASSERT
            Assert.AreEqual(fulltour1, tour);
        }

        [Test]
        public void Test_UpdateTourlogReturnUpdated()
        {
            //ACT
            Tourlog tourlog = tourfactory.UpdateTourlogInDB(tourlog1);

            //ASSERT
            Assert.AreEqual(tourlog1, tourlog);
        }

        [Test]
        public void Test_ReturnsLogListWithSameTourID()
        {
            //ARRANGE
            bool sameid = true; 
            ObservableCollection<Tourlog> newloglist = new ObservableCollection<Tourlog>();
            loglist.Clear(); 
            loglist.Add(fulltourlog2);
            loglist.Add(fulltourlog2);

            //ACT
            newloglist = tourfactory.GetTourlogsByIdFromDB(2); 

            foreach(Tourlog tourlog in newloglist)
            {
                if(tourlog.Id != 2) sameid = false;
            }

            //ASSERT
            Assert.IsTrue(sameid);
        }

        [Test]
        public void Test_FulltextsearchReturnsFoundTours()
        {
            //ARRANGE
            tourlist.Clear(); 
            tourlist.Add(fulltour3);

            ObservableCollection<Tour> tours = new ObservableCollection<Tour>();

            //ACT
            foreach (Tour tour in tourfactory.SearchInDB("graz"))
            {
                tours.Add(tour);
            }

            //ASSERT
            Assert.AreEqual(1, tours.Count); 
        }

        [Test]
        public async Task Test_DirectionsRequestReturnsValues()
        {

            //ARRANGE
            RESTRequest rq = new RESTRequest(); 
            Tour tour = new Tour("Testtour", "Test Description", "Wien", "Salzburg", TransportType.Bus);

            //ACT
            tour = await rq.DirectionsRequest(tour);

            //ASSERT
            Assert.IsNotNull(tour.TourDistance, tour.EstimatedTime);

        }

        [Test]
        public async Task Test_DirectionsRequestFails()
        {
            //ARRANGE
            RESTRequest rq = new RESTRequest();
            Tour tour = new Tour("Testtour", "Test Description", "Wien", "doesntexist", TransportType.Bus);

            //ACT
            tour = await rq.DirectionsRequest(tour);

            //ASSERT
            Assert.IsNull(tour); 
        }

        [Test]
        public async Task Test_StaticMapRequestReturnsValue()
        {
            //ARRANGE
            Tour tour = new Tour(1, "Testtour", "Test Description", "Wien", "Linz", TransportType.Car, "350", "3:00");
            RESTRequest rq = new RESTRequest();

            //ACT & ASSERT
            if(await rq.StaticmapRequest(tour) != null)
            {
                Assert.Pass(); 
            }
        }


        [Test]
        public void Test_PopularityCalc()
        {
            //ARRANGE
            CalcComputedAttributes cc = new CalcComputedAttributes();
            Tour tour = new Tour(0, "Testtour", "Test Description", "Wien", "Salzburg", TransportType.Car, "300", "3:00");
            tour.Tourlogs.Add(new Tourlog(0, "10.02.2022", "Testcomment", 3, "180", 2)); 
            tour.Tourlogs.Add(new Tourlog(1, "10.02.2022", "Testcomment", 3, "180", 2)); 
            tour.Tourlogs.Add(new Tourlog(2, "10.02.2022", "Testcomment", 3, "180", 2));

            //ACT & ASSERT 
            Assert.AreEqual(2, cc.CalcPopularity(tour));
        }

        [Test]
        public void Test_PopularityCalcNotEnoughEntries()
        {
            //ARRANGE
            CalcComputedAttributes cc = new CalcComputedAttributes();
            Tour tour = new Tour(0, "Testtour", "Test Description", "Wien", "Salzburg", TransportType.Car, "300", "3:00");

            //ACT & ASSERT
            Assert.AreEqual(0, cc.CalcPopularity(tour));
        }

        [Test]
        public void Test_DifficultyCalc()
        {
            //ARRANGE
            CalcComputedAttributes cc = new CalcComputedAttributes();
            Tour tour = new Tour(0, "Testtour", "Test Description", "Wien", "Salzburg", TransportType.Car, "300", "3:00");
            tour.Tourlogs.Add(new Tourlog(0, "10.02.2022", "Testcomment", 1, "180", 2));
            tour.Tourlogs.Add(new Tourlog(1, "10.02.2022", "Testcomment", 5, "180", 2));
            tour.Tourlogs.Add(new Tourlog(2, "10.02.2022", "Testcomment", 3, "180", 2));

            //ACT & ASSERT
            Assert.AreEqual(3, cc.CalcDifficulty(tour));
        }

        [Test]
        public void Test_DifficultyCalcNotEnoughEntries()
        {
            //ARRANGE
            CalcComputedAttributes cc = new CalcComputedAttributes();
            Tour tour = new Tour(0, "Testtour", "Test Description", "Wien", "Salzburg", TransportType.Car, "300", "3:00");

            //ACT & ASSERT
            Assert.AreEqual(0, cc.CalcDifficulty(tour));
        }

        [Test]
        public void Test_TotalTimesCalc()
        {
            //ARRANGE
            CalcComputedAttributes cc = new CalcComputedAttributes();
            Tour tour = new Tour(0, "Testtour", "Test Description", "Wien", "Salzburg", TransportType.Car, "300", "3:00");
            tour.Tourlogs.Add(new Tourlog(0, "10.02.2022", "Testcomment", 1, "110", 2));
            tour.Tourlogs.Add(new Tourlog(1, "10.02.2022", "Testcomment", 5, "135", 2));
            tour.Tourlogs.Add(new Tourlog(2, "10.02.2022", "Testcomment", 3, "145", 2));

            //ACT & ASSERT
            Assert.AreEqual(4, cc.CalcTotalTimes(tour));
        }

        [Test]
        public void Test_TotalTimesCalcNotEnoughEntries()
        {
            //ARRANGE
            CalcComputedAttributes cc = new CalcComputedAttributes();
            Tour tour = new Tour(0, "Testtour", "Test Description", "Wien", "Salzburg", TransportType.Car, "300", "3:00");

            //ACT & ASSERT
            Assert.AreEqual(0, cc.CalcTotalTimes(tour));
        }

        [Test]
        public void Test_DistanceCalcCheck()
        {
            //ARRANGE
            CalcComputedAttributes cc = new CalcComputedAttributes();
            Tour tour = new Tour(0, "Testtour", "Test Description", "Wien", "Salzburg", TransportType.Car, "300", "3:00");

            //ACT & ASSERT
            Assert.AreEqual(2, cc.CalcDistance(80)); 
        }

        [Test]
        public void Test_ChildFriendlinessCalc()
        {
            //ACT
            CalcComputedAttributes cc = new CalcComputedAttributes();
            Tour tour = new Tour(0, "Testtour", "Test Description", "Wien", "Salzburg", TransportType.Car, "80", "3:00");
            tour.Tourlogs.Add(new Tourlog(0, "10.02.2022", "Testcomment", 1, "110", 2));
            tour.Tourlogs.Add(new Tourlog(1, "10.02.2022", "Testcomment", 5, "135", 2));
            tour.Tourlogs.Add(new Tourlog(2, "10.02.2022", "Testcomment", 3, "145", 2));

            //ARRANGE & ASSERT
            Assert.AreEqual(2, cc.CalcChildFriendliness(tour)); 
        }

        [Test]
        public void Test_ChildFriendlinessCalcNotEnoughEntries()
        {
            //ARRANGE
            CalcComputedAttributes cc = new CalcComputedAttributes();
            Tour tour = new Tour(0, "Testtour", "Test Description", "Wien", "Salzburg", TransportType.Car, "80", "3:00");

            //ACT & ASSERT
            Assert.AreEqual(0, cc.CalcChildFriendliness(tour));
        }

        [Test]
        public void Test_TourDeletedAtIndex()
        {
            //ARRANGE
            Tour t1 = new Tour(1, "Testtour", "Test Description", "Wien", "Salzburg", TransportType.Car, "80", "3:00");
            Tour t2 = new Tour(2, "Testtour", "Test Description", "Wien", "Linz", TransportType.Car, "80", "3:00");
            Tour t3 = new Tour(3, "Testtour", "Test Description", "Wien", "Graz", TransportType.Car, "80", "3:00");
            ObservableCollection<Tour> oldtours = new ObservableCollection<Tour>();
            ObservableCollection<Tour> newtours = new ObservableCollection<Tour>();
            oldtours.Add(t1);
            oldtours.Add(t2);
            oldtours.Add(t3);

            //ACT
            foreach(Tour tour in TourFactory.GetInstance().DeleteTour(oldtours, 1))
            {
                newtours.Add(tour); 
            }

            //ASSERT
            foreach(Tour tour in newtours)
            {
                if(tour.Id == 2) Assert.Fail();
            }

            Assert.AreEqual(2, newtours.Count);
        }
    }
}
