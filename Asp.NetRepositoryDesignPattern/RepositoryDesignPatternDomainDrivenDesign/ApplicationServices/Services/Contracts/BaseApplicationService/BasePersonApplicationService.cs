namespace RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services.Contracts.BaseApplicationService
{
    public class BasePersonApplicationService<TSelectByIdAsync, USelectByIdAsync, TSelectAllAsync, TInsertAsync, TDeleteAsync, TUpdateAsync, TCreateAbstractId, TGetRealId> :IPersonApplicationService <TSelectByIdAsync, USelectByIdAsync, TSelectAllAsync, TInsertAsync, TDeleteAsync, TUpdateAsync, TCreateAbstractId, TGetRealId>
        where TSelectByIdAsync : class
        where USelectByIdAsync : class
        where TSelectAllAsync : class
        where TInsertAsync : class
        where TDeleteAsync : class
        where TUpdateAsync : class
        where TCreateAbstractId : class
        where TGetRealId : class

    {
        public Task CreateAbstractId(TCreateAbstractId CreateAbstractIdDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TDeleteAsync entity)
        {
            throw new NotImplementedException();
        }

        public Task GetRealId(TGetRealId getRealIdDto)
        {
            throw new NotImplementedException();
        }

        public Task PopulateIdMappingsFromDatabase()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(TInsertAsync entity)
        {
            throw new NotImplementedException();
        }

        public Task<TSelectByIdAsync> SelectByIdAsync(USelectByIdAsync? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TSelectAllAsync>> ShowAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TUpdateAsync entity)
        {
            throw new NotImplementedException();
        }
    }
}

