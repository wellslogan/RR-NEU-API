using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RR_NEU_API.Contexts;
using RR_NEU_API.Models;
using RR_NEU_API.Repository;

namespace RR_NEU_API
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
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddCors();

            var connectionString = Environment.GetEnvironmentVariable("DATABASE_CS");
            services.AddEntityFrameworkNpgsql().AddDbContext<RRContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IRRRepository, RRRepository>();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");
                googleOptions.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET");
            });

            services.AddAuthentication(options => 
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o => 
                {
                    o.SecurityTokenValidators.Clear();
                    o.SecurityTokenValidators.Add(new GoogleTokenValidator());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                Console.WriteLine("Warning! Running in Development environment!");
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseCors(builder => builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader());

            app.UseMvc();
        }
    }
}
