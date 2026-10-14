using kiemtragiuaki.Localization;
using kiemtragiuaki.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace kiemtragiuaki.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => RedirectToAction("Index", "Dishes");

        // Switches the UI language and returns to the same page (query string kept, so filters are preserved).
        [HttpGet]
        public IActionResult SetLanguage(string culture, string? returnUrl)
        {
            if (culture != Translations.English && culture != Translations.Vietnamese)
                culture = Translations.English;

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true, Path = "/" });

            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                returnUrl = Url.Action("Index", "Dishes")!;
            return LocalRedirect(returnUrl);
        }

        [Route("Home/StatusCode/{code:int}")]
        public new IActionResult StatusCode(int code)
        {
            Response.StatusCode = code;
            ViewData["Message"] = code == 404 ? Translations.Get("Err_PageNotFound") : Translations.Get("Err_Generic");
            return View("NotFound");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
