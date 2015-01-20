namespace DropboxRestAPI.Models.Core
{
    public class DeltaCursor
    {
        /// <summary>
        /// A string that encodes the latest information that has been returned. On the next call to /delta, pass in this value.
        /// </summary>
        public string cursor { get; set; }
    }
}