using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.StaticValues;
using Data;
using GuideAPI;
using GuideAPI.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Service.Dependencies;
using Service.Mail;
using Service.Payment;
using Service.Security.JWT;
using Service.Services;
using System;
using System.Reflection;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        ConfigurationManager configuration = builder.Configuration;
        IWebHostEnvironment environment = builder.Environment;

        builder.Services.AddDbContext<Context>(x =>
        {
            x.UseSqlServer(configuration.GetConnectionString("SqlConnection"), opt =>
            {
                opt.MigrationsAssembly(Assembly.GetAssembly(typeof(Context)).GetName().Name);
            });
        });
        builder.Services.Configure<YKConfig>(builder.Configuration.GetSection("YapiKredi"));
        //builder.Services.AddCors(options =>
        //{
        //    options.AddDefaultPolicy(
        //        policy =>
        //        {
        //            policy.AllowAnyOrigin()
        //                .AllowAnyHeader()
        //                .AllowAnyMethod();
        //        });
        //});

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IGoogleRecaptchaService, GoogleRecaptchaService>();

        builder.Services.AddDependencies(configuration);

        builder.Services.AddControllers(opt =>
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
                Location = ResponseCacheLocation.Any,
                NoStore = false,
            });
        });
        builder.Services.AddHttpClient();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<Client>();

        builder.Services.AddResponseCaching();

        builder.Services.Configure<JwtSettings>(configuration.GetSection("TokenOptions"));
        builder.Services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration.GetSection("TokenOptions")["Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration.GetSection("TokenOptions")["Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("TokenOptions")["SecurityKey"])),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

        });



        //builder.Services.AddQuartz(q =>
        //{
        //    q.UseMicrosoftDependencyInjectionJobFactory();
        //    // Just use the name of your job that you created in the Jobs folder.
        //    var jobKey = new JobKey("SendEmailJob");
        //    q.AddJob<RecurringEmailJob>(opts => opts.WithIdentity(jobKey));

        //    q.AddTrigger(opts => opts
        //        .ForJob(jobKey)
        //        .WithIdentity("SendEmailJob-trigger")
        //        .WithSimpleSchedule(x => x
        //                    .WithIntervalInSeconds(60) // Ýþin kaç saniyede bir çalýþacaðýný belirttim 60
        //                    .RepeatForever())
        //        .WithCronSchedule("0 * * ? * *")
        //    );
        //});
        //builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        builder.Services.AddMemoryCache();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        //app.UseCors();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        //app.UseMiddleware<CustomJwtMiddleware>();

        app.MapControllers();


        app.Run();

    }
}