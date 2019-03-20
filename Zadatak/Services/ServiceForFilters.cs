using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Remotion.Linq.Clauses;
using Zadatak.Attributes;

namespace Zadatak.Services
{
    public static class ServiceForFilters
    {

        public static void AddFilters(this IServiceCollection serviceCollection)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes().Where(x => x.GetCustomAttributes<IsFilter>().Any());
            
            serviceCollection.AddMvc(options =>
            {
                foreach (var type in types)
                {
                    options.Filters.Add(type);
                }
            });
        }
    }
}
