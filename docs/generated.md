# Rhythm.Globalization.Core

<table>
<tbody>
<tr>
<td><a href="#globalizationhelper">GlobalizationHelper</a></td>
<td><a href="#urlparsing">UrlParsing</a></td>
</tr>
</tbody>
</table>


## GlobalizationHelper

Helps with globalizaton.

### GetCulture(url)

Gets the culture, either from the specified URL or the current HTTP context items.

| Name | Description |
| ---- | ----------- |
| url | *System.String*<br>The URL to attempt to extract the culture from. |

#### Returns

The culture (e.g., "es-mx").

#### Remarks

This function will first attempt to extract the culture from the specified URL. If the URL does not incdicate the culture, it will attemp to exract the culture from the current HTTP context items.

### GetCultureFromCurrentRequest

Returns the culture stored in the current HTTP context items.

#### Returns

The culture, or null.

#### Remarks

This will not inspect the URL in the current HTTP request.

### SetCultureForCurrentRequest(culture)

Stores the specified culture in the current HTTP context items.

| Name | Description |
| ---- | ----------- |
| culture | *System.String*<br>The culture to store. |


## UrlParsing

Assists with parsing globalized URL's.

### #cctor

Static constructor.

### CulturePrefixRegex

The regex used to match the culture prefix (e.g., "/en-us") in a URL.

### CultureRegex

The regex used to match the culture (e.g., "en-us") in a URL.

### GetCultureFromUrl(url)

Extracts the culture from the specified URL.

| Name | Description |
| ---- | ----------- |
| url | *System.String*<br>The URL (e.g., "http://www.rhythmagency.com/en-us/some-path"). |

#### Returns

The culture (e.g., "en-us").

#### Remarks

The URL doesn't need to include the domain.

### GetLanguageFromUrl(url)

Extracts the language from the specified URL.

| Name | Description |
| ---- | ----------- |
| url | *System.String*<br>The URL (e.g., "http://www.rhythmagency.com/en-us/some-path"). |

#### Returns

The language (e.g., "en").

#### Remarks

The URL doesn't need to include the domain.

### GetPathAndQueryWithoutCulture(url)

Returns the path for a URL, minus the culture portion.

| Name | Description |
| ---- | ----------- |
| url | *System.String*<br>The URL (e.g., "http://www.rhythmagency.com/en-us/some-path?some=path"). |

#### Returns

The path, without the culture (e.g., "/some-path?some=path").

### GetPathWithoutCulture(url)

Returns the path for a URL, minus the culture portion, and minus the query string.

| Name | Description |
| ---- | ----------- |
| url | *System.String*<br>The URL (e.g., "http://www.rhythmagency.com/en-us/some-path?some=path"). |

#### Returns

The path, without the culture (e.g., "/some-path").

### GetRegionFromUrl(url)

Extracts the region from the specified URL.

| Name | Description |
| ---- | ----------- |
| url | *System.String*<br>The URL (e.g., "http://www.rhythmagency.com/en-us/some-path"). |

#### Returns

The region (e.g., "us").

#### Remarks

The URL doesn't need to include the domain.

### PrefixCultureToUrl(url, culture)

Prefixes a culture to a URL.

| Name | Description |
| ---- | ----------- |
| url | *System.String*<br>The URL (e.g., "/about/company"). |
| culture | *System.String*<br>The culture code (e.g., "en-us"). |

#### Returns

The URL with a prefixed culture (e.g., "/en-us/about/company").

### RegionRegex

The regex used to match the region in a URL.

#### Remarks

Will match "us" in the URL "/en-us/some-path".
