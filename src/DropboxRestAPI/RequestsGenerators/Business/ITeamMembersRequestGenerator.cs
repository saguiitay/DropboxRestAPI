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


using DropboxRestAPI.Utils;

namespace DropboxRestAPI.RequestsGenerators.Business
{
    public interface ITeamMembersRequestGenerator
    {
        IRequest Add(string member_email, string member_given_name, string member_surname, string member_external_id = null, bool? send_welcome_email = null);
        IRequest SetProfile(string member_id = null, string external_id = null, string new_email = null, string new_external_id = null);
        IRequest SetPermissions(string member_id = null, string external_id = null, bool? new_is_admin = null);
        IRequest Remove(string member_id = null, string external_id = null, string transfer_dest_member_id = null, string transfer_admin_member_id = null, bool delete_data = true);
    }
}