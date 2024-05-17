using DOANCOSO26.Models;

namespace DOANCOSO26.Repository
{
    public interface ISeatRepository
    {
        Task<IEnumerable<Seat>> GetAllAsync();
        Task<Seat> GetByIdAsync(int id);
        Task AddAsync(Seat seat);
        Task UpdateAsync(Seat seat);
        Task DeleteAsync(int id);
    }
}
