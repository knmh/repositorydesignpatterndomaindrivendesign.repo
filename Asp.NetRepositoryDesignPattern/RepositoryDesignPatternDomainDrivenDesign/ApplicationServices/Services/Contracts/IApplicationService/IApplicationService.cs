using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.PersonDtos;

namespace RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services.Contracts.IApplicationService
{
    public interface IApplicationService<TSelectByIdAsync, USelectByIdAsync, TSelectAllAsync, TInsertAsync, TDeleteAsync, TUpdateAsync, TCreateAbstractId, TGetRealId>
    {

        Task<TSelectByIdAsync> SelectByIdAsync(USelectByIdAsync? id);

        Task<IEnumerable<TSelectAllAsync>> ShowAllAsync();

        Task SaveAsync(TInsertAsync SaveDto);

        Task DeleteAsync(TDeleteAsync deleteDto);

        Task UpdateAsync(TUpdateAsync updateDto);

        Task PopulateIdMappingsFromDatabase();

        Task CreateAbstractId(TCreateAbstractId createAbstractIdDto);

        Task GetRealId(TGetRealId getRealIdDto);






    }
}
