using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoExample.Infrastructure.Helpers.Contracts
{
    public interface IDependencyInjectionHelper
    {
        public T ResolveService<T>();
    }
}
