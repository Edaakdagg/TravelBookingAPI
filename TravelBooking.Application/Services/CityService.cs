
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelBooking.Application.DTOs.City;
using TravelBooking.Application.Interfaces;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Services
{
    public class CityService : ICityService
    {
        private readonly IGenericRepository<City> _cityRepository;

        public CityService(IGenericRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<CityResponseDto>> GetAllAsync()
        {
            var cities = await _cityRepository.GetAllAsync();
            return cities.Select(c => new CityResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Country = c.Country
            });
        }

        public async Task<CityResponseDto> GetByIdAsync(int id)
        {
            var city = await _cityRepository.GetByIdAsync(id);
            if (city == null) return null;

            return new CityResponseDto
            {
                Id = city.Id,
                Name = city.Name,
                Country = city.Country
            };
        }

        public async Task<CityResponseDto> CreateAsync(CityCreateDto dto)
        {
            var city = new City
            {
                Name = dto.Name,
                Country = dto.Country
            };
            await _cityRepository.AddAsync(city);
            
            // Assuming SaveChanges is handled in repository or need to call it explicit if UoW is not used. 
            // UserService called SaveChangesAsync, so I should too.
            await _cityRepository.SaveChangesAsync();

            return new CityResponseDto
            {
                Id = city.Id,
                Name = city.Name,
                Country = city.Country
            };
        }

        public async Task<CityResponseDto> UpdateAsync(int id, CityUpdateDto dto)
        {
            var city = await _cityRepository.GetByIdAsync(id);
            if (city == null) return null;

            city.Name = dto.Name;
            city.Country = dto.Country;

            _cityRepository.Update(city);
            await _cityRepository.SaveChangesAsync();

            return new CityResponseDto
            {
                Id = city.Id,
                Name = city.Name,
                Country = city.Country
            };
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _cityRepository.GetByIdAsync(id);
            if (city != null)
            {
                _cityRepository.Delete(city);
                await _cityRepository.SaveChangesAsync();
            }
        }
    }
}
