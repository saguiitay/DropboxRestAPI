namespace DropboxRestAPI.Models.Core
{
    public class Owner
    {
        /// <summary>
        /// Display name of the owner.
        /// </summary>
        public string display_name { get; set; }
        /// <summary>
        /// The Owner's unique Dropbox ID.
        /// </summary>
        public int uid { get; set; }
        /// <summary>
        /// If endpoint is called by a Dropbox for Business app , member_id of the owner.
        /// </summary>
        public int member_id { get; set; }
    }
}