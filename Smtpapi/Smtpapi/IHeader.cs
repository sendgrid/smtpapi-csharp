using System;
using System.Collections.Generic;

namespace SendGrid.SmtpApi
{
    /// <summary>
    ///     Represents the additional functionality to add SendGrid specific mail headers
    /// </summary>
    public interface IHeader
    {
        /// <summary>
        ///     Gets the array of recipient addresses from the X-SMTPAPI header
        /// </summary>
        IEnumerable<string> To { get; }

        /// <summary>
        ///     Allows you to specify a filter setting.  You can find a list of filters and settings here:
        ///     http://docs.sendgrid.com/documentation/api/web-api/filtersettings/
        /// </summary>
        /// <param name="filter">The name of the filter to set</param>
        /// <param name="settings">The multipart name of the parameter being set</param>
        /// <param name="value">The value that the settings name will be assigning</param>
        void AddFilterSetting(String filter, IEnumerable<String> settings, String value);

        /// <summary>
        ///     Adds a substitution section to be used during the mail merge.
        /// </summary>
        /// <param name="tag">string to be replaced with the section in the message</param>
        /// <param name="text">The text of the section. May include substituion tags.</param>
        void AddSection(String tag, String text);

        /// <summary>
        ///     This adds a substitution value to be used during the mail merge.  Substitutions
        ///     will happen in order added, so calls to this should match calls to AddTo.
        ///     If a tag already exists, it will be overwritten.
        /// </summary>
        /// <param name="tag">string to be replaced in the message</param>
        /// <param name="substitutions">substitutions to be made, one per recipient</param>
        void AddSubstitution(String tag, IEnumerable<String> substitutions);

        /// <summary>
        ///     This adds parameters and values that will be bassed back through SendGrid's
        ///     Event API if an event notification is triggered by this email.
        /// </summary>
        /// <param name="identifiers">parameter value pairs to be passed back on event notification</param>
        void AddUniqueArgs(IDictionary<String, String> identifiers);

        /// <summary>
        ///     Shortcut method for disabling a filter.
        /// </summary>
        /// <param name="filter">The name of the filter to disable</param>
        void DisableFilter(String filter);

        /// <summary>
        ///     Shortcut method for enabling a filter.
        /// </summary>
        /// <param name="filter">The name of the filter to enable</param>
        void EnableFilter(String filter);

        /// <summary>
        ///     Converts the filter settings into a JSON string.
        /// </summary>
        /// <returns>String representation of the SendGrid headers</returns>
        String JsonString();

        /// <summary>
        ///     This sets the categories for this email.  Statistics are stored on a per category
        ///     basis, so this can be useful for tracking on a per group basis.
        /// </summary>
        /// <param name="categories">categories applied to the message</param>
        void SetCategories(IEnumerable<string> categories);

        /// <summary>
        ///     This sets the category for this email.  Statistics are stored on a per category
        ///     basis, so this can be useful for tracking on a per group basis.
        /// </summary>
        /// <param name="category">categories applied to the message</param>
        void SetCategory(String category);

        /// <summary>
        ///     This adds the "to" array to the X-SMTPAPI header so that multiple recipients
        ///     may be addressed in a single email. (but they each get their own email, instead of a single email with multiple TO:
        ///     addressees)
        /// </summary>
        /// <param name="addresses">List of email addresses</param>
        void SetTo(IEnumerable<string> addresses);

        /// <summary>
        ///     This sets the ASM Group ID for this email.  You can find further documentation about ASM here:
        ///     https://sendgrid.com/docs/API_Reference/Web_API_v3/Advanced_Suppression_Manager/index.html
        /// </summary>
        /// <param name="id">ASM group applied to the message</param>
        void SetASMGroupID(int id);
    }
}