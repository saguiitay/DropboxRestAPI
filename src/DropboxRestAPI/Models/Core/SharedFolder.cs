namespace DropboxRestAPI.Models.Core
{
    public class SharedFolder
    {
        public string shared_folder_id { get; set; }
        public string shared_folder_name { get; set; }
        public string path { get; set; }
        public string access_type { get; set; }
        public string shared_link_policy { get; set; }
        public Membership[] membership { get; set; }
        public Owner owner { get; set; }
        public string[] groups { get; set; }
    }
}