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
            //ARRANGE
            Tour tour = new Tour("Testtour", "Test Description", "Wien", "Salzburg", TransportType.Bus);

            RESTRequest rq = new RESTRequest(); 

            //ACT
            tour = await rq.DirectionsRequest(tour);

            //ASSERT
            Assert.IsNotNull(tour.TourDistance, tour.EstimatedTime);
        }
    }
}