using TravelBooking.Application.Extensions; 
using TravelBooking.Infrastructure.Extensions; 
using TravelBooking.Api.Endpoints; 
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Servis Ekleme ---
// Infrastructure Servisleri (DB Context ve Repository)
builder.Services.AddInfrastructureServices(builder.Configuration);

// Application Servisleri (UserService, vb.)
builder.Services.AddApplicationServices();

// Yetkilendirme için gerekli temel servis eklendi (KRİTİK DÜZELTME)
builder.Services.AddAuthorization(); 

// Minimal API ve Swagger/OpenAPI desteği
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// --- 2. JWT Konfigürasyonu ---
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("JWT Key bulunamadı.");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// --- 3. CORS Konfigürasyonu ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.AllowAnyOrigin() 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// --- 4. Middleware Yapılandırması ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS Kullanımı
app.UseCors("AllowSpecificOrigin"); 

// Kimlik Doğrulama ve Yetkilendirme sırası önemlidir
app.UseAuthentication();
app.UseAuthorization(); // Bu noktada hata veriyordu, şimdi servisler tanımlı olduğu için sorun çözülmeli

// Minimal API Endpoint'lerini haritala
app.MapAuthEndpoints(); 

app.Run();