using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using GuideWeb.Helpers.Routing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.IO;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
    options.EnableActionInvokers = true;
});

builder.Services.AddControllersWithViews(opt =>
{
    opt.CacheProfiles.Add(AppConstants.Cache60, new CacheProfile()
    {
        Duration = 60,
        Location = ResponseCacheLocation.Any,
        NoStore = false,
    });
    opt.CacheProfiles.Add(AppConstants.Cache120, new CacheProfile()
    {
        Duration = 120,
        Location = ResponseCacheLocation.Client,
        NoStore = false,
    });
});

builder.Services.AddHttpContextAccessor();
//builder.Services.Configure<GoogleRecaptchaConfiguration>(builder.Configuration.GetSection("GoogleRecaptcha"));

builder.Services.AddScoped<IGoogleRecaptchaService, GoogleRecaptchaService>();


builder.Services.AddScoped<IApiHandler, ApiHandler>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<ICookie, Cookie>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Auth/Index";
    opt.LogoutPath = "/Auth/Logout";
    opt.AccessDeniedPath = "/Home/Index";
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.Cookie.Name = "JwtCookie";
    opt.ExpireTimeSpan = TimeSpan.FromDays(2);
    opt.Cookie.MaxAge = TimeSpan.FromDays(2);
});
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
//builder.Services.AddResponseCaching();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

Resource.Url = builder.Configuration.GetValue<string>("ResourceUrl");
Resource.ImagePath = builder.Configuration.GetValue<string>("ImagePath");

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseMvc(routes =>
{
    //routes.MapMiddlewareGet(template: "{*}/{preSlug?}/{slug}/{id?}", (applicationBuilder) =>
    //{
    //    applicationBuilder.UseRouter(new MyRouteHandler(routes.DefaultHandler, app.Configuration));
    //});
    //routes.MapMiddlewareGet(template: "{path:regex(.*\\.xml$)}", (builder) =>
    //{
    //    builder.UseRouter(new MyRouteHandler(routes.DefaultHandler, app.Configuration));
    //});
    //routes.MapMiddlewareGet(template: "{preSlug?}/{slug}", (builder) =>
    //{
    //    builder.UseRouter(new MyRouteHandler(routes.DefaultHandler, app.Configuration));
    //});  
    routes.MapMiddlewareGet(template: "{*all}", (builder) =>
    {
        builder.UseRouter(new MyRouteHandler(routes.DefaultHandler, app.Configuration));
    });

});
app.UseResponseCaching();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


});

app.Run();
