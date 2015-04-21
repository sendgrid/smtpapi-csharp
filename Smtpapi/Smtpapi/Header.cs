using System.Collections.Generic;
using System.Linq;

namespace SendGrid.SmtpApi
{
    /// <summary>
    /// </summary>
    public class Header : IHeader
    {
        #region Private Members

        private readonly HeaderSettingsNode _settings;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the array of recipient addresses from the X-SMTPAPI header
        /// </summary>
        public IEnumerable<string> To
        {
            get { return _settings.GetArray("to"); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// </summary>
        public Header()
        {
            _settings = new HeaderSettingsNode();
        }

        /// <summary>
        ///     Allows you to specify a filter setting.  You can find a list of filters and settings here:
        ///     http://docs.sendgrid.com/documentation/api/web-api/filtersettings/
        /// </summary>
        /// <param name="filter">The name of the filter to set</param>
        /// <param name="settings">The multipart name of the parameter being set</param>
        /// <param name="value">The value that the settings name will be assigning</param>
        public void AddFilterSetting(string filter, IEnumerable<string> settings, string value)
        {
            List<string> keys = new List<string> {"filters", filter, "settings"}.Concat(settings).ToList();
            _settings.AddSetting(keys, value);
        }

        /// <summary>
        ///     Adds a substitution section to be used during the mail merge.
        /// </summary>
        /// <param name="tag">string to be replaced with the section in the message</param>
        /// <param name="text">The text of the section. May include substituion tags.</param>
        public void AddSection(string tag, string text)
        {
            var keys = new List<string> {"section", tag};
            _settings.AddSetting(keys, text);
        }

        /// <summary>
        ///     This adds a substitution value to be used during the mail merge.  Substitutions
        ///     will happen in order added, so calls to this should match calls to AddTo.
        ///     If a tag already exists, it will be overwritten.
        /// </summary>
        /// <param name="tag">string to be replaced in the message</param>
        /// <param name="substitutions">substitutions to be made, one per recipient</param>
        public void AddSubstitution(string tag, IEnumerable<string> substitutions)
        {
            var keys = new List<string> {"sub", tag};
            _settings.AddArray(keys, substitutions);
        }

        /// <summary>
        ///     This adds parameters and values that will be bassed back through SendGrid's
        ///     Event API if an event notification is triggered by this email.
        /// </summary>
        /// <param name="identifiers">parameter value pairs to be passed back on event notification</param>
        public void AddUniqueArgs(IDictionary<string, string> identifiers)
        {
            foreach (string key in identifiers.Keys)
            {
                var keys = new List<string> {"unique_args", key};
                string value = identifiers[key];
                _settings.AddSetting(keys, value);
            }
        }

        /// <summary>
        ///     Shortcut method for disabling a filter.
        /// </summary>
        /// <param name="filter">The name of the filter to disable</param>
        public void DisableFilter(string filter)
        {
            AddFilterSetting(filter, new List<string> {"enable"}, "0");
        }

        /// <summary>
        ///     Shortcut method for enabling a filter.
        /// </summary>
        /// <param name="filter">The name of the filter to enable</param>
        public void EnableFilter(string filter)
        {
            AddFilterSetting(filter, new List<string> {"enable"}, "1");
        }

        /// <summary>
        ///     Converts the filter settings into a JSON string.
        /// </summary>
        /// <returns>String representation of the SendGrid headers</returns>
        public string JsonString()
        {
            return _settings.IsEmpty() ? "" : _settings.ToJson();
        }

        /// <summary>
        ///     This sets the categories for this email.  Statistics are stored on a per category
        ///     basis, so this can be useful for tracking on a per group basis.
        /// </summary>
        /// <param name="categories">categories applied to the message</param>
        public void SetCategories(IEnumerable<string> categories)
        {
            if (categories == null) return;
            var keys = new List<string> {"category"};
            _settings.AddArray(keys, categories);
        }

        /// <summary>
        ///     This sets the category for this email.  Statistics are stored on a per category
        ///     basis, so this can be useful for tracking on a per group basis.
        /// </summary>
        /// <param name="category">categories applied to the message</param>
        public void SetCategory(string category)
        {
            var keys = new List<string> {"category"};
            _settings.AddSetting(keys, category);
        }

        /// <summary>
        ///     This adds the "to" array to the X-SMTPAPI header so that multiple recipients
        ///     may be addressed in a single email. (but they each get their own email, instead of a single email with multiple TO:
        ///     addressees)
        /// </summary>
        /// <param name="addresses">List of email addresses</param>
        public void SetTo(IEnumerable<string> addresses)
        {
            _settings.AddArray(new List<string> {"to"}, addresses);
        }

        /// <summary>
        ///     This sets the ASM Group ID for this email.  You can find further documentation about ASM here:
        ///     https://sendgrid.com/docs/API_Reference/Web_API_v3/Advanced_Suppression_Manager/index.html
        /// </summary>
        /// <param name="id">ASM group applied to the message</param>
        public void SetASMGroupID(int id)
        {
            var keys = new List<string> { "asm_group_id" };
            _settings.AddSetting(keys, id);
        }

        #endregion
    }
}