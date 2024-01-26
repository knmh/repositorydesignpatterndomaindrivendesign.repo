using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services.Contracts.IApplicationService;

namespace RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services.Contracts
{
    public interface IProductApplicationService<TSelectByIdAsync, USelectByIdAsync, TSelectAllAsync, TInsertAsync, TDeleteAsync, TUpdateAsync>
          : IApplicationService<TSelectByIdAsync, USelectByIdAsync, TSelectAllAsync, TInsertAsync, TDeleteAsync, TUpdateAsync>


          where TSelectByIdAsync : class
          where USelectByIdAsync : class
          where TSelectAllAsync : class
          where TInsertAsync : class
          where TDeleteAsync : class
          where TUpdateAsync : class

    {
    }
}
