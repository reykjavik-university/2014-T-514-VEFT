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

#Multiple languages in web API

To add this feature to a web API service, a new MessageHandler can be created. The message handler validates the request header for localized languages

```c#
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CoursesAPI
{
    public class LanguageMessageHandler : DelegatingHandler
    {
        private const string LangfrFR = "fr-FR";
        private const string LangesCL = "es-CL";
        private const string LangenGB = "en-GB";

        private readonly List<string> _supportedLanguages = new List<string> { LangfrFR, LangesCL, LangenGB };

        private bool SetHeaderIfAcceptLanguageMatchesSupportedLanguage(HttpRequestMessage request)
        {
            foreach (var lang in request.Headers.AcceptLanguage)
            {
                if (_supportedLanguages.Contains(lang.Value))
                {
                    SetCulture(request, lang.Value);
                    return true;
                }
            }

            return false;
        }

        private bool SetHeaderIfGlobalAcceptLanguageMatchesSupportedLanguage(HttpRequestMessage request)
        {
            foreach (var lang in request.Headers.AcceptLanguage)
            {
                var globalLang = lang.Value.Substring(0, 2);
                if (_supportedLanguages.Any(t => t.StartsWith(globalLang)))
                {
                    SetCulture(request, _supportedLanguages.FirstOrDefault(i => i.StartsWith(globalLang)));
                    return true;
                }
            }

            return false;
        }

        private void SetCulture(HttpRequestMessage request, string lang)
        {
            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(lang));
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
        }

        protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!SetHeaderIfAcceptLanguageMatchesSupportedLanguage(request))
            {
                // Whoops no localization found. Lets try Globalisation
                if (!SetHeaderIfGlobalAcceptLanguageMatchesSupportedLanguage(request))
                {
                    // no global or localization found
                    SetCulture(request, LangenGB);
                }
            }

            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}
```

The LanguageMessageHandler class is then added to the global config for the Web API

```c#
using System.Web.Http;
 
namespace WebAPILocalization
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new LanguageMessageHandler());
        }
    }
}
```

The next step is to add the translations to Resource files in the application. 
This can be done in various ways. The code above supports fr-FR, es-CL and the default language en-GB. **Resource files don’t have to be used, translations could be in a database.**


The Model class LanguageViewModel used the translations for its validation. If a required validation exception occurs, the validation message will be displayed in the localized culture.

```c#
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoursesAPI.Models
{
    public class LanguageViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), AllowEmptyStrings = false, ErrorMessageResourceName = "NameRequired")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources), AllowEmptyStrings = false, ErrorMessageResourceName = "DescriptionRequired")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources), AllowEmptyStrings = false, ErrorMessageResourceName = "TimestampRequired")]
        public DateTime Timestamp { get; set; }
    }
}
```

To test the implemented cultures we could write the following method in our Web API.

```c#
[HttpGet]
[Route("")]
public IEnumerable<LanguageViewModel> Get()
{
    var languageViewModel = new LanguageViewModel
    {
        Description = Resources.Resources.Description,
        Timestamp = DateTime.UtcNow,
        Name = Resources.Resources.Name
    };
    return new[] { languageViewModel };
}
```
The action controller does not required any specific language methods. The get works for all cultures and the test result of a Get should return the correct string for a given localization.
For example, if you set the prefered language of your browser to fr-FR of french-France, a Get test response in JSON should look like this: 
[{"Name":"Name fr-FR","Description":"Description fr-FR","Timestamp":"2014-09-15T14:06:38.3202911Z"}]


To test translations for validation we could can create a action controller that uses the ModelState to validate the create object request in the Post method. If the Model is invalid, a HttpError object is created from the ModelState. The BadRequest(ModelState) provided by the framework cannot be used, because this method results in non-localized strings.

```c#
[HttpPost]
[Route("")]
public HttpResponseMessage Post(LanguageViewModel model)
{
    if (!ModelState.IsValid)
    {
        HttpError error = GetErrors(ModelState, true);
        return Request.CreateResponse(HttpStatusCode.BadRequest, error);
    }
    return new HttpResponseMessage(HttpStatusCode.Created);
}
```

Because the BadRequest cannot be used, the HttpError is created in a private method.

```c#
private HttpError GetErrors(IEnumerable<KeyValuePair<string, ModelState>> modelState, bool includeErrorDetail)
{
    var modelStateError = new HttpError();
    foreach (KeyValuePair<string, ModelState> keyModelStatePair in modelState)
    {
        string key = keyModelStatePair.Key;
        ModelErrorCollection errors = keyModelStatePair.Value.Errors;
        if (errors != null && errors.Count > 0)
        {
            IEnumerable<string> errorMessages = errors.Select(error =>
            {
                if (includeErrorDetail && error.Exception != null)
                {
                    return error.Exception.Message;
                }
                return String.IsNullOrEmpty(error.ErrorMessage) ? "ErrorOccurred" : error.ErrorMessage;
            }).ToArray();
            modelStateError.Add(key, errorMessages);
        }
    }

    return modelStateError;
}
```

A Post test response in JSON for fr-FR should then look like this: 
{"model.Name":["Name is required fr-FR"],"model.Description":["Description is required fr-FR"]}



** Related link and material **
* [Web Api Localization](http://damienbod.wordpress.com/2014/03/20/web-api-localization/)
* [ASP.NET Internationalization](http://www.asp.net/mvc/overview/internationalization)
## Validation

