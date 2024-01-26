namespace RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services.Contracts.BaseApplicationService
{
    public class BasePersonApplicationService<TSelectByIdAsync, USelectByIdAsync, TSelectAllAsync, TInsertAsync, TDeleteAsync, TUpdateAsync> :IPersonApplicationService <TSelectByIdAsync, USelectByIdAsync, TSelectAllAsync, TInsertAsync, TDeleteAsync, TUpdateAsync>
        where TSelectByIdAsync : class
        where USelectByIdAsync : class
        where TSelectAllAsync : class
        where TInsertAsync : class
        where TDeleteAsync : class
        where TUpdateAsync : class

    {

        public Task DeleteAsync(TDeleteAsync entity)
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

