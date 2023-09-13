using Api.CrossCutting.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureService.ConfiguracaoDependenciaService(builder.Services);
ConfigureRepository.ConfiguracaoDependenciaRepositorio(builder.Services);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Aprendizagem API com DDD",
        Description = "Aprofundando em DDD",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Esdras Lima - Linkedin",
            Email = "esdraslimaf@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/esdrasdev/")
        }
    });
});
// Adicionamos o Swagger, mais abaixo estamos realizando sua configuração

// O AddDbContext foi colocado também na ConfigureRepository simplesmenta para não deixarmos aqui na Application!

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API com DDD Net 6.0");
        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
