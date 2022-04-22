using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BusinessLayer
{
    public static class TourFactory
    {
        private static ITourFactory instance;

        public static ITourFactory GetInstance()
        {
            if(instance == null)
            {
                instance = new TourFactoryImpl();
            }
            return instance;
        }
    }
}
