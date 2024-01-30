namespace RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts.RepositoryFrameworks
{
    public interface IRepository<T, U> where T : class
    {
        Task<T> SelectByIdAsync(U? id);

        Task<IEnumerable<T>> SelectAllAsync();

        Task InsertAsync(T entity);

        //Task DeleteAsync(U? id);

        Task<bool> DeleteAsync(T entity);

        // Task UpdateAsync(U id, T t);

        // Task UpdateAsync(U? id);

        Task UpdateAsync(T entity);
    }
   
}
