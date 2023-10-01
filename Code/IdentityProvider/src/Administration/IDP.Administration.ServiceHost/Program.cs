using IDP.Administration.ServiceHost;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureConfiguration()
    .ConfigureServices()
    .ConfigurePipeline();

app.Run();
public partial class Program { }
