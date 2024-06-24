using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StockApp.Application.Interfaces;
using StockApp.Application.Services;
using StockApp.Domain.Interfaces;
using StockApp.Infra.IoC;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddInfrastructureAPI(builder.Configuration);



        //adicionar serviços de container de injeção de dependencias
        builder.Services.AddScoped<IAvaliacaoRepository, IAvaliacaoRepository>();
        builder.Services.AddScoped<IAvaliacaoService, AvaliacaoService>();


        // Configuração de serviços

        builder.Services.AddControllers();
        builder.Services.AddSingleton<ICustomReportService, CustomReportService>();


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseIpRateLimiting();

        //configura a autenticação do jwt
        var key = Encoding.ASCII.GetBytes("UmaChaveSuperSecreta");
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "NossaIssue",
                ValidAudience="NossaAudiencia",
                IssuerSigningKey= new SymmetricSecurityKey(key)
            };
        });


        app.MapControllers();

        app.Run();
    }
}