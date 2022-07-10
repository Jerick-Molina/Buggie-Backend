using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buggie.Interface;
using Buggie.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Buggie.Logic;

using Buggie.DataProperties;

namespace Buggie_Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        string MyCORS = "_myCORS";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   
          
            services.AddControllers();
            services.AddSingleton<IMySqlDataAccess,MySqlDataAccess>();
            services.AddSingleton<IAccountAuthentication,AccountAuthentication>();
            services.AddSingleton<IAccountAccess,AccountAccess>();
            services.AddSingleton<IUserAccess,UserAccess>();
            
            

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddJwtBearer(options => {
             options.TokenValidationParameters = new TokenValidationParameters{
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidIssuer = "https://localhost:5501",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("j79n4hfrug5c9jk1u"))
             };
           });

           services.AddCors(options => {
                options.AddPolicy(name: MyCORS,policy =>{
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader();
                });
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();
            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
