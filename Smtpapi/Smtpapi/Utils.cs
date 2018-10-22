namespace SendGrid.SmtpApi
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    public class Utils
    {
        /// <summary>
        ///     Serializes an object
        /// </summary>
        /// <typeparam name="T">Type of the object to serialize</typeparam>
        /// <param name="objectToSerialize">Object to serialize</param>
        /// <returns>A JSON string representation of the object</returns>
        public static string Serialize<T>(T objectToSerialize)
        {
            if (objectToSerialize == null)
            {
                throw new ArgumentNullException("A key or value in your X-SMTPAPI header is null.");
            }

            return JsonConvert.SerializeObject(objectToSerialize);
        }

        /// <summary>
        ///     Serializes a dictionary
        /// </summary>
        /// <param name="dictionaryToSerialize">Dictionary to serialize</param>
        /// <returns>A JSON string representation of the dictionary</returns>
        public static string SerializeDictionary(IDictionary<string, string> dictionaryToSerialize)
        {
            return JsonConvert.SerializeObject(dictionaryToSerialize);
        }

        /// <summary>
        ///     ASCII escapes non-ASCII characters
        /// </summary>
        /// <param name="value">The string to escape</param>
        /// <returns>Escaped string</returns>
        public static string EncodeNonAsciiCharacters(string value)
        {
            var sb = new StringBuilder();
            foreach (char c in value)
            {
                if (c > 127)
                {
                    // This character is too big for ASCII
                    string encodedValue = "\\u" + ((int)c).ToString("x4");
                    sb.Append(encodedValue);
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Convert a DateTime to a UNIX Epoch Timestamp
        /// </summary>
        /// <param name="dateTime">Date to convert to timestamp</param>
        /// <returns>Timestamp</returns>
        public static int DateTimeToUnixTimestamp(DateTime dateTime)
        {
            var span = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return (int)span.TotalSeconds;
        }
    }
}