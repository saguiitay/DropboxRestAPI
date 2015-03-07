namespace DropboxRestAPI.Models.Core
{
    public class Membership
    {
        public User user { get; set; }
        public string access_type { get; set; }
        public bool active { get; set; }
    }
}