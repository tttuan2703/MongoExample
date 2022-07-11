namespace MongoExample.Infrastructure.Helpers.Contracts
{
    public interface IPatternContainerProvider<ScopeType> : IDependencyInjectionHelper
        where ScopeType : IDisposable
    {
        ScopeType InitScope();
    }
}
