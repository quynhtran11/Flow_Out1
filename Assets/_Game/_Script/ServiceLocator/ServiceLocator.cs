using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static Dictionary<Type, BaseService> services = new();

    public static void Installer() 
    {
        Register(new SceneService());
    }

    public static void Proc()
    {
        foreach (var service in services)
        {
            service.Value?.Proc();
        }
    }

    private static void Register<T>(T service) where T : BaseService
    {
        services[typeof(T)] = service;
    }

    public static T Get<T>() where T : BaseService
    {
        return (T)services[typeof(T)];
    }
}