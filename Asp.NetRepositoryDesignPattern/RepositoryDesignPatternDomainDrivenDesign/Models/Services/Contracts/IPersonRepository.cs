using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts.RepositoryFrameworks;

namespace RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts
{
    public interface IPersonRepository<T, U> : IRepository<T, U> where T : class
    {
    }
}
