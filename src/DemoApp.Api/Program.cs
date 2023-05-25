using DemoApp.Application.Configurations;
using DemoApp.Infra.Configurations;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddControllers().AddOData(opt => opt.Count().Filter().OrderBy().Select().SetMaxTop(50));

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

services.AddApplicationConfig();
services.AddInfraConfiguration(configuration);

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
