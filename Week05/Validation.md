##Globalization and Localization in ASP.NET

#Introduction

Internationalization involves Globalization and Localization. Globalization is the process of designing applications that support different cultures. Localization is the process of customizing an application for a given culture.
The format for the culture name is "<languagecode2>-<country/regioncode2>", where <languagecode2> is the language code and <country/regioncode2> is the subculture code. Examples include es-CL for Spanish (Chile) and en-US for English (United States).
Anyway, Internationalization is often abbreviated to "I18N". The abbreviation takes the first and last letters and the number of letters between them, so 18 stands for the number of letters between the first "I" and the last "N". The same applies to Globalization (G11N), and Localization (L10N).

ASP.NET keeps track of two culture values, the [Culture](http://msdn.microsoft.com/en-us/library/system.web.ui.page.culture.aspx) and [UICulture](http://msdn.microsoft.com/en-us/library/system.web.ui.page.uiculture.aspx). The culture value determines the results of culture-dependent functions, such as the date, number, and currency formatting. The UICulture determines which resources are to be loaded for the page by the ResourceManager. The ResourceManager simply looks up culture-specific resources that is determined by CurrentUICulture. Every thread in .NET has CurrentCulture and CurrentUICulture objects. So ASP.NET inspects these values when rendering culture-dependent functions. For example, if current thread's culture (CurrentCulture) is set to "en-US" (English, United States), DateTime.Now.ToLongDateString() shows "Saturday, January 08, 2011", but if CurrentCulture is set to "es-CL" (Spanish, Chile) the result will be "sábado, 08 de enero de 2011".

Terms used:
* Globalization (G11N): The process of making an application support different languages and regions.
* Localization (L10N): The process of customizing an application for a given language and region.
* Internationalization (I18N): Describes both globalization and localization.
* Culture: It is a language and, optionally, a region.
* Locale: A locale is the same as a culture.
* Neutral culture: A culture that has a specified language, but not a region. (e.g. "en", "es")
* Specific culture: A culture that has a specified language and region. (e.g. "en-US", "en-GB", "es-CL")

#Detect user´s language

On each HTTP request, there is a header field called Accept-Language which determines which languages the user’s browser supports:
        Accept-Language: en-us,en;q=0.5
This means that my browser prefers English (United States), but it can accept other types of English. The "q" parameter indicates an estimate of the user’s preference for that language. You can control the list of languages using your web browser.

## Validation

