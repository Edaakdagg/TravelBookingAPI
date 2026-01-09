using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TravelBooking.Api.Middleware; 
using TravelBooking.Application.Extensions;
using TravelBooking.Infrastructure.Data;
using TravelBooking.Infrastructure.Extensions;
using Microsoft.OpenApi.Models; 
using TravelBooking.Api.Endpoints; // Endpointleri dahil etmek için
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------------------------------
// I. SERVİSLERİ KAYDETME (Dependency Injection)
// --------------------------------------------------------------------------

// 1. Veritabanı Bağlantısını ve Repository'leri Ekle
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddInfrastructureServices(connectionString);

// 2. İş Mantığı Servislerini Ekle
builder.Services.AddApplicationServices();

// 3. JWT Authentication Ayarı
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
builder.Services.AddAuthorization(); 

// 4. Controllers
builder.Services.AddControllers();

// 5. Swagger/OpenAPI Ayarı
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TravelBookingAPI", Version = "v1" });
    
    // JWT için Swagger'a güvenlik şeması ekleme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Lütfen 'Bearer ' + token formatında JWT Token giriniz",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

// --------------------------------------------------------------------------
// II. MİDDLEWARE'LERİ YAPILANDIRMA
// --------------------------------------------------------------------------

// 1. Global Exception Handling Middleware (Zorunlu)
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Uygulama başlangıcında Migration'ları Uygula (Seed Data'yı da burada yapabilirsiniz)
    try
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate(); 
        }
    }
    catch (Exception ex)
    {
        // Loglama gereklidir.
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veritabanı migration sırasında bir hata oluştu.");
    }
}

app.UseHttpsRedirection();

// 2. Authentication ve Authorization Middleware'leri
app.UseAuthentication();
app.UseAuthorization();

// 3. Endpointleri Tanımlama (Minimal API)
// 3. Endpointleri Tanımlama (Minimal API)
AuthEndpoints.Map(app);
CityEndpoints.Map(app);
HotelEndpoints.Map(app);
RoomEndpoints.Map(app);
ReservationEndpoints.Map(app);

// 4. Controllers Tanımlama
app.MapControllers();


app.Run();