using MongoExample.Infrastructure.Helpers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoExample.Infrastructure.Helpers.Services
{
    public static class DependencyInjectionHelper
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static IDependencyInjectionHelper DependencyInjection { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static T ResolveService<T>()
        {
            if(DependencyInjection == null)
            {
                Console.WriteLine($"Don't find service {nameof(T)}");
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return DependencyInjection.ResolveService<T>();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public static void Init(IDependencyInjectionHelper dependencyInjectionHelper)
        {
            DependencyInjection = dependencyInjectionHelper;
        }
    }
}
