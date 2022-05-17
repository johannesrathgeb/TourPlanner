using NUnit.Framework;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer;
using TourPlanner.Models;

namespace TourPlannerTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task DirectionsRequestReturnsValues()
        {
            Tour tour = new Tour("Testtour", "Test Description", "Wien", "Salzburg", TransportType.Bus);

            RESTRequest rq = new RESTRequest(); 

            tour = await rq.DirectionsRequest(tour);

            if(tour.TourDistance == null || tour.EstimatedTime == null)
            {
            Assert.Fail();
            }
            Assert.Pass();
        }
    }
}