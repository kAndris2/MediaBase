using NLog;
using NLog.Web;

namespace MediaBase
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup()
                .LoadConfigurationFromFile("appsettings")
                .GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");

                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseNLog();
                })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            })
            .UseNLog();
    }
}