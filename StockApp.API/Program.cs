using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StockApp.Application.Interfaces;


using StockApp.Application.Services;
using StockApp.Domain.Interfaces;

using StockApp.Infra.IoC;

using System.Text;
=======
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Repositories;
using FluentAssertions.Common;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureAPI(builder.Configuration);


// Configura��o de servi�os
builder.Services.AddControllers();
builder.Services.AddSingleton<IContractManagementService, ContractManagementService>();




//adicionar serviços de container de injeção de dependencias
builder.Services.AddScoped<IAvaliacaoRepository, IAvaliacaoRepository>();



// Configuração de serviços

builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddSingleton<IProjectFeasibilityAnalysisService, ProjectFeasibilityAnalysisService>();
builder.Services.AddSingleton<IProductionPlanningService, ProductionPlanningService>();
builder.Services.AddSingleton<IProcessAutomationService, ProcessAutomationService>();
builder.Services.AddSingleton<IQualityMonitoringService, QualityMonitoringService>();
builder.Services.AddControllers();
builder.Services.AddSingleton<ICustomerRelationshipManagementService, CustomerRelationshipManagementService>();
builder.Services.AddSingleton<IFinancialManagementService, FinancialManagementService>();
builder.Services.AddSingleton<ICompetitivenessAnalysisService, CompetitivenessAnalysisService>();
builder.Services.AddSingleton<ISupplierRelationshipManagementService, SupplierRelationshipManagementService>();
builder.Services.AddSingleton<ISentimentAnalysisService, SentimentAnalysisService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddSingleton<IJustInTimeInventoryService, JustInTimeInventoryService>();

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

builder.Services.AddControllers();
builder.Services.AddSingleton<ICustomerFeedbackManagementService, ICustomerFeedbackManagementService>();



app.UseHttpsRedirection();

app.UseAuthorization();


        app.UseIpRateLimiting();

        //configura a autentica��o do jwt
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
=======
app.MapControllers();


app.Run();
