namespace RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services.Contracts.IApplicationService
{
    public interface IApplicationService<TSelectByIdAsync, USelectByIdAsync, TSelectAllAsync, TInsertAsync, TDeleteAsync, TUpdateAsync>
    {

        // Task<IEnumerable<TGetPersonByIdAsync>> ShowPersonByIdAsync();
        Task<TSelectByIdAsync> SelectByIdAsync(USelectByIdAsync? id);

        Task<IEnumerable<TSelectAllAsync>> ShowAllAsync();

        Task SaveAsync(TInsertAsync entity);

        Task DeleteAsync(TDeleteAsync entity);

        Task UpdateAsync(TUpdateAsync entity);

       




    }
}
