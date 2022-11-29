/*
 USTRAP
 EXTENSION CLASS
 EXTENDED STRING
 v1.0
 LAST EDITED: TUESDAY NOVEMBER 29, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

using System;

namespace UStrap.Extensions
{
    /// <summary>
    /// Extends the functionality of Strings
    /// </summary>
    public static class ExtendedString
    {
        #region GENERAL

        /// <summary>
        /// Returns the string with all instances of the given value removed.
        /// </summary>
        /// <param name="s">Reference to the string.</param>
        /// <param name="value">Substring to remove.</param>
        /// <returns>Filtered string.</returns>
        public static string Erase(this string s, string value)
        => s != null ? s.Replace(value, "") : string.Empty;

        /// <summary>
        /// Trims a string then splits it based on the seperators list.
        /// </summary>
        /// <param name="s">Reference to the string.</param>
        /// <param name="seperators">Characters to seperate the string upon.</param>
        /// <returns>Trimmed string that has been split</returns>
        public static string[] TrimAndSplit(this string s, params char[] seperators)
        => s != null ? s.Trim().Split(seperators) : new string[0];

        /// <summary>
        /// Trims a string and erases all instances of a value from that string.
        /// </summary>
        /// <param name="s">Reference to the string.</param>
        /// <param name="value">Value to be erased from the string.</param>
        /// <returns>Trimmed string with erased value</returns>
        public static string TrimAndErase(this string s, string value)
        => s.Trim().Erase(value);

        #endregion

        #region UI QUERIES

        /// <summary>
        /// Is the query tag selector a class (false means it's an ID).
        /// </summary>
        /// <param name="QueryTag">Reference to the query string</param>
        /// <returns>True if the query string starts with a period (classes)</returns>
        public static bool IsClass(this string QueryTag)
        => !string.IsNullOrWhiteSpace(QueryTag) && QueryTag.Trim()[0] == '.';

        /// <summary>
        /// Is the query tag selector an id (false means it's a class).
        /// </summary>
        /// <param name="QueryTag">Reference to the query string</param>
        /// <returns>True if the query string starts with a hashtag (ID)</returns>
        public static bool IsID(this string QueryTag)
        => !string.IsNullOrWhiteSpace(QueryTag) && QueryTag.Trim()[0] == '#';

        /// <summary>
        /// Validates that a query tag is a class or an ID.
        /// </summary>
        /// <param name="QueryTag">Reference to the query string</param>
        /// <returns>True if the query string starts with a period (.) or a hashtag (#)</returns>
        public static bool ValidateQueryString(this string QueryTag)
        => QueryTag.IsClass() || QueryTag.IsID();

        /// <summary>
        /// Given a query string of an element, returns a list of classes.
        /// </summary>
        /// <param name="QueryTag">Reference to the query string</param>
        /// <returns>The list of classes split by the period (.)</returns>
        public static string[] GetClassList(this string QueryTag)
        => QueryTag != null ? QueryTag.Split('.') : new string[0];

        /// <summary>
        /// Given a query, will perform the query action if the query is an ID.
        /// </summary>
        /// <param name="QueryTag">Reference to the query string</param>
        /// <param name="IDAction">Action to perform if query begins with a hashtag</param>
        public static void Query(this string QueryTag, Action<string> IDAction)
        {
            if (QueryTag.ValidateQueryString() && QueryTag.IsID())
                IDAction?.Invoke(QueryTag.TrimAndErase("#"));
        }

        /// <summary>
        /// Given a query, will perform the query action if the query is a class list.
        /// </summary>
        /// <param name="QueryTag">Reference to the query string</param>
        /// <param name="ClassAction">Action to perform if query begins with a period</param>
        public static void Query(this string QueryTag, Action<string[]> ClassAction)
        {
            if (QueryTag.ValidateQueryString() && QueryTag.IsClass())
                ClassAction?.Invoke(QueryTag.TrimAndSplit('.'));
        }

        /// <summary>
        /// Given a query, will perform the following actions based on whether the query is an ID or a class list.
        /// </summary>
        /// <param name="QueryTag">Reference to the query string</param>
        /// <param name="IDAction">Action to perform if query begins with a hashtag</param>
        /// <param name="ClassAction">Action to perform if query begins with a period</param>
        public static void Query(this string QueryTag, Action<string> IDAction, Action<string[]> ClassAction)
        {
            if (QueryTag.ValidateQueryString())
            {
                if(QueryTag.IsClass())
                    ClassAction?.Invoke(QueryTag.TrimAndSplit('.'));
                else
                    IDAction?.Invoke(QueryTag.TrimAndErase("#"));
            }
        }

        #endregion
    }
}