using Framework.Configuration;
using Framework.Core.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Framework.JsonSerializer.NewtonSoft;

public class NewtonSoftSerializerModule : IFrameworkModule
{
    private readonly JsonSerializerSettings _defaultSettings = new();

    public NewtonSoftSerializerModule(Action<JsonSerializerSettings> settings)
    {
        settings.Invoke(_defaultSettings);
    }
    public NewtonSoftSerializerModule()
    {
        _defaultSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,

            ContractResolver = new PrivateSetterContractResolver()
        };
    }

    public void Register(IDependencyRegister dependencyRegister)
    {

        dependencyRegister.RegisterSingleton<IJsonSerializerAdapter>(() => new NewtonSoftSerializerAdapter(_defaultSettings));
    }
}
