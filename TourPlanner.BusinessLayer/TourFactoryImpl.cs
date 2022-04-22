using System;
using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    internal class TourFactoryImpl : ITourFactory
    {
        public IEnumerable<Tour> GetItems()
        {
            return new List<Tour>()
            {
                new Tour() { Name = "Tour1", Id=1},
                new Tour() { Name = "Tour2", Id=2},
                new Tour() { Name = "Tour3", Id=3},
                new Tour() { Name = "Tour4", Id=4},
                new Tour() { Name = "Tour5", Id=5}
            };
        }

        public IEnumerable<Tour> Search(string itemName, bool caseSensitive = false)
        {
            throw new NotImplementedException();
        }
    }
}
