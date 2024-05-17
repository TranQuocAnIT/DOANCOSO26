using Microsoft.EntityFrameworkCore;
using DOANCOSO26.Data;
using DOANCOSO26.Models;
using static    DOANCOSO26.Repository.EFSeatRepository;

namespace DOANCOSO26.Repository
{
    public class EFSeatRepository : ISeatRepository
    {

        private readonly ApplicationDbContext _context;

        public EFSeatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seat>> GetAllAsync()
        {
            return await _context.Seats.Include(p => p.BusTrip).ToListAsync();
        }

        public async Task<Seat> GetByIdAsync(int id)
        {
            return await _context.Seats.Include(p => p.BusTrip).FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task AddAsync(Seat seat)
        {
            _context.Seats.Add(seat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seat seat)
        {
            _context.Seats.Update(seat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var seat = await _context.Seats.FindAsync(id);
            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();
        }
    }
}
