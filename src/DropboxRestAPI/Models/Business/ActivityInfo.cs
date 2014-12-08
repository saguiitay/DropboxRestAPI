/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2014 Itay Sagui
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */


namespace DropboxRestAPI.Models.Business
{
    public class ActivityInfo
    {
        public string start_date { get; set; }
        public long?[] active_users_1_day { get; set; }
        public long?[] active_users_7_day { get; set; }
        public long?[] active_users_28_day { get; set; }
        public long?[] adds { get; set; }
        public long?[] edits { get; set; }
        public long?[] deletes { get; set; }
        public long?[] active_shared_folders_1_day { get; set; }
        public long?[] active_shared_folders_7_day { get; set; }
        public long?[] active_shared_folders_28_day { get; set; }
        public long?[] shared_links_created { get; set; }
        public long?[] shared_links_viewed_total { get; set; }
        public long?[] shared_links_viewed_by_team { get; set; }
        public long?[] shared_links_viewed_by_outside_user { get; set; }
        public long?[] shared_links_viewed_by_not_logged_in { get; set; }
    }
}