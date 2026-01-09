
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelBooking.Application.DTOs.City;
using TravelBooking.Application.DTOs.Hotel;
using TravelBooking.Application.Interfaces;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IGenericRepository<Hotel> _hotelRepository;
        private readonly IGenericRepository<City> _cityRepository; // To include city info if needed

        public HotelService(IGenericRepository<Hotel> hotelRepository, IGenericRepository<City> cityRepository)
        {
            _hotelRepository = hotelRepository;
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<HotelResponseDto>> GetAllAsync()
        {
            // Ideally use Includes here if GenericRepository supports it
            var hotels = await _hotelRepository.GetAllAsync();
            var dtos = new List<HotelResponseDto>();
            foreach (var h in hotels)
            {
                var city = await _cityRepository.GetByIdAsync(h.CityId);
                dtos.Add(new HotelResponseDto
                {
                    Id = h.Id,
                    Name = h.Name,
                    Address = h.Address,
                    Rating = h.Rating,
                    CityId = h.CityId,
                    City = city != null ? new CityResponseDto { Id = city.Id, Name = city.Name, Country = city.Country } : null
                });
            }
            return dtos;
        }

        public async Task<HotelResponseDto> GetByIdAsync(int id)
        {
            var h = await _hotelRepository.GetByIdAsync(id);
            if (h == null) return null;

            var city = await _cityRepository.GetByIdAsync(h.CityId);

            return new HotelResponseDto
            {
                Id = h.Id,
                Name = h.Name,
                Address = h.Address,
                Rating = h.Rating,
                CityId = h.CityId,
                City = city != null ? new CityResponseDto { Id = city.Id, Name = city.Name, Country = city.Country } : null
            };
        }

        public async Task<HotelResponseDto> CreateAsync(HotelCreateDto dto)
        {
            var hotel = new Hotel
            {
                Name = dto.Name,
                Address = dto.Address,
                Rating = dto.Rating,
                CityId = dto.CityId
            };
            await _hotelRepository.AddAsync(hotel);
            await _hotelRepository.SaveChangesAsync();

            return await GetByIdAsync(hotel.Id);
        }

        public async Task<HotelResponseDto> UpdateAsync(int id, HotelUpdateDto dto)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id);
            if (hotel == null) return null;

            hotel.Name = dto.Name;
            hotel.Address = dto.Address;
            hotel.Rating = dto.Rating;
            hotel.CityId = dto.CityId;

            _hotelRepository.Update(hotel);
            await _hotelRepository.SaveChangesAsync();

            return await GetByIdAsync(hotel.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id);
            if (hotel != null)
            {
                _hotelRepository.Delete(hotel);
                await _hotelRepository.SaveChangesAsync();
            }
        }
    }
}
