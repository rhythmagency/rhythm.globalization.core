# Introduction

A library of tools for globalization (handling multiple languages and regions).
The primary classes are `GlobalizationHelper` and `UrlParsing`.

Refer to the [generated documentation](docs/generated.md) for more details.

# Installation

Install with NuGet. Search for "Rhythm.Globalization.Core".

# Overview

The `UrlParsing` class has the following methods:

* **GetRegionFromUrl** Extracts a region (e.g., "us") from a URL (e.g., "/en-us/some-path").
* **GetPathWithoutCulture** Gets a path portion (e.g., "/some-path") of a path with a culture (e.g., "/en-us/some-path"). Excludes the query string.
* **GetPathAndQueryWithoutCulture** Gets a path portion (e.g., "/some-path?some=path") of a path with a culture (e.g., "/en-us/some-path?some=path"). Includes the query string.
* **GetCultureFromUrl** Extracts a culture (e.g., "en-us") from a URL (e.g., "/en-us/some-path").
* **GetLanguageFromUrl** Extracts a language (e.g., "en") from a URL (e.g., "/en-us/some-path").
* **PrefixCultureToUrl** Prefixes a culture (e.g., "en-us") to a URL (e.g., "/some-path") to form the full URL (e.g., "/en-us/some-path")

The `GlobalizationHelper` class has the following methods:

* **SetCultureForCurrentRequest** Sets the culture for the current request (can be useful in API methods in which the culture is not indicated by the incoming URL).
* **GetCultureFromCurrentRequest** Returns the culture that was previously set on the current request.
* **GetCulture** Returns the culture from the specified URL (will fallback to the culture set by the current request).

# Maintainers

To create a new release to NuGet, see the [NuGet documentation](docs/nuget.md).