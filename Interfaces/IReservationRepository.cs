using MVCproject.Models;

namespace MVCproject.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAll();
        Task<Reservation> GetByIdAsync(int id);
        Task<Reservation> GetByIdAsyncNoTracking(int id);
        bool Add(Reservation reservation);
        bool Delete(Reservation reservation);
        bool Update(Reservation reservation);
        bool Save();
    }
}
