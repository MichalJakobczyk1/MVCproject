using Microsoft.AspNetCore.Mvc;
using MVCproject.Interfaces;
using MVCproject.Models;
using MVCproject.Repositories;
using MVCproject.ViewModels;

namespace MVCproject.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Reservation> reservations = await _reservationRepository.GetAll();
            return View(reservations);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return View(reservation);
            }
            _reservationRepository.Add(reservation);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reservationDetail = await _reservationRepository.GetByIdAsync(id);
            if (reservationDetail == null) return View("Error");
            return View(reservationDetail);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservationDetails = await _reservationRepository.GetByIdAsync(id);
            if (reservationDetails == null) return View("Error");

            _reservationRepository.Delete(reservationDetails);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null) return View("Error");
            var reservationVM = new EditReservationViewModel
            {
                Id = id,
                HowManyPeople = reservation.HowManyPeople,
                DateOfReservation = reservation.DateOfReservation,
            };
            return View(reservationVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditReservationViewModel editReservationViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit employee");
                return View("Edit", editReservationViewModel);
            }

            var reservation = await _reservationRepository.GetByIdAsyncNoTracking(id);

            if (reservation != null)
            {
                var emp = new Reservation
                {
                    Id = id,
                    HowManyPeople = reservation.HowManyPeople,
                    DateOfReservation = reservation.DateOfReservation,
                };

                _reservationRepository.Update(reservation);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editReservationViewModel);
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            Reservation reservation = await _reservationRepository.GetByIdAsync(id);
            return View(reservation);
        }
    }
}
