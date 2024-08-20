using Xabe.FFmpeg;
using MediaBase.Interfaces;
using MediaBase.Models.ConfigModels;

namespace MediaBase.Services
{
    public class MediaConverter
    {
        private readonly ConversionConfig _config;

        public MediaConverter(ConversionConfig config)
        {
            _config = config;
        }

        public async Task Convert(IEnumerable<IMediaFileInfo> mediaFileInfos, string outputFolder)
        {
            FFmpeg.SetExecutablesPath(_config.ConverterPath);

            var tasks = new List<Task>();

            foreach (var mediaFileInfo in mediaFileInfos)
            {
                tasks.Add(
                    Task.Run(() => ConvertToMp4(mediaFileInfo, outputFolder))
                );
            }

            await Task.WhenAll(tasks);
        }

        private async Task ConvertToMp4(IMediaFileInfo mediaFileInfo, string outputFolder)
        {
            var mediaInfo = await FFmpeg.GetMediaInfo(mediaFileInfo.FilePath);
            var fileName = mediaFileInfo.FileName.Replace(mediaFileInfo.Extension, string.Empty);
            var outputFilePath = $"{outputFolder}\\{mediaFileInfo.ParentFolder}\\{fileName}.mp4";

            var conversion = FFmpeg.Conversions.New()
                .AddStream(mediaInfo.VideoStreams.FirstOrDefault()?.SetCodec(VideoCodec.h264))
                .AddStream(mediaInfo.AudioStreams.FirstOrDefault()?.SetCodec(AudioCodec.aac))
                .SetOutput(outputFilePath);

            await conversion.Start();
        }
    }
}