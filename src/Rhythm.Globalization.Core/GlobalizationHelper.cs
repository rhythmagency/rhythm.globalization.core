namespace Rhythm.Globalization.Core
{

    // Namespaces.
    using System.Web;

    /// <summary>
    /// Helps with globalizaton.
    /// </summary>
    public class GlobalizationHelper
    {

        #region Constants

        private const string CultureKey = "Rhythm Globalization Culture";

        #endregion

        #region Methods

        /// <summary>
        /// Stores the specified culture in the current HTTP context items.
        /// </summary>
        /// <param name="culture">
        /// The culture to store.
        /// </param>
        public static void SetCultureForCurrentRequest(string culture)
        {
            var context = HttpContext.Current;
            var items = context.Items;
            items[CultureKey] = culture;
        }

        /// <summary>
        /// Returns the culture stored in the current HTTP context items.
        /// </summary>
        /// <returns>
        /// The culture, or null.
        /// </returns>
        /// <remarks>
        /// This will not inspect the URL in the current HTTP request.
        /// </remarks>
        public static string GetCultureFromCurrentRequest()
        {
            var context = HttpContext.Current;
            var items = context.Items;
            return items.Contains(CultureKey)
                ? items[CultureKey] as string
                : null;
        }

        /// <summary>
        /// Gets the culture, either from the specified URL or the
        /// current HTTP context items.
        /// </summary>
        /// <param name="url">
        /// The URL to attempt to extract the culture from.
        /// </param>
        /// <returns>
        /// The culture (e.g., "es-mx").
        /// </returns>
        /// <remarks>
        /// This function will first attempt to extract the culture from the specified URL.
        /// If the URL does not incdicate the culture, it will attempt to exract the
        /// culture from the current HTTP context items.
        /// </remarks>
        public static string GetCulture(string url)
        {
            return UrlParsing.GetCultureFromUrl(url) ?? GetCultureFromCurrentRequest();
        }

        #endregion

    }

}