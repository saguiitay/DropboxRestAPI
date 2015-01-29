using System;
using System.Linq;

namespace DropboxRestAPI.Utils
{
    public static class HttpValueCollectionExtensions
    {
        public static HttpValueCollection ParseQueryString(this string query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            if ((query.Length > 0) && (query[0] == '?'))
            {
                query = query.Substring(1);
            }

            return new HttpValueCollection(query, true);
        }

        public static string ToQueryString(this HttpValueCollection nvc, bool includePrefix = true)
        {
            string[] array = (from kvp in nvc
                              select string.Format("{0}={1}", Uri.EscapeUriString(kvp.Key), Uri.EscapeDataString(kvp.Value)))
                .ToArray();
            return (includePrefix ? "?" : "") + string.Join("&", array);
        }
    }
}