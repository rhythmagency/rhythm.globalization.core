namespace Rhythm.Globalization.Core
{

    // Namespaces.
    using System.Web.Configuration;

    /// <summary>
    /// Settings for globalization.
    /// </summary>
    internal class Settings
    {

        #region Methods

        /// <summary>
        /// Should the default culture (e.g., "en-us") be excluded from the URL?
        /// </summary>
        /// <returns>
        /// True, if the default culture should be excluded from the URL; otherwise, false.
        /// </returns>
        public static bool ShouldExcludeDefaultCultureFromUrl()
        {
            var settingValue = WebConfigurationManager.AppSettings["Exclude Default Culture From URL"] ?? "false";
            return "true".Equals(settingValue, System.StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Returns the default culture.
        /// </summary>
        /// <returns>
        /// The default culture.
        /// </returns>
        public static string GetDefaultCulture()
        {
            return WebConfigurationManager.AppSettings["Default Culture"];
        }

        #endregion

    }

}