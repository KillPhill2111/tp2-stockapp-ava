using StockApp.Application.Interfaces;
using StockApp.Application.Services;
using StockApp.Domain.Interfaces;
using StockApp.Infra.IoC;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddInfrastructureAPI(builder.Configuration);



        //adicionar servi�os de container de inje��o de dependencias
        builder.Services.AddScoped<IAvaliacaoRepository, IAvaliacaoRepository>();
        builder.Services.AddScoped<IAvaliacaoService, AvaliacaoService>();


        // Configura��o de servi�os

        builder.Services.AddControllers();
        builder.Services.AddSingleton<ICustomReportService, CustomReportService>();

        builder.Services.AddHttpClient<IPaymentIntegrationService, PaymentIntegrationService>(client =>
        {
            client.BaseAddress = new Uri("https://api.payment.com/");
        });


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

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}