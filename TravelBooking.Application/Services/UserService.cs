using TravelBooking.Application.Interfaces;
using TravelBooking.Application.DTOs.User;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration; // IConfiguration için
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; 
using System.Security.Cryptography; // Şifreleme için

namespace TravelBooking.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IGenericRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public Task<UserResponseDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException("Diğer CRUD metotları tamamlanmadı.");
        }

        public async Task<UserResponseDto> RegisterAsync(UserCreateDto dto)
        {
            // 1. Kullanıcının var olup olmadığını kontrol et (409 Conflict)
            var existingUser = (await _userRepository.FindAsync(u => u.Email == dto.Email)).FirstOrDefault();
            if (existingUser != null)
            {
                throw new Exception("Kullanıcı zaten mevcut. Conflict"); 
            }

            // 2. Şifreyi Hash'le (Gerçek implementasyon)
            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // 3. Entity oluştur ve kaydet
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "User",
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            // 4. Response DTO
            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }
        
        public async Task<(string Token, UserResponseDto User)> LoginAsync(UserLoginDto dto)
        {
            var user = (await _userRepository.FindAsync(u => u.Email == dto.Email)).FirstOrDefault();

            // 1. Kullanıcı yoksa veya şifre yanlışsa (401 Unauthorized)
            if (user == null || !VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new UnauthorizedAccessException("E-posta veya parola yanlış.");
            }
            
            // 2. JWT Token oluştur
            var token = CreateToken(user);

            // 3. Response DTO
            var userDto = new UserResponseDto 
            { 
                Id = user.Id, 
                Username = user.Username, 
                Email = user.Email, 
                Role = user.Role, 
                CreatedAt = user.CreatedAt 
            };

            return (token, userDto);
        }

        // --- Şifre Yardımcı Metotları ---
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        // --- JWT Token Oluşturma ---
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var tokenKey = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("JWT Key bulunamadı.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}