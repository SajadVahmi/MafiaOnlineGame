using Framework.Configuration.Loaders;
using Framework.Configuration.Scrutor;
using Framework.JsonSerializer.NewtonSoft;
using Framework.Mapping.AutoMapper;
using Framework.Presentation.AspNetCore.Extensions;
using Players.Config;
using Players.Mapping.PlayerAggregate;
using Players.RestApi.V1.PlayerAggregate.Validations.Register;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextServices();

builder.Services.AddValidatorsFromAssemblyContaining<PlayerRegistrationRequestValidator>();

FrameworkModuleBuilder.Setup()
    .WithIocModule(new ScrutorModule(builder.Services))
    .WithModule(new PlayersModule(builder.Configuration, builder.Services))
    .WithModule(new AutoMapperModule(typeof(PlayerMappings).Assembly))
    .WithModule(new NewtonSoftSerializerModule());

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }