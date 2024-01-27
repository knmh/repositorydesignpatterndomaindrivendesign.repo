using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services.Contracts.IApplicationService;

namespace RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services.Contracts
{
    public interface IPersonApplicationService<TSelectByIdAsync, USelectByIdAsync, TSelectAllAsync, TInsertAsync, TDeleteAsync, TUpdateAsync, TCreateAbstractId, TGetRealId>
          : IApplicationService<TSelectByIdAsync, USelectByIdAsync, TSelectAllAsync, TInsertAsync, TDeleteAsync, TUpdateAsync, TCreateAbstractId, TGetRealId>


          where TSelectByIdAsync : class
          where USelectByIdAsync : class
          where TSelectAllAsync : class
          where TInsertAsync : class
          where TDeleteAsync : class
          where TUpdateAsync : class
          where TCreateAbstractId : class
          where TGetRealId : class

    {
    }
}
