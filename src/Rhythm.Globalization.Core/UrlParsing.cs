namespace Rhythm.Globalization.Core
{

    // Namespaces.
    using System.Text.RegularExpressions;
    using RhythmUrlParsing = Parsing.Core.UrlParsing;

    /// <summary>
    /// Assists with parsing globalized URL's.
    /// </summary>
    public class UrlParsing
    {

        #region Properties

        /// <summary>
        /// The regex used to match the region in a URL.
        /// </summary>
        /// <remarks>
        /// Will match "us" in the URL "/en-us/some-path".
        /// </remarks>
        private static Regex RegionRegex { get; set; }

        /// <summary>
        /// The regex used to match the culture prefix (e.g., "/en-us") in a URL.
        /// </summary>
        private static Regex CulturePrefixRegex { get; set; }

        /// <summary>
        /// The regex used to match the culture (e.g., "en-us") in a URL.
        /// </summary>
        private static Regex CultureRegex { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor.
        /// </summary>
        static UrlParsing()
        {
            var options = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase;
            RegionRegex = new Regex(@"(?<=^/[a-z]{2}-)[a-z]{2}(?=((/.*)?|\?.*)$)", options);
            CulturePrefixRegex = new Regex(@"^/[a-z]{2}-[a-z]{2}(?=((/.*)?|\?.*)$)", options);
            CultureRegex = new Regex(@"(?<=^/)[a-z]{2}-[a-z]{2}(?=((/.*)?|\?.*)$)", options);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Extracts the region from the specified URL.
        /// </summary>
        /// <param name="url">
        /// The URL (e.g., "http://www.rhythmagency.com/en-us/some-path").
        /// </param>
        /// <returns>
        /// The region (e.g., "us").
        /// </returns>
        /// <remarks>
        /// The URL doesn't need to include the domain.
        /// </remarks>
        public static string GetRegionFromUrl(string url)
        {
            var path = RhythmUrlParsing.GetPathFromUrl(url);
            var region = RegionRegex.Match(path)?.Value;
            if (Settings.ShouldExcludeDefaultCultureFromUrl() && string.IsNullOrEmpty(region))
            {
                var culture = Settings.GetDefaultCulture();
                if (!string.IsNullOrEmpty(culture) && culture.Length >= 2)
                {
                    region = culture.Substring(0, 2);
                }
            }
            var hasRegion = !string.IsNullOrWhiteSpace(region);
            return hasRegion
                ? region.ToLower()
                : null;
        }

        /// <summary>
        /// Returns the path for a URL, minus the culture portion, and minus the query string.
        /// </summary>
        /// <param name="url">
        /// The URL (e.g., "http://www.rhythmagency.com/en-us/some-path?some=path").
        /// </param>
        /// <returns>
        /// The path, without the culture (e.g., "/some-path").
        /// </returns>
        public static string GetPathWithoutCulture(string url)
        {
            var path = RhythmUrlParsing.GetPathFromUrl(url);
            var isMatch = CulturePrefixRegex.IsMatch(path);
            var newPath = isMatch
                ? CulturePrefixRegex.Replace(path, string.Empty)
                : path;
            return newPath;
        }

        /// <summary>
        /// Returns the path for a URL, minus the culture portion.
        /// </summary>
        /// <param name="url">
        /// The URL (e.g., "http://www.rhythmagency.com/en-us/some-path?some=path").
        /// </param>
        /// <returns>
        /// The path, without the culture (e.g., "/some-path?some=path").
        /// </returns>
        public static string GetPathAndQueryWithoutCulture(string url)
        {
            var path = RhythmUrlParsing.GetPathAndQueryFromUrl(url);
            var isMatch = CulturePrefixRegex.IsMatch(path);
            var newPath = isMatch
                ? CulturePrefixRegex.Replace(path, string.Empty)
                : path;
            return newPath;
        }

        /// <summary>
        /// Extracts the culture from the specified URL.
        /// </summary>
        /// <param name="url">
        /// The URL (e.g., "http://www.rhythmagency.com/en-us/some-path").
        /// </param>
        /// <returns>
        /// The culture (e.g., "en-us").
        /// </returns>
        /// <remarks>
        /// The URL doesn't need to include the domain.
        /// </remarks>
        public static string GetCultureFromUrl(string url)
        {
            var path = RhythmUrlParsing.GetPathFromUrl(url);
            var culture = CultureRegex.Match(path)?.Value;
            if (Settings.ShouldExcludeDefaultCultureFromUrl() && string.IsNullOrEmpty(culture))
            {
                culture = Settings.GetDefaultCulture();
            }
            var hasCulture = !string.IsNullOrWhiteSpace(culture);
            return hasCulture
                ? culture.ToLower()
                : null;
        }

        /// <summary>
        /// Extracts the language from the specified URL.
        /// </summary>
        /// <param name="url">
        /// The URL (e.g., "http://www.rhythmagency.com/en-us/some-path").
        /// </param>
        /// <returns>
        /// The language (e.g., "en").
        /// </returns>
        /// <remarks>
        /// The URL doesn't need to include the domain.
        /// </remarks>
        public static string GetLanguageFromUrl(string url)
        {
            var culture = GetCultureFromUrl(url) ?? string.Empty;
            return culture.Length < 2
                ? null
                : culture.Substring(0, 2);
        }

        /// <summary>
        /// Prefixes a culture to a URL.
        /// </summary>
        /// <param name="url">
        /// The URL (e.g., "/about/company").
        /// </param>
        /// <param name="culture">
        /// The culture code (e.g., "en-us").
        /// </param>
        /// <returns>
        /// The URL with a prefixed culture (e.g., "/en-us/about/company").
        /// </returns>
        public static string PrefixCultureToUrl(string url, string culture)
        {
            var path = RhythmUrlParsing.GetPathAndQueryFromUrl(url) ?? string.Empty;
            if (path == "/" || path.StartsWith("/?"))
            {
                path = path.Substring(1);
            }
            path = $"/{culture}{path}";
            return path;
        }

        #endregion

    }

}