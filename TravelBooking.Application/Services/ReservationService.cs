
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelBooking.Application.DTOs.Reservation;
using TravelBooking.Application.Interfaces;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IGenericRepository<Reservation> _reservationRepository;

        public ReservationService(IGenericRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<ReservationResponseDto>> GetAllAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return reservations.Select(r => new ReservationResponseDto
            {
                Id = r.Id,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                Status = r.Status,
                UserId = r.UserId,
                RoomId = r.RoomId
            });
        }

        public async Task<ReservationResponseDto> GetByIdAsync(int id)
        {
            var r = await _reservationRepository.GetByIdAsync(id);
            if (r == null) return null;

            return new ReservationResponseDto
            {
                Id = r.Id,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                Status = r.Status,
                UserId = r.UserId,
                RoomId = r.RoomId
            };
        }

        public async Task<ReservationResponseDto> CreateAsync(ReservationCreateDto dto)
        {
            var reservation = new Reservation
            {
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                UserId = dto.UserId,
                RoomId = dto.RoomId,
                Status = "Pending" // Default status
            };
            await _reservationRepository.AddAsync(reservation);
            await _reservationRepository.SaveChangesAsync();

            return new ReservationResponseDto
            {
                Id = reservation.Id,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                Status = reservation.Status,
                UserId = reservation.UserId,
                RoomId = reservation.RoomId
            };
        }

        public async Task<ReservationResponseDto> UpdateAsync(int id, ReservationUpdateDto dto)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null) return null;

            reservation.CheckInDate = dto.CheckInDate;
            reservation.CheckOutDate = dto.CheckOutDate;
            reservation.Status = dto.Status;

            _reservationRepository.Update(reservation);
            await _reservationRepository.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation != null)
            {
                _reservationRepository.Delete(reservation);
                await _reservationRepository.SaveChangesAsync();
            }
        }
    }
}
