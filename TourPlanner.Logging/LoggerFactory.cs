namespace TourPlanner.Logging
{
    public static class LoggerFactory
    {
        public static ILoggerWrapper GetLogger()
        {
            return Log4NetWrapper.CreateLogger("../../../../config/log4netconfig/log4net.config");
        }
    }
}
