using Microsoft.EntityFrameworkCore;
using   DOANCOSO26.Data;
using   DOANCOSO26.Models;

namespace DOANCOSO26.Repository
{
    public class EFBookingRepository :IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public EFBookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Tương tự như EFProductRepository, nhưng cho Booking
        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings.Include(p => p.Seats).ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _context.Bookings.Include(p => p.Seats).FirstOrDefaultAsync(p => p.Id == id);

        }
        public async Task AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}
