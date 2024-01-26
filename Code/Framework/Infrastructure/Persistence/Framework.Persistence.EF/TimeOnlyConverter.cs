using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Framework.Persistence.EF;

public class TimeOnlyConverter() : ValueConverter<TimeOnly, TimeSpan>(timeOnly => timeOnly.ToTimeSpan(),
    timeSpan => TimeOnly.FromTimeSpan(timeSpan));