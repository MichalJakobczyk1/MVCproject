using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationViewModel createReservationViewModel)
        {
            if (ModelState.IsValid)
            {
                var reservation = new Reservation
                {
                    HowManyPeople = createReservationViewModel.HowManyPeople,
                    DateOfReservation = createReservationViewModel.DateOfReservation,
                };
                _reservationRepository.Add(reservation);
                return RedirectToAction("Index");
            }
            return View(createReservationViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var reservationDetail = await _reservationRepository.GetByIdAsync(id);
            if (reservationDetail == null) return View("Error");
            return View(reservationDetail);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservationDetails = await _reservationRepository.GetByIdAsync(id);
            if (reservationDetails == null) return View("Error");

            _reservationRepository.Delete(reservationDetails);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
                var res = new Reservation
                {
                    Id = id,
                    HowManyPeople = editReservationViewModel.HowManyPeople,
                    DateOfReservation = editReservationViewModel .DateOfReservation,
                };

                _reservationRepository.Update(res);
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
