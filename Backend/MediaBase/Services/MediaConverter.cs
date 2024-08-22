using Microsoft.Extensions.Options;
using Xabe.FFmpeg;
using MediaBase.Interfaces;
using MediaBase.Models.ConfigModels;

namespace MediaBase.Services
{
    public class MediaConverter
    {
        private readonly ILogger<MediaConverter> _logger;
        private readonly ConversionConfig _config;

        public MediaConverter(IOptions<ConversionConfig> config, ILogger<MediaConverter> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        public async Task Convert(IEnumerable<IMediaFileInfo> mediaFileInfos)
        {
            if (!CheckConfiguration(out string errorMessage))
            {
                _logger.LogError($"{errorMessage} Please check the configuration!");
                return;
            }

            _logger.LogInformation($"The conversion is about to start. Files included: {mediaFileInfos.Count()}");

            FFmpeg.SetExecutablesPath(_config.ConverterPath);

            var tasks = new List<Task>();

            foreach (var mediaFileInfo in mediaFileInfos)
            {
                tasks.Add(
                    Task.Run(() => ConvertToMp4(mediaFileInfo))
                );
            }

            await Task.WhenAll(tasks);

            _logger.LogInformation("The conversion has been ended successfully.");
        }

        private async Task ConvertToMp4(IMediaFileInfo mediaFileInfo)
        {
            _logger.LogInformation($"Starting to convert: {mediaFileInfo.FileName}");

            var mediaInfo = await FFmpeg.GetMediaInfo(mediaFileInfo.FilePath);
            var fileName = mediaFileInfo.FileName.Replace(mediaFileInfo.Extension, string.Empty);
            var outputFilePath = $"{_config.StreamFolder}\\{mediaFileInfo.ParentFolder}\\{fileName}.mp4";

            var conversion = FFmpeg.Conversions.New()
                .AddStream(mediaInfo.VideoStreams.FirstOrDefault()?.SetCodec(VideoCodec.h264))
                .AddStream(mediaInfo.AudioStreams.FirstOrDefault()?.SetCodec(AudioCodec.aac))
                .SetOutput(outputFilePath);

            await conversion.Start();

            _logger.LogInformation($"The conversion of {mediaFileInfo.FileName} completed successfully.");
        }

        private bool CheckConfiguration(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(_config.ConverterPath))
            {
                errorMessage = "The 'ConverterPath' cannot be empty!";
                return false;
            }

            else if (string.IsNullOrEmpty(_config.StreamFolder))
            {
                errorMessage = "The 'StreamFolder' cannot be empty!";
                return false;
            }

            return string.IsNullOrEmpty(errorMessage);
        }
    }
}