using Advertisements.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Drawing;
using QuestPDF.Infrastructure;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
    // Environment variables and command-line arguments keep their override priority.
    builder.Configuration.AddEnvironmentVariables();
    builder.Configuration.AddCommandLine(args);
}

//Community License for QuestPDF (free)
QuestPDF.Settings.License = LicenseType.Community;

FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-Black.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-BlackItalic.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-Bold.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-BoldItalic.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-ExtraBold.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-ExtraBoldItalic.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-ExtraLight.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-ExtraLightItalic.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-Italic.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-Light.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-LightItalic.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-Medium.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-MediumItalic.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-Regular.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-SemiBold.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-SemiBoldItalic.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-Thin.ttf"));
FontManager.RegisterFont(File.OpenRead("wwwroot/assets/fonts/poppins/Poppins-ThinItalic.ttf"));

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IReposClients, ReposClients>();
builder.Services.AddTransient<IReposCreators, ReposCreators>();
builder.Services.AddTransient<IReposCampaigns, ReposCampaigns>();
builder.Services.AddTransient<IReposMetrics, ReposMetrics>();
builder.Services.AddTransient<IReposIndustries, ReposIndustries>();
builder.Services.AddTransient<IReposTransactions, ReposTransactions>();
builder.Services.AddTransient<IReposEntities, ReposEntities>();
builder.Services.AddTransient<IReposProducts, ReposProducts>();
builder.Services.AddTransient<IReposCampaignCreators, ReposCampaignCreators>();
builder.Services.AddTransient<IServiceWebhookMessages, ServiceWebhookMessages>();
builder.Services.AddTransient<IReposCountries, ReposCountries>();
builder.Services.AddTransient<IReposStates, ReposStates>();

var app = builder.Build();

// Configurar cultura
var defaultCulture = new CultureInfo("es-BO"); // o "es-ES", seg�n corresponda
defaultCulture.NumberFormat.NumberDecimalSeparator = "."; // Defining my preferrence for number
defaultCulture.NumberFormat.CurrencyDecimalSeparator = ".";
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = new List<CultureInfo> { defaultCulture },
    SupportedUICultures = new List<CultureInfo> { defaultCulture }
};

app.UseRequestLocalization(localizationOptions);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

