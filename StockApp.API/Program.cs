using StockApp.Application.Interfaces;
using StockApp.Application.Services;
using StockApp.Infra.IoC;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Repositories;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddInfrastructureAPI(builder.Configuration);

        // Configuração de serviços
        builder.Services.AddControllers();
        builder.Services.AddSingleton<IContractManagementService, ContractManagementService>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IRecommendationService, RecommendationService>();
        builder.Services.AddInfrastructureAPI(builder.Configuration);
        builder.Services.AddSingleton<IProjectFeasibilityAnalysisService, ProjectFeasibilityAnalysisService>();

        builder.Services.AddControllers();
        builder.Services.AddSingleton<ICustomerRelationshipManagementService, CustomerRelationshipManagementService>();

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