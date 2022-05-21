using F1.Common.DataContext;
using F1.Common.DataContext.DataModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Concurrent;

namespace F1TeamManager.WebApi.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private static ConcurrentDictionary <int, Team>? teamsCache;
        private F1DbContext db;

        public TeamRepository(F1DbContext context)
        {
            db = context;
            if (teamsCache is null)
            {
                teamsCache = new ConcurrentDictionary<int, Team>(
                db.Teams.ToDictionary(_ => _.Id));
            }
        }
        public async Task<Team?> CreateAsync(Team team)
        {
            EntityEntry<Team> added = await db.Teams.AddAsync(team);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                if (teamsCache is null) return team;
                return teamsCache.AddOrUpdate(team.Id, team, UpdateCache); 
            }
            else
            {
                return null;
            }
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Team? team = db.Teams.Find(id);
            if (team is null) return null;
            db.Teams.Remove(team);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                if (teamsCache is null) return null;
                return teamsCache.TryRemove(id, out team);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Team>> RetrieveAllAsync()
        {
            return Task.FromResult(teamsCache is null ? Enumerable.Empty<Team>() : teamsCache.Values);
        }

        public Task<Team?> RetrieveAsync(int id)
        {
            if (teamsCache is null) return null!;
            teamsCache.TryGetValue(id, out Team? c); 
            return Task.FromResult(c);
        }

        public async Task<Team?> UpdateAsync(int id, Team team)
        {
            db.Teams.Update(team);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                // update in cache
                return UpdateCache(id, team);
            }
            return null;
        }

        private Team UpdateCache(int id, Team team)
        {
            Team? old;
            if (teamsCache is not null)
            {
                if (teamsCache.TryGetValue(id, out old))
                {
                    if (teamsCache.TryUpdate(id, team, old))
                    {
                        return team;
                    }
                }
            }
            return null!;
        }
    }
}
