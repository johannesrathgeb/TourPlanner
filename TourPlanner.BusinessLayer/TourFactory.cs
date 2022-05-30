namespace TourPlanner.BusinessLayer
{
    public static class TourFactory
    {
        private static ITourFactory? instance;

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
