using Dto;
using DTO;
using FluentValidation;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using GuidePanel.FluentValidation.Auth;
using GuidePanel.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IApiHandler, ApiHandler>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<ICookie, Cookie>();
builder.Services.AddSingleton<IAuthorizationHandler, RolePermissionHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Auth/Login";
    opt.LogoutPath = "/Auth/Logout";
    opt.AccessDeniedPath = "/Home/Index";
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.Cookie.Name = "JwtCookie";
    opt.Cookie.Domain = builder.Configuration.GetValue<string>("DomainUrl");

    //opt.ExpireTimeSpan = TimeSpan.FromDays(2);
    //opt.Cookie.MaxAge = TimeSpan.FromDays(2);
    opt.Events = new CookieAuthenticationEvents
    {
        OnRedirectToAccessDenied = context =>
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Response.Redirect("/Auth/Unauthorized");
            }
            else
            {
                context.Response.Redirect("/Auth/Login");
            }
            return Task.CompletedTask;
        }

    };
});
builder.Services.AddAuthorization();
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();

//var controllerNames = ControllerCollector.GetConrollerNames();
//IApiHandler apiHandler = builder.Services.BuildServiceProvider().GetRequiredService<IApiHandler>();
//apiHandler.PostApi<CustomResponseNullDto>(new ControllerNamesPostDto
//{
//    ControllerNames = controllerNames,
//}, $"{builder.Configuration["BaseUrl"]}Auth/PostControllerNames");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UsePathBase("/panel");

Resource.Url = builder.Configuration.GetValue<string>("ResourceUrl");
Resource.ImagePath = builder.Configuration.GetValue<string>("ImagePath");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}/{languageId?}");


app.Run();
