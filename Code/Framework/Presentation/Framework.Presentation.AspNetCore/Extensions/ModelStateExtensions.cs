using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Framework.Presentation.AspNetCore.Extensions;

public static class ModelStateExtensions
{
    public static IDictionary<string, string[]> ToDictionaryOfStringArrays(this ModelStateDictionary modelState)
    {
        return modelState.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToArray()
        );
    }
}