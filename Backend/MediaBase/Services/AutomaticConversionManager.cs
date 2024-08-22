using Microsoft.Extensions.Options;
using MediaBase.Models.ConfigModels;

namespace MediaBase.Services
{
    public class AutomaticConversionManager : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AutomaticConversionManager> _logger;
        private readonly int _checkMinutes;
        private Timer _timer;

        public AutomaticConversionManager(IServiceProvider serviceProvider, ILogger<AutomaticConversionManager> logger, IOptions<ConversionConfig> config)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _checkMinutes = config.Value.CheckMinutes;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_checkMinutes <= 0)
                throw new InvalidOperationException("The 'CheckMinutes' must be greater than zero! Please check the configuration!");

            _logger.LogInformation("Automatic Conversion Manager running.");

            _timer = new Timer(CheckMedias, null, TimeSpan.Zero, TimeSpan.FromMinutes(_checkMinutes));

            return Task.CompletedTask;
        }

        private void CheckMedias(object state)
        {
            _logger.LogInformation("Collect medias to send for conversion.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var moviePathProvider = scope.ServiceProvider.GetRequiredService<MoviePathProvider>();
                var seriesPathProvider = scope.ServiceProvider.GetRequiredService<SeriesPathProvider>();
                var mediaConverter = scope.ServiceProvider.GetRequiredService<MediaConverter>();

                var moviesToConvert = moviePathProvider.GetMediasToConvert();
                var seriesToConvert = seriesPathProvider.GetMediasToConvert();
                var mediasToConvert = moviesToConvert.Concat(seriesToConvert);

                if (mediasToConvert.Count() <= 0) return;

                _ = mediaConverter.Convert(mediasToConvert);
            }
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}