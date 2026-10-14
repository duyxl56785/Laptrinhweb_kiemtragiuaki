using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.Extensions.WebEncoders;
using kiemtragiuaki.Data;
using kiemtragiuaki.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Output Vietnamese characters as-is instead of &#x..; entities.
builder.Services.Configure<WebEncoderOptions>(o => o.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All));

// Localization: a dictionary-backed localizer (see Localization/Translations.cs).
builder.Services.AddSingleton<IStringLocalizerFactory, DictionaryStringLocalizerFactory>();
builder.Services.AddLocalization();

builder.Services.AddControllersWithViews(options =>
    {
        // Model binding messages are looked up at request time, so they follow the current UI culture.
        var m = options.ModelBindingMessageProvider;
        m.SetValueMustBeANumberAccessor(f => Translations.Get("Bind_ValueMustBeNumber", f));
        m.SetAttemptedValueIsInvalidAccessor((v, f) => Translations.Get("Bind_ValueInvalid", v, f));
        m.SetValueMustNotBeNullAccessor(_ => Translations.Get("Bind_ValueRequired"));
        m.SetMissingBindRequiredValueAccessor(_ => Translations.Get("Bind_ValueRequired"));
        m.SetMissingKeyOrValueAccessor(() => Translations.Get("Bind_ValueRequired"));
        m.SetUnknownValueIsInvalidAccessor(f => Translations.Get("Bind_ValueMustBeNumber", f));
        m.SetValueIsInvalidAccessor(v => Translations.Get("Bind_AttemptedValueInvalid", v));
        m.SetNonPropertyAttemptedValueIsInvalidAccessor(v => Translations.Get("Bind_AttemptedValueInvalid", v));
        m.SetNonPropertyUnknownValueIsInvalidAccessor(() => Translations.Get("Bind_NonPropertyValueMustBeNumber"));
        m.SetNonPropertyValueMustBeANumberAccessor(() => Translations.Get("Bind_NonPropertyValueMustBeNumber"));
    })
    .AddDataAnnotationsLocalization(o =>
        o.DataAnnotationLocalizerProvider = (_, factory) => factory.Create(typeof(SharedResource)));

// Both cultures use invariant number formatting ("12.5") so <input type="number"> and model binding agree.
static CultureInfo MakeCulture(string name)
{
    var c = (CultureInfo)CultureInfo.GetCultureInfo(name).Clone();
    c.NumberFormat = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
    return c;
}
var supportedCultures = new[] { MakeCulture(Translations.English), MakeCulture(Translations.Vietnamese) };

builder.Services.Configure<RequestLocalizationOptions>(o =>
{
    o.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
    o.SupportedCultures = supportedCultures;
    o.SupportedUICultures = supportedCultures;
    // Only the cookie decides the language, so first visit is always English (browser language ignored).
    o.RequestCultureProviders = new List<IRequestCultureProvider> { new CookieRequestCultureProvider() };
});

var app = builder.Build();

// Apply migrations (creates database BIT240081_MID and seed data on first run).
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRequestLocalization();
app.UseStatusCodePagesWithReExecute("/Home/StatusCode/{0}");

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dishes}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
