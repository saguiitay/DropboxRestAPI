namespace DropboxRestAPI.Models.Core
{
    public class User
    {
        /// <summary>
        /// Display name of the owner.
        /// </summary>
        public string display_name { get; set; }
        /// <summary>
        /// The user's unique Dropbox ID.
        /// </summary>
        public int uid { get; set; }
        /// <summary>
        /// If endpoint is called by a Dropbox for Business app , member_id of the user.
        /// </summary>
        public int member_id { get; set; }
        /// <summary>
        /// If the linked account is a member of a Dropbox for Business team, whether the user is on the same team as the linked account
        /// </summary>
        public bool same_team { get; set; }
    }
}