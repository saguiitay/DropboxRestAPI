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

using System.Collections.Generic;

namespace DropboxRestAPI.Models.Core
{
    public class Entries
    {
        /// <summary>
        /// A list of "delta entries"
        /// Each delta entry is a 2-item list of one of the following forms:
        /// - [&gt;path&lt;, &gt;metadata&lt;] - Indicates that there is a file/folder at the given path. You should add the entry to
        ///   your local state. The metadata value is the same as what would be returned by the /metadata call, except folder metadata
        ///   doesn't have hash or contents fields. To correctly process delta entries:If the new entry includes parent folders that
        ///   don't yet exist in your local state, create those parent folders in your local state.
        ///   - If the new entry is a file, replace whatever your local state has at path with the new entry.
        ///   - If the new entry is a folder, check what your local state has at &gt;path&lt;. If it's a file, replace it with the new
        ///     entry. If it's a folder, apply the new &gt;metadata&lt; to the folder, but don't modify the folder's children. If your
        ///     local state doesn't yet include this path, create it as a folder.
        ///   - If the new entry is a folder with the read-only field set to true, apply the read-only permission recursively to all
        ///     files within the shared folder.
        /// - [&gt;path&lt;, null] - Indicates that there is no file/folder at the given path. To update your local state to match,
        ///   anything at path and all its children should be deleted. Deleting a folder in your Dropbox will sometimes send down a
        ///   single deleted entry for that folder, and sometimes separate entries for the folder and all child paths. If your local
        ///   state doesn't have anything at path, ignore this entry.
        /// Note: Dropbox treats file names in a case-insensitive but case-preserving way. To facilitate this, the &gt;path&lt; values
        /// above are lower-cased versions of the actual path. The last path component of the &gt;metadata&lt; value will be case-preserved.
        /// </summary>
        public IEnumerable<string[]> entries { get; set; }
        /// <summary>
        /// If true, clear your local state before processing the delta entries. reset is always true on the initial call to /delta
        /// (i.e. when no cursor is passed in). Otherwise, it is true in rare situations, such as after server or account maintenance,
        /// or if a user deletes their app folder.
        /// </summary>
        public bool reset { get; set; }
        /// <summary>
        /// A string that encodes the latest information that has been returned. On the next call to /delta, pass in this value.
        /// </summary>
        public string cursor { get; set; }
        /// <summary>
        /// If true, then there are more entries available; you can call /delta again immediately to retrieve those entries. If 'false',
        /// then wait for at least five minutes (preferably longer) before checking again.
        /// </summary>
        public bool has_more { get; set; }
    }
}