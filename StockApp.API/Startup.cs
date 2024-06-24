using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StockApp.Application.Interfaces;
using StockApp.Application.Mappings;
using StockApp.Application.Services;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using StockApp.Infra.Data.Repositories;
using System.Configuration;
using System.Security.Claims;

namespace StockApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddAuthorization(options =>
            {
                options.AddPolicy("adminPolicy", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "admin"));
            });

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainToDTOMappingProfile());
                mc.AddProfile(new DTOToCommandMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                // Configuração do CORS
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowAnyOrigin",
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader();
                        }
                    );
                });

            }
            //Configura serviços de middleware
             public void ConfigureServices(IServiceCollection services)
            {
                services.AddControllers();

                // Configurar Rate Limiting
                services.AddMemoryCache();
                services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
                services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
                services.AddInMemoryRateLimiting();
            }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthorization();

                // Configurar Rate Limiting Middleware
                app.UseIpRateLimiting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }

        }
    }
}
