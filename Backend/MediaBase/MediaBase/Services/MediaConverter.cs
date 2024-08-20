using MediaBase.Interfaces;
using Xabe.FFmpeg;

namespace MediaBase.Services
{
    public class MediaConverter
    {
        private readonly string _streamFolder;

        public MediaConverter(string streamFolder)
        {
            _streamFolder = streamFolder;
        }

        public async Task Convert(IEnumerable<IMediaFileInfo> mediaFileInfos)
        {
            FFmpeg.SetExecutablesPath(@"");

            var tasks = new List<Task>();

            foreach (var mediaFileInfo in mediaFileInfos)
            {
                tasks.Add(
                    Task.Run(() => ConvertToMp4(mediaFileInfo))
                );
            }

            await Task.WhenAll(tasks);
        }

        private async Task ConvertToMp4(IMediaFileInfo mediaFileInfo)
        {
            var mediaInfo = await FFmpeg.GetMediaInfo(mediaFileInfo.FilePath);
            var fileName = mediaFileInfo.FileName.Replace(mediaFileInfo.Extension, string.Empty);
            var outputFilePath = $"{_streamFolder}\\{mediaFileInfo.ParentFolder}\\{fileName}.mp4";

            var conversion = FFmpeg.Conversions.New()
                .AddStream(mediaInfo.VideoStreams.FirstOrDefault()?.SetCodec(VideoCodec.h264))
                .AddStream(mediaInfo.AudioStreams.FirstOrDefault()?.SetCodec(AudioCodec.aac))
                .SetOutput(outputFilePath);

            await conversion.Start();
        }
    }
}