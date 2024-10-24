﻿using System.Reflection;
using Framework.Core.Domain.Events;

namespace Framework.Persistence.EventStore;

public class EventTypeResolver : IEventTypeResolver
{
    private readonly Dictionary<string, Type> _types = new Dictionary<string, Type>();
    public void AddTypesFromAssembly(Assembly assembly)
    {
        var events = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(DomainEvent))).ToList();
        events.ForEach(a =>
        {
            _types.Add(a.Name, a);
        });
    }

    public Type? GetType(string typeName)
    {

        if (_types.ContainsKey(typeName))
            return _types[typeName];

        return null;
    }
}