using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Zadatak.Attributes;

namespace Zadatak.Services
{
    public static class ServiceForDi
    {
        public static void AddDependency(this IServiceCollection serviceCollection)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes().Where(x => x.GetCustomAttributes<DefineScopeType>().Any());

            foreach (var type in types)
            {
                var scopeType = type.GetCustomAttribute<DefineScopeType>().Type;

                Type[] interfaces = type.GetInterfaces();
                
                foreach (var iface in interfaces)
                {
                    if (iface.IsGenericTypeDefinition && type.IsGenericTypeDefinition
                        && iface.IsGenericType && type.IsGenericType)
                    {
                        switch (scopeType)
                        {
                            case EnumScopeType.Scoped:
                                serviceCollection.AddScoped(iface, type);
                                break;

                            case EnumScopeType.Transient:
                                serviceCollection.AddTransient(iface, type);
                                break;

                            case EnumScopeType.Singleton:
                                serviceCollection.AddSingleton(iface, type);
                                break;
                        }
                    }

                    else if (!iface.IsGenericTypeDefinition && !type.IsGenericTypeDefinition)
                        switch (scopeType)
                        {
                            case EnumScopeType.Scoped:
                                serviceCollection.AddScoped(iface, type);
                                break;

                            case EnumScopeType.Transient:
                                serviceCollection.AddTransient(iface, type);
                                break;

                            case EnumScopeType.Singleton:
                                serviceCollection.AddSingleton(iface, type);
                                break;
                        }
                    }
                }
            }


        }
    }

