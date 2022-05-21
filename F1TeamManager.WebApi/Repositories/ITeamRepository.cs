using F1.Common.DataContext.DataModel;

namespace F1TeamManager.WebApi.Repositories
{
    public interface ITeamRepository
    {
        Task<Team?> CreateAsync(Team team);
        Task<IEnumerable<Team>> RetrieveAllAsync();
        Task<Team?> RetrieveAsync(int id);
        Task<Team?> UpdateAsync(int id, Team team);
        Task<bool?> DeleteAsync(int id);
    }
}
