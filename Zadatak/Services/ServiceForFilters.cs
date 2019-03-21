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
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceForFilters
    {

        /// <summary>
        /// Adds the MVC and filters.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        public static void AddMvcAndFilters(this IServiceCollection serviceCollection)
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
