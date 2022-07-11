using Autofac;
using MongoExample.Infrastructure.Helpers.Contracts;

namespace MongoExample.Infrastructure.Helpers.Services
{
    public class IoCHelper : IPatternContainerProvider<ILifetimeScope>
    {
        private readonly IContainer _container;

        private ILifetimeScope _scope;

        public ILifetimeScope Scope
        {
            get
            {
                if(_scope == null)
                {
                    _scope = InitScope();
                }

                return _scope;
            }
        }

        /// <summary>
        /// init
        /// </summary>
        /// <param name="container"></param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IoCHelper(IContainer container)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _container = container;
        }

        public ILifetimeScope InitScope()
        {
            return _container.BeginLifetimeScope();
        }

        public T ResolveService<T>()
        {
            var type = typeof(T);
            if (type.IsGenericType)
            {
                Console.WriteLine($"No Service provice for {type}");
            }

            return Scope.Resolve<T>();
        }
    }
}
