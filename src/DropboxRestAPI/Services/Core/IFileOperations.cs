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


using System.Threading;
using System.Threading.Tasks;
using DropboxRestAPI.Models.Core;

namespace DropboxRestAPI.Services.Core
{
    public interface IFileOperations
    {
        /// <summary>
        /// Copies a file or folder to a new location.
        /// </summary>
        /// <param name="from_path">Specifies the file or folder to be copied from relative to root.</param>
        /// <param name="to_path">Specifies the destination path, including the new name for the file or folder, relative to root.</param>
        /// <param name="from_copy_ref">Specifies a copy_ref generated from a previous /copy_ref call. Must be used instead of the from_path parameter.</param>
        /// <param name="locale">The metadata returned will have its size field translated based on the given locale.</param>
        /// <param name="asTeamMember">Specify the member_id of the user that the app wants to act on.</param>
        /// <returns>Metadata for the copy of the file or folder.</returns>
        Task<MetaData> CopyAsync(string from_path, string to_path, string from_copy_ref = null, string locale = null, string asTeamMember = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates a folder.
        /// </summary>
        /// <param name="path">The path to the new folder to create relative to root.</param>
        /// <param name="locale">The metadata returned will have its size field translated based on the given locale.</param>
        /// <param name="asTeamMember">Specify the member_id of the user that the app wants to act on.</param>
        /// <returns>Metadata for the new folder.</returns>
        Task<MetaData> CreateFolderAsync(string path, string locale = null, string asTeamMember = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes a file or folder.
        /// </summary>
        /// <param name="path">The path to the file or folder to be deleted.</param>
        /// <param name="locale">The metadata returned will have its size field translated based on the given locale.</param>
        /// <param name="asTeamMember">Specify the member_id of the user that the app wants to act on.</param>
        /// <returns>Metadata for the deleted file or folder.</returns>
        Task<MetaData> DeleteAsync(string path, string locale = null, string asTeamMember = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Moves a file or folder to a new location.
        /// </summary>
        /// <param name="from_path">Specifies the file or folder to be moved from relative to root.</param>
        /// <param name="to_path">Specifies the destination path, including the new name for the file or folder, relative to root.</param>
        /// <param name="locale">The metadata returned will have its size field translated based on the given locale.</param>
        /// <param name="asTeamMember">Specify the member_id of the user that the app wants to act on.</param>
        /// <returns>Metadata for the moved file or folder.</returns>
        Task<MetaData> MoveAsync(string from_path, string to_path, string locale = null, string asTeamMember = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}