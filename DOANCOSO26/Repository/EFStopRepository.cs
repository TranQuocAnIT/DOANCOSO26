using Microsoft.EntityFrameworkCore;
using       DOANCOSO26.Data;
using DOANCOSO26.Models;

namespace DOANCOSO26.Repository
{
    public class EFStopRepository : IStopRepository
    {
        private readonly ApplicationDbContext _context;

        public EFStopRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stop>> GetAllAsync()
        {
            return await _context.Stops.Include(p => p.BusTrip).ToListAsync();
        }

        public async Task<Stop> GetByIdAsync(int id)
        {
            return await _context.Stops.Include(p => p.BusTrip).FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task AddAsync(Stop stop)
        {
            _context.Stops.Add(stop);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Stop stop)
        {
            _context.Stops.Update(stop);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var stop = await _context.Seats.FindAsync(id);
            _context.Seats.Remove(stop);
            await _context.SaveChangesAsync();
        }
    }
}
