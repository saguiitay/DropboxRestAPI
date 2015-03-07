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
    public class MetaData
    {
        /// <summary>
        /// A human-readable description of the file size (translated by locale).
        /// </summary>
        public string size { get; set; }
        /// <summary>
        /// A folder's hash is useful for indicating changes to the folder's contents in later calls to /metadata. This is roughly
        /// the folder equivalent to a file's rev.
        /// </summary>
        public string hash { get; set; }
        /// <summary>
        /// A unique identifier for the current revision of a file. This field is the same rev as elsewhere in the API and can
        /// be used to detect changes and avoid conflicts.
        /// </summary>
        public string rev { get; set; }
        /// <summary>
        /// True if the file is an image that can be converted to a thumbnail via the /thumbnails call.
        /// </summary>
        public bool thumb_exists { get; set; }

        /// <summary>
        /// The file size in bytes.
        /// </summary>
        public long bytes { get; set; }
        /// <summary>
        /// The last time the file was modified on Dropbox, in the standard date format (not included for the root folder). 
        /// </summary>
        public string modified { get; set; }
        /// <summary>
        /// For files, this is the modification time set by the desktop client when the file was added to Dropbox, in the standard
        /// date format. Since this time is not verified (the Dropbox server stores whatever the desktop client sends up), this should
        /// only be used for display purposes (such as sorting) and not, for example, to determine if a file has changed or not.
        /// </summary>
        public string client_mtime { get; set; }
        /// <summary>
        /// Returns the canonical path to the file or folder.
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// Whether the given entry is a folder or not.
        /// </summary>
        public bool is_dir { get; set; }
        /// <summary>
        /// Whether the given entry is deleted (only included if deleted files are being returned).
        /// </summary>
        public bool is_deleted { get; set; }
        /// <summary>
        /// The name of the icon used to illustrate the file type in Dropbox's icon library.
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// The root or top-level folder depending on your access level. All paths returned are relative to this root level.
        /// Permitted values are either dropbox or app_folder.
        /// </summary>
        public string root { get; set; }
        /// <summary>
        /// The mime type of the file.
        /// </summary>
        public string mime_type { get; set; }
        /// <summary>
        /// A deprecated field that semi-uniquely identifies a file. Use rev instead.
        /// </summary>
        public int revision { get; set; }
        /// <summary>
        /// Only returned when the include_media_info parameter is true and the file is an image. 
        /// </summary>
        public PhotoInfo photo_info { get; set; }
        /// <summary>
        /// Only returned when the include_media_info parameter is true and the file is a video. 
        /// </summary>
        public VideoInfo video_info { get; set; }
        /// <summary>
        /// List of metadata entries for the contents of the folder. 
        /// </summary>
        public List<MetaData> contents { get; set; }
        /// <summary>
        /// This field will be included for shared folders. If the include_membership parameter is passed, there will
        /// additionally be a membership field and a groups field.
        /// </summary>
        public SharedFolder shared_folder { get; set; }
        /// <summary>
        /// For shared folders, this field specifies whether the user has read-only access to the folder. For files within
        /// a shared folder, this specifies the read-only status of the parent shared folder.
        /// </summary>
        public bool read_only { get; set; }
        /// <summary>
        /// For files within a shared folder, this field specifies the ID of the containing shared folder.
        /// </summary>
        public string parent_shared_folder_id { get; set; }
        /// <summary>
        /// For files within a shared folder, this field specifies which user last modified this file. The value is a user
        /// dictionary with the fields uid (user ID), display_name, and, if the linked account is a member of a Dropbox for
        /// Business team, same_team (whether the user is on the same team as the linked account). If this endpoint is called
        /// by a Dropbox for Business app and the user is on that team, a member_id field will also be present in the user
        /// dictionary. If the modifying user no longer exists, the value will be null.
        /// </summary>
        public User modifier { get; set; }

        

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(path))
                {
                    return string.Empty;
                }

                if (path.LastIndexOf("/") == -1)
                {
                    return string.Empty;
                }

                return string.IsNullOrEmpty(path) ? "root" : path.Substring(path.LastIndexOf("/") + 1);
            }
        }

        public string Extension
        {
            get
            {
                if (string.IsNullOrEmpty(path))
                {
                    return string.Empty;
                }

                if (path.LastIndexOf(".") == -1)
                {
                    return string.Empty;
                }

                return is_dir ? string.Empty : path.Substring(path.LastIndexOf("."));
            }
        }
    }
}