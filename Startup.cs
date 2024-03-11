using MemRegPortal.DataAccessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemRegPortal
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
            services.AddControllers()
                .AddJsonOptions(options=>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            //implementing services of dependency injection
            services.AddScoped<IMemRegDL, MemRegDL>();
            services.AddScoped<IAuthenticationDL, AuthenticationDL>();

            //implementing CORS policy
            /* services.AddCors(options => options.AddDefaultPolicy(
                 builder => builder.AllowAnyOrigin()));
            */
            services.AddCors(options => options.AddPolicy(name: "FrontendUI",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                }
                ));

            //implementing jwtbearer services
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(Configuration["jwtConfig:Key"]);
                //o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,                 //issuer is that who gives token in this case its localhost
                    ValidateAudience = true,                //localhost
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost",
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                    ClockSkew=TimeSpan.Zero                     //to change default duration of 5 mins as we are giving manual duration
                };
            }
            );

            services.AddSwaggerGen();
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

            app.UseAuthorization();

            //SWAGGER IMPLEMENTATION

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
               
            });

            //CORS
            app.UseCors("FrontendUI");                     //cors should always be added in between app.useRouting and app.UserAuthentication

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
