using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using CloudinaryDotNet;
using FluentValidation.AspNetCore;
using RobloxWithPinoo_UI.Services.AccountService;
using RobloxWithPinoo_UI.Services.ActivationCodeService;
using RobloxWithPinoo_UI.Services.AdminDashboardService;
using RobloxWithPinoo_UI.Services.AuthService;
using RobloxWithPinoo_UI.Services.CardService;
using RobloxWithPinoo_UI.Services.CloudinaryService;
using RobloxWithPinoo_UI.Services.ContactFormService;
using RobloxWithPinoo_UI.Services.DocArticleService;
using RobloxWithPinoo_UI.Services.DocCategoryService;
using RobloxWithPinoo_UI.Services.UserService;
using RobloxWithPinoo_UI.Validators;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

Account account = new Account(
    builder.Configuration["Cloudinary:CloudName"], // Cloud adý
    builder.Configuration["Cloudinary:ApiKey"],    // API anahtarý
    builder.Configuration["Cloudinary:ApiSecret"]  // API þifresi
);

builder.Services.AddNotyf(config => { config.DurationInSeconds = 3; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });

builder.Services.AddSingleton(new Cloudinary(account));

builder.Services.AddControllersWithViews()
        .AddFluentValidation(opt =>
        {
            opt.RegisterValidatorsFromAssemblyContaining<CreateDocArticleValidator>();
            opt.DisableDataAnnotationsValidation = true;
            opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
        });
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddHttpClient();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IDocCategoryService, DocCategoryService>();
builder.Services.AddScoped<IDocArticleService, DocArticleService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IActivationCodeService, ActivationCodeService>();
builder.Services.AddScoped<IAdminDashboardService, AdminDashboardService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<IContactFormService, ContactFormService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
	endpoints.MapAreaControllerRoute(
		name: "user",
		areaName: "UserDashboard",
		pattern: "UserDashboard/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapAreaControllerRoute(
        name: "admin",
        areaName: "AdminDashboard",
        pattern: "AdminDashboard/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.UseNotyf();

app.Run();
