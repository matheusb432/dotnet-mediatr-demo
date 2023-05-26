using DemoApp.Api.Services;
using DemoApp.Application.Common.Interfaces;
using DemoApp.Application.Configurations;
using DemoApp.Infra.Configurations;
using Microsoft.AspNetCore.OData;
using ZymLabs.NSwag.FluentValidation;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services
    .AddControllers()
    .AddOData(opt => opt.Count().Filter().OrderBy().Expand().Select().SetMaxTop(50));

services.AddApplicationConfig();
services.AddInfraConfiguration(configuration);

services.AddHttpContextAccessor();
services.AddScoped<ICurrentUserService, CurrentUserService>();
services.AddScoped<FluentValidationSchemaProcessor>(provider =>
{
    var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
    var loggerFactory = provider.GetService<ILoggerFactory>();

    return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

app.UseResponseCaching();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
