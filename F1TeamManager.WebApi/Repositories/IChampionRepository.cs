using F1.Common.DataContext.DataModel;

namespace F1TeamManager.WebApi.Repositories
{
    public interface IChampionRepository
    {
        Task<Champion?> CreateAsync(Champion champion);
        Task<IEnumerable<Champion>> RetrieveAllAsync();
        Task<Champion?> RetrieveAsync(string id);
        Task<Champion?> UpdateAsync(string id, Champion champion);
        Task<bool?> DeleteAsync(string id);
    }
}
