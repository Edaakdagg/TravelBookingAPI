
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelBooking.Application.DTOs.Room;
using TravelBooking.Application.Interfaces;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IGenericRepository<Room> _roomRepository;

        public RoomService(IGenericRepository<Room> roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<RoomResponseDto>> GetAllAsync()
        {
            var rooms = await _roomRepository.GetAllAsync();
            return rooms.Select(r => new RoomResponseDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                Price = r.Price,
                Type = r.Type,
                IsAvailable = r.IsAvailable,
                HotelId = r.HotelId
            });
        }

        public async Task<RoomResponseDto> GetByIdAsync(int id)
        {
            var r = await _roomRepository.GetByIdAsync(id);
            if (r == null) return null;

            return new RoomResponseDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                Price = r.Price,
                Type = r.Type,
                IsAvailable = r.IsAvailable,
                HotelId = r.HotelId
            };
        }

        public async Task<RoomResponseDto> CreateAsync(RoomCreateDto dto)
        {
            var room = new Room
            {
                RoomNumber = dto.RoomNumber,
                Price = dto.Price,
                Type = dto.Type,
                IsAvailable = dto.IsAvailable,
                HotelId = dto.HotelId
            };
            await _roomRepository.AddAsync(room);
            await _roomRepository.SaveChangesAsync();

            return new RoomResponseDto
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Price = room.Price,
                Type = room.Type,
                IsAvailable = room.IsAvailable,
                HotelId = room.HotelId
            };
        }

        public async Task<RoomResponseDto> UpdateAsync(int id, RoomUpdateDto dto)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return null;

            room.RoomNumber = dto.RoomNumber;
            room.Price = dto.Price;
            room.Type = dto.Type;
            room.IsAvailable = dto.IsAvailable;
            room.HotelId = dto.HotelId;

            _roomRepository.Update(room);
            await _roomRepository.SaveChangesAsync();

            return await GetByIdAsync(room.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room != null)
            {
                _roomRepository.Delete(room);
                await _roomRepository.SaveChangesAsync();
            }
        }
    }
}
