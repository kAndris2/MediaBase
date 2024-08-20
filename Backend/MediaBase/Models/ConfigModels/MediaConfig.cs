namespace MediaBase.Models.ConfigModels
{
    public class MediaConfig
    {
        public string[] SourceFolders { get; set; }
        public string StreamFolder { get; set; }
        public string[] SupportedExtensions { get; set; }
    }
}