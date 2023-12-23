using Players.ServiceHost.Extensions;

var builder = WebApplication.CreateBuilder(args);


var app = builder
       .ConfigureServices()
       .ConfigurePipeline();

app.Run();


public partial class Program { }