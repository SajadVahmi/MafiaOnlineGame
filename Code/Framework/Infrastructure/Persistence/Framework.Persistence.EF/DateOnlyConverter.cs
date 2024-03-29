﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Framework.Persistence.EF;

public class DateOnlyConverter() : ValueConverter<DateOnly, DateTime>(
    dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
    dateTime => DateOnly.FromDateTime(dateTime));
