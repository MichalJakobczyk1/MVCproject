using Microsoft.EntityFrameworkCore;
using MVCproject.Data;
using MVCproject.Interfaces;
using MVCproject.Models;

namespace MVCproject.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;
        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(Reservation reservation)
        {
            _context.Add(reservation);
            return Save();
        }

        public bool Delete(Reservation reservation)
        {
            _context.Remove(reservation);
            return Save();
        }

        public async Task<IEnumerable<Reservation>> GetAll()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Reservation> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Reservations.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Reservation reservation)
        {
            _context.Update(reservation);
            return Save();
        }
    }
}
