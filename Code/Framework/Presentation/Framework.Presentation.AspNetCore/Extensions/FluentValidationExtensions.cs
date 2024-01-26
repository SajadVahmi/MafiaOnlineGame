using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Framework.Presentation.AspNetCore.Extensions;

public static class FluentValidationExtensions
{
    public static IDictionary<string, string[]> ToDictionary(this ValidationResult validationResult)
    {
        return validationResult.Errors
          .GroupBy(x => x.PropertyName)
          .ToDictionary(
            g => g.Key,
            g => g.Select(x => x.ErrorMessage).ToArray()
          );
    }

    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
    {
        foreach (var error in result.Errors)
            modelState.AddModelError(error.PropertyName,error.ErrorMessage);
  
    }
}
