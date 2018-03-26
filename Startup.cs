using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using waveRiderTester.Data;

namespace waveRiderTester
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                    builder.WithOrigins("http://localhost:5000", "https://www.waverider.garrettwarddev.com","https://waverider.garrettwarddev.com","http://localhost:5000","http://localhost:8080","http:127.0.0.1:5000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials()
                        ); 
            }
            );
            services.AddMvc();
            var connection = $"Filename=/var/www/garrettwarddev.com/waveServer/Data/waveDb.db";
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:5000", "https://www.waverider.garrettwarddev.com","https://waverider.garrettwarddev.com","http://localhost:5000","http://localhost:8080","http:127.0.0.1:5000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            app.UseMvc();
        }
    }
}
