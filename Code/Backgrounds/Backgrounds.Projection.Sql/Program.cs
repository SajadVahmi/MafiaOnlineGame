using Backgrounds.Projection.Sql;
using Backgrounds.Projection.Sql._Shared;
using Backgrounds.Projection.Sql.Handlers;
using EventStore.Client;
using Framework.Core.Domain.Events;
using Framework.Persistence.EventStore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<IEventTypeResolver, EventTypeResolver>();
builder.Services.AddSingleton<IEventBus,EventBus>();
builder.Services.Scan(s => s.FromAssemblies(typeof(PlayerRegisteredHandler).Assembly)
    .AddClasses(c => c.AssignableToAny(typeof(IEventHandler<>)))
    .AsImplementedInterfaces()
    .WithSingletonLifetime());

builder.Services.ConfigureSqlServerCursor(builder.Configuration);

var settings = EventStoreClientSettings.Create("esdb://localhost:2113?tls=false&tlsVerifyCert=false");
builder.Services.AddSingleton(_=> new EventStoreClient(settings));

var host = builder.Build();
host.Run();
