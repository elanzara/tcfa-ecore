using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eCore.Context;
using eCore.Models;
using eCore.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using eCore.Services.WebApi.Services;
using eCore.Helpers;
using eCore.Middlewares;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace eCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();


            //Mapeos de Entidades
            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<AccGrupos, GrupoDTO>();
                configuration.CreateMap<AccGruposXPerfil, GrupoXPerfilDTO>();
                configuration.CreateMap<MAccGruposXPerfil, GrupoXPerfilDTO>();
                configuration.CreateMap<AccPerfiles, PerfilDTO>();
                configuration.CreateMap<AccModulos, ModulosDTO>();
                configuration.CreateMap<AccModulos, ModuloResDTO>();
                configuration.CreateMap<AccProgramas, ProgramasDTO>();
                configuration.CreateMap<AccAcciones, AccionesDTO>();
                configuration.CreateMap<AccUsuarios, User>()
                 .ForMember(x => x.Username,
                            m => m.MapFrom(a => a.AdCuenta)).ReverseMap();
                configuration.CreateMap<AccGrupos, MAccGrupos>()
                 .ForMember(x => x.IdOrigen,
                            m => m.MapFrom(a => a.Id)).ReverseMap();
                configuration.CreateMap<MAccGrupos, LAccGrupos>()
                                 .ForMember(x => x.Accionsql,
                                            m => m.MapFrom(a => a.Modifica)).ReverseMap();
                configuration.CreateMap<MAccGrupos, NAccGrupos>()
                                 .ForMember(x => x.Accionsql,
                                            m => m.MapFrom(a => a.Modifica)).ReverseMap();
                configuration.CreateMap<AccPerfiles, MAccPerfiles>()
                 .ForMember(x => x.IdOrigen,
                            m => m.MapFrom(a => a.Id)).ReverseMap();
                configuration.CreateMap<MAccPerfiles, LAccPerfiles>()
                                 .ForMember(x => x.Accionsql,
                                            m => m.MapFrom(a => a.Modifica)).ReverseMap();
                configuration.CreateMap<MAccPerfiles, NAccPerfiles>()
                                 .ForMember(x => x.Accionsql,
                                            m => m.MapFrom(a => a.Modifica)).ReverseMap();
                configuration.CreateMap<AccModulos, MAccModulos>()
                 .ForMember(x => x.IdOrigen,
                            m => m.MapFrom(a => a.Id)).ReverseMap();
                configuration.CreateMap<MAccModulos, LAccModulos>()
                                 .ForMember(x => x.Accionsql,
                                            m => m.MapFrom(a => a.Modifica)).ReverseMap();
                configuration.CreateMap<MAccModulos, NAccModulos>()
                                 .ForMember(x => x.Accionsql,
                                            m => m.MapFrom(a => a.Modifica)).ReverseMap();
                configuration.CreateMap<AccProgramas, MAccProgramas>()
                 .ForMember(x => x.IdOrigen,
                            m => m.MapFrom(a => a.Id)).ReverseMap();
                configuration.CreateMap<MAccProgramas, LAccProgramas>()
                                 .ForMember(x => x.Accionsql,
                                            m => m.MapFrom(a => a.Modifica)).ReverseMap();
                configuration.CreateMap<MAccProgramas, NAccProgramas>()
                                 .ForMember(x => x.Accionsql,
                                            m => m.MapFrom(a => a.Modifica)).ReverseMap();
                configuration.CreateMap<AccUsuarios, MAccUsuarios>()
                 .ForMember(x => x.IdOrigen,
                            m => m.MapFrom(a => a.Id)).ReverseMap();
                configuration.CreateMap<MAccUsuarios, LAccUsuarios>()
                                 .ForMember(x => x.Accionsql,
                                            m => m.MapFrom(a => a.Modifica)).ReverseMap();
                configuration.CreateMap<MAccUsuarios, NAccUsuarios>()
                                 .ForMember(x => x.Accionsql,
                                            m => m.MapFrom(a => a.Modifica)).ReverseMap();
                configuration.CreateMap<AccGruposXPerfil, MAccGruposXPerfil>()
                 .ForMember(x => x.IdOrigen,
                            m => m.MapFrom(a => a.Id)).ReverseMap();
                configuration.CreateMap<MAccGruposXPerfil, LAccGruposXPerfil>()
                                 .ForMember(x => x.Accionsql,
                                            m => m.MapFrom(a => a.Modifica)).ReverseMap();
                configuration.CreateMap<MAccGruposXPerfil, NAccGruposXPerfil>()
                                 .ForMember(x => x.Accionsql,
                                            m => m.MapFrom(a => a.Modifica)).ReverseMap();


            }, typeof(Startup));

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            .EnableSensitiveDataLogging(true));

            services.AddDbContext<SecureDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SecureConnection"))
            .EnableSensitiveDataLogging(true));

            services.AddDbContext<SecureOMDb>(options => options.UseSqlServer(Configuration.GetConnectionString("SecureOMConnection"))
            .EnableSensitiveDataLogging(true));

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), builder => builder.UseRowNumberForPaging()));

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options
                                                                .AllowAnyOrigin()
                                                                .AllowAnyMethod()
                                                                .AllowAnyHeader());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddJsonOptions(options => options.SerializerSettings.DateFormatString = "yyyy-MM-dd hh:mm:ss");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseCors("AllowOrigin");
            app.UseMvc();
        }
    }
}
