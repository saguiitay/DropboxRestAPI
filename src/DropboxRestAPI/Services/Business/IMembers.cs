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
using DropboxRestAPI.Models.Business;

namespace DropboxRestAPI.Services.Business
{
    public interface IMembers
    {
        /// <summary>
        /// Adds a member to a team.
        /// </summary>
        /// <param name="member_email">member email</param>
        /// <param name="member_given_name">member first name</param>
        /// <param name="member_surname">member last name</param>
        /// <param name="member_external_id">external ID for member</param>
        /// <param name="send_welcome_email">boolean to send a welcome email to the member. Default is true.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>Returns the profile of the created member.</returns>
        /// <remarks>
        /// Users in the "active" state have successfully logged into their Dropbox for Business accounts, while users in the
        /// "invited" state have not.
        /// If no Dropbox account exists with the email address specified, a new Dropbox account will be created with the given email
        /// address, and that account will be invited to the team.
        /// If a personal Dropbox account exists with the email address specified in the call, this call will create a placeholder
        /// Dropbox account for the user on the team and send an email inviting the user to migrate their existing personal account
        /// onto the team.
        /// Team member management apps are required to set an initial given_name and surname for a user to use in the team invitation
        /// and for "Perform as team member" actions taken on the user before they become "active".
        /// Team member management apps will not be able to subsequently edit the given_name and surname fields using the set_profile
        /// endpoint.
        /// If send_welcome_email is false, no email invitation will be sent to the user. This may be useful for apps using single
        /// sign-on (SSO) flows for onboarding that want to handle Dropbox for Business announcements themselves.
        /// </remarks>
        Task<MemberInfo> AddAsync(string member_email, string member_given_name, string member_surname, string member_external_id = null, bool? send_welcome_email = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates a team member's profile.
        /// </summary>
        /// <remarks>
        /// Exactly one of either a member_id or an external_id must be provided to identify the user account.
        /// Exactly one of either a new_member_id or a new_external_id must be provided to update: attempting to change both at the
        /// same time will return a 400 error.
        /// </remarks>
        /// <param name="member_id">member ID. Must specify either a member_id or external_id.</param>
        /// <param name="external_id">external ID. Must specify either a member_id or external_id.</param>
        /// <param name="new_email">new email for member. Must specify either a new_email or new_external_id.</param>
        /// <param name="new_external_id">new external ID for member. Must specify either a new_email or new_external_id.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>Returns the profile of the updated member.</returns>
        Task<MemberInfo> SetProfileAsync(string member_id = null, string external_id = null, string new_email = null, string new_external_id = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates a team member's permissions.
        /// </summary>
        /// <remarks>Exactly one of either a member_id or an external_id must be provided to identify the user account.</remarks>
        /// <param name="member_id">member ID. Must specify either a member_id or external_id.</param>
        /// <param name="external_id">external ID. Must specify either a member_id or external_id.</param>
        /// <param name="new_is_admin">change the admin status of the member.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>Returns the member's permissions.</returns>
        Task<PermissionInfo> SetPermissionsAsync(string member_id = null, string external_id = null, bool? new_is_admin = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Removes a member from a team.
        /// </summary>
        /// <remarks>
        /// Exactly one of either a member_id or an external_id must be provided to identify the user account.
        /// This is not a deactivation where the account can be re-activated again. Calling add_member with the removed user's email
        /// address will create a new account with a new member_id that will not have access to any content that was shared with the
        /// initial account.
        /// </remarks>
        /// <param name="member_id">member ID. Must specify either a member_id or external_id.</param>
        /// <param name="external_id">external ID. Must specify either a member_id or external_id.</param>
        /// <param name="transfer_dest_member_id">files from the deleted member account will be transferred to this member.</param>
        /// <param name="transfer_admin_member_id">errors during the transfer process will be sent via email to the transfer_admin_member_id.</param>
        /// <param name="delete_data">controls if the user's data will be deleted on their linked devices. Default is true.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        Task RemoveAsync(string member_id = null, string external_id = null, string transfer_dest_member_id = null, string transfer_admin_member_id = null, bool delete_data = true, CancellationToken cancellationToken = default(CancellationToken));
    }
}