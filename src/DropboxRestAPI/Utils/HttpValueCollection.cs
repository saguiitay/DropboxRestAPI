using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DropboxRestAPI.Utils
{
    public class HttpValueCollection : Collection<HttpValue>
    {
        #region Constructors

        public HttpValueCollection()
        {
        }

        public HttpValueCollection(string query)
            : this(query, true)
        {
        }

        public HttpValueCollection(string query, bool urlencoded)
        {
            if (!string.IsNullOrEmpty(query))
            {
                FillFromString(query, urlencoded);
            }
        }

        #endregion

        #region Parameters

        public string this[string key]
        {
            get { return this.First(x => string.Equals(x.Key, key, StringComparison.OrdinalIgnoreCase)).Value; }
            set { this.First(x => string.Equals(x.Key, key, StringComparison.OrdinalIgnoreCase)).Value = value; }
        }

        #endregion

        #region Public Methods

        public void Add(string key, string value)
        {
            Add(new HttpValue(key, value));
        }

        public bool ContainsKey(string key)
        {
            return this.Any(x => string.Equals(x.Key, key, StringComparison.OrdinalIgnoreCase));
        }

        public string[] GetValues(string key)
        {
            return this.Where(x => string.Equals(x.Key, key, StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).ToArray();
        }

        public void Remove(string key)
        {
            var toRemove = this.Where(x => string.Equals(x.Key, key, StringComparison.OrdinalIgnoreCase))
                .ToList();
            foreach (var x in toRemove)
                Remove(x);
        }

        public override string ToString()
        {
            return ToString(true);
        }

        public virtual string ToString(bool urlencoded)
        {
            return ToString(urlencoded, null);
        }

        public virtual string ToString(bool urlencoded, IEnumerable<string> excludeKeys)
        {
            if (Count == 0)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();

            foreach (HttpValue item in this)
            {
                string key = item.Key;

                if ((excludeKeys == null) || !excludeKeys.Contains(key))
                {
                    string value = item.Value;

                    if (urlencoded)
                    {
                        key = Uri.EscapeDataString(key);
                    }

                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Append('&');
                    }

                    stringBuilder.Append((key != null) ? (key + "=") : string.Empty);

                    if (!string.IsNullOrEmpty(value))
                    {
                        if (urlencoded)
                        {
                            value = Uri.EscapeDataString(value);
                        }

                        stringBuilder.Append(value);
                    }
                }
            }

            return stringBuilder.ToString();
        }

        #endregion

        #region Private Methods

        private void FillFromString(string query, bool urlencoded)
        {
            int num = (query != null) ? query.Length : 0;
            for (int i = 0; i < num; i++)
            {
                int startIndex = i;
                int num4 = -1;
                while (i < num)
                {
                    char ch = query[i];
                    if (ch == '=')
                    {
                        if (num4 < 0)
                        {
                            num4 = i;
                        }
                    }
                    else if (ch == '&')
                    {
                        break;
                    }
                    i++;
                }
                string str = null;
                string str2;
                if (num4 >= 0)
                {
                    str = query.Substring(startIndex, num4 - startIndex);
                    str2 = query.Substring(num4 + 1, (i - num4) - 1);
                }
                else
                {
                    str2 = query.Substring(startIndex, i - startIndex);
                }

                if (urlencoded)
                {
                    Add(Uri.UnescapeDataString(str), Uri.UnescapeDataString(str2));
                }
                else
                {
                    Add(str, str2);
                }

                if ((i == (num - 1)) && (query[i] == '&'))
                {
                    Add(null, string.Empty);
                }
            }
        }

        #endregion
    }
}