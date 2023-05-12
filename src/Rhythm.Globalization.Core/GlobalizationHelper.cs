using Microsoft.AspNetCore.Http;
using System;

namespace Rhythm.Globalization.Core
{

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
        /// <param name="defaultCulture">
        /// The fallback default culture.
        /// </param>
        /// <param name="shouldExcludeDefaultCultureFromUrl">
        /// Should the default culture be excluded from the URL?
        /// </param>
        /// <param name="context">
        /// The current HttpContext.
        /// </param>
        public static void SetCultureForCurrentRequest(
            string culture, 
            string defaultCulture,
            bool shouldExcludeDefaultCultureFromUrl,
            HttpContext context)
        {
            if (string.IsNullOrEmpty(culture))
            {
                throw new ArgumentException($"'{nameof(culture)}' cannot be null or empty.", nameof(culture));
            }

            if (string.IsNullOrEmpty(defaultCulture))
            {
                throw new ArgumentException($"'{nameof(defaultCulture)}' cannot be null or empty.", nameof(defaultCulture));
            }

            if (shouldExcludeDefaultCultureFromUrl && string.IsNullOrEmpty(culture))
            {
                culture = defaultCulture;
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var items = context.Items;
            items[CultureKey] = culture;
        }

        /// <summary>
        /// Returns the culture stored in the current HTTP context items.
        /// </summary>
        /// <returns>
        /// The culture, or null.
        /// </returns>
        /// <param name="defaultCulture">
        /// The fallback default culture.
        /// </param>
        /// <param name="shouldExcludeDefaultCultureFromUrl">
        /// Should the default culture be excluded from the URL?
        /// </param>
        /// <param name="context">
        /// The current HttpContext.
        /// </param>
        /// <remarks>
        /// This will not inspect the URL in the current HTTP request.
        /// </remarks>
        public static string GetCultureFromCurrentRequest(
            bool shouldExcludeDefaultCultureFromUrl,
            string defaultCulture,
            HttpContext context)
        {
            var items = context.Items;
            var culture = items.ContainsKey(CultureKey)
                ? items[CultureKey] as string
                : null;
            if (shouldExcludeDefaultCultureFromUrl && string.IsNullOrEmpty(culture))
            {
                culture = defaultCulture;
            }
            return culture;
        }

        /// <summary>
        /// Gets the culture, either from the specified URL or the
        /// current HTTP context items.
        /// </summary>
        /// <param name="url">
        /// The URL to attempt to extract the culture from.
        /// </param>
        /// <param name="defaultCulture">
        /// The fallback default culture.
        /// </param>
        /// <param name="shouldExcludeDefaultCultureFromUrl">
        /// Should the default culture be excluded from the URL?
        /// </param>
        /// <param name="context">
        /// The current HttpContext.
        /// </param>
        /// <returns>
        /// The culture (e.g., "es-mx").
        /// </returns>
        /// <remarks>
        /// This function will first attempt to extract the culture from the specified URL.
        /// If the URL does not incdicate the culture, it will attempt to exract the
        /// culture from the current HTTP context items.
        /// </remarks>
        public static string GetCulture(
            string url, 
            string defaultCulture,
            bool shouldExcludeDefaultCultureFromUrl,
            HttpContext context)
        {
            var culture = UrlParsing.GetCultureFromUrl(url, defaultCulture, shouldExcludeDefaultCultureFromUrl) 
                ?? GetCultureFromCurrentRequest(shouldExcludeDefaultCultureFromUrl, defaultCulture, context);
            if (shouldExcludeDefaultCultureFromUrl && string.IsNullOrEmpty(culture))
            {
                culture = defaultCulture;
            }
            return culture;
        }

        #endregion

    }

}