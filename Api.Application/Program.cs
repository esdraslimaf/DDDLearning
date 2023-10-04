using Api.CrossCutting.DependencyInjection;
using Api.Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureService.ConfiguracaoDependenciaService(builder.Services);
ConfigureRepository.ConfiguracaoDependenciaRepositorio(builder.Services);

 var signingConfigurations = new SigningConfigurations();
            builder.Services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations(); 
            new ConfigureFromConfigurationOptions<TokenConfigurations>( 
                builder.Configuration.GetSection("TokenConfigurations")) //Aqui o objeto TokenConfiguration será preenchido com o appsettings(TokenConfigurations).
                     .Configure(tokenConfigurations);
            builder.Services.AddSingleton(tokenConfigurations);

builder.Services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            builder.Services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });


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
