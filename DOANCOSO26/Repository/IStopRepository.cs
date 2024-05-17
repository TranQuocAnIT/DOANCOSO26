using DOANCOSO26.Models;

namespace DOANCOSO26.Repository
{
    public interface IStopRepository
    {
        Task<IEnumerable<Stop>> GetAllAsync();
        Task<Stop> GetByIdAsync(int id);
        Task AddAsync(Stop stop);
        Task UpdateAsync(Stop stop);
        Task DeleteAsync(int id);
    }
}
