namespace DropboxRestAPI.Models.Core
{
    public class SaveUrl
    {
        public string Job { get; set; }
        public SaveUrlStatus Status { get; set; }
    }

    public enum SaveUrlStatus
    {
        Pending,
        Downloading,
        Complete,
        Failed
    }
}