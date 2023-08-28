using Api.Configurations;
using Api.Mapping;
using AutoMapper;
using Core.Contracts.Data;
using Core.Domain.ApplicationModels;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using IoC.Resolver;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Transversal.Extensions;
using Transversal.Helpers;

internal class Program
{
    [Obsolete]
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ILoggerFactory _loggerFactory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug());


        #region ConfigureServices

        #region Configure Basic Services
        IdentityModelEventSource.ShowPII = true;
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpClient();
        #endregion

        #region Configure Personalized
        builder.Services.ConfigureSwagger();
        //builder.Services.AddSwaggerGen();
        builder.Services.ConfigureIoC(builder.Configuration);
        builder.Services.ConfigureJwt(builder.Configuration);
        builder.Services.ConfigureLogger(builder?.Configuration);
        builder.Services.AddHttpContextAccessor();
        //builder.Services.TryAddScoped<SignInManager<Users>>();
        #endregion

        #region Configure DbContext
        builder.Services.AddDbContext<ApplicationDbContext>
        (
            options => options
            .UseSqlServer(GetConnectionString(), builder =>
                 builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)) //Al contexto le agrego la conexion de la base de datos

            //En esta parte configuramos el entity framework para ver los querys en consola (IMPORTANTE: desactivarlo en produccion)
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(_loggerFactory)
        );
        #endregion

        #region Configure Identity
        //Porque usamos Identity Core? porque tenemos configurado ya por defecto nuestras settings de JWT
        //Entonces is usamos AddIdentity este ya configura la autorización y nos pisaría nuestros settings. (se puede ver en la definición de Identity)
        builder.Services.AddIdentityCore<Users>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.User.RequireUniqueEmail = true;
            //options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
        })
        .AddSignInManager<SignInManager<Users>>()
        .AddRoles<Privileges>()
        .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        #endregion

        #region Configure AppSettings Inyection
        builder.Services.AddConfig<FrontConfiguration>(builder.Configuration, nameof(FrontConfiguration));
        builder.Services.AddConfig<ActionLoggerMiddlewareConfiguration>(builder.Configuration, nameof(ActionLoggerMiddlewareConfiguration));
        #endregion

        #region Configure Mapper
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ApiMapping());
        });
        IMapper mapper = mappingConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);
        #endregion
        
        #region Configure ML (Machine Learning)
        var modelPath = builder.Configuration["ML_Config:ModelPath"] ?? "";
        //builder.Services.AddSingleton(new QuestionPredictionEngine(modelPath));
        //builder.Services.AddSingleton<PredictionEngine<QuestionModel, QuestionPrediction>>();
        #endregion
        #endregion


        #region ConfigureApp
        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            Configure(app,
                      app.Environment,
                      services.GetRequiredService<ApplicationDbContext>()
                      );
        }

        #region Configure Init Application
        //void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context, RoleManager<Roles> _roleManager)
        //{
        //    #region Configure Development Environment
        //    if (env.IsDevelopment())
        //    {
        //        app.UseSwagger();
        //        app.UseSwaggerUI();
        //        app.UseDeveloperExceptionPage();
        //        context.Database.Migrate(); //Cuando se ejecuta la aplicación se ejecuta el metodo update-database de dotnet ef core...
        //    }
        //    #endregion

        //    #region Configure Swagger
        //    app.UseSwagger();
        //    app.UseSwaggerUI(c =>
        //    {
        //        c.RoutePrefix = String.Empty;
        //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UAI TFI-TP-FINAL API V1");
        //        c.OAuthClientId(builder.Configuration["AuthenticationConfiguration:Google:ClientId"]);
        //        c.OAuthClientSecret(builder.Configuration["AuthenticationConfiguration:Google:ClientSecret"]);
        //        c.OAuth2RedirectUrl("https://localhost:44352/signin-google");
        //        c.OAuthAppName("API - Swagger");
        //        c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        //        c.OAuthUsePkce();
        //        c.OAuthScopeSeparator(" ");
        //        c.EnableValidator(null);
        //        c.OAuthAdditionalQueryStringParams(new Dictionary<string, string> { { "audience", "" } });
        //    });
        //    #endregion

        //    #region Configure CORS
        //    var corsAllowAll = builder.Configuration["CorsAllowedAllHosts"] ?? "false";
        //    app.UseCors(GetCorsConfig(corsAllowAll == "true"));
        //    #endregion

        //    #region Save Data For Application
        //    var htmlDocuments = builder.Configuration.GetSection("TemplatesEmailPath").Value ?? AppDomain.CurrentDomain.BaseDirectory + "HtmlDocuments\\";

        //    AppDomain.CurrentDomain.SetData("ContentRootPath", env.ContentRootPath);
        //    AppDomain.CurrentDomain.SetData("WebRootPath", env.WebRootPath);
        //    AppDomain.CurrentDomain.SetData("HtmlDocuments", htmlDocuments);
        //    #endregion

        //    #region Configure Default Methods .NET
        //    app.UseStaticFiles();
        //    app.UseRouting();
        //    app.UseAuthentication();
        //    app.UseAuthorization();
        //    //app.UseHttpsRedirection();
        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //    });
        //    #endregion

        //    #region Set Default Roles If Not Exists
        //    if (_roleManager.FindByNameAsync("Admin").Result == null && _roleManager.FindByNameAsync("User").Result == null)
        //    //{
        //    //    //Create Roles
        //    //    var role = new Roles();
        //    //    role.Name = "Admin";
        //    //    IdentityResult roleResult = _roleManager.CreateAsync(role).Result;

        //    //    role = new Roles();
        //    //    role.Name = "User";
        //    //    roleResult = _roleManager.CreateAsync(role).Result;
        //    //}
        //    {
        //        _roleManager.CreateAsync(role: new Roles { Name = "Admin", NormalizedName = "ADMIN" }).Wait();
        //        _roleManager.CreateAsync(role: new Roles { Name = "User", NormalizedName = "USER" }).Wait();
        //    }
        //    #endregion

        //    #region Configure SignalR
        //    //app.UseMiddleware<ActionLoggerMiddleware>();
        //    #endregion
        //}
        #endregion

        void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               // context.Database.Migrate(); //Cuando se ejecuta la aplicación se ejecuta el metodo update-database de dotnet ef core...
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = String.Empty;
                c.SwaggerEndpoint("/swagger/InnoviaReachAPISpecification/swagger.json", "UAI InnoviaReach API");
            });


            app.UseHttpsRedirection();

            var corsAllowAll = builder.Configuration["CorsAllowedAllHosts"] ?? "false";
            app.UseCors(GetCorsConfig(corsAllowAll == "true"));


            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region Run App
        //RUN APP
        app.Run();
        #endregion

        #endregion


        #region Helpers
        Action<CorsPolicyBuilder> GetCorsConfig(bool allowAnyOrigin)
        {
            void configAllowSpecific(CorsPolicyBuilder configurePolicy)
            {
                string origins = builder?.Configuration?.GetSection("AllowedOrigins")?.Value ?? "";

                configurePolicy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(origins.Split(","))
                .AllowCredentials();
            }

            void configAllowAll(CorsPolicyBuilder configurePolicy)
            {
                configurePolicy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            }

            if (allowAnyOrigin) return configAllowAll;
            else return configAllowSpecific;
        }
        string GetConnectionString()
        {
            var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
            return connectionString;
        }
        string GetMySQLConnectionString()
        {
            var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
            return connectionString;
        }
        #endregion
    }
}