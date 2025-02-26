using Microsoft.EntityFrameworkCore; // Entity Framework Core i�in gerekli namespace
using Microsoft.Extensions.DependencyInjection; // Dependency Injection (DI) i�in gerekli namespace
using Microsoft.Extensions.Hosting; // Host yap�land�rmas� i�in gerekli namespace
using deneme.Data; // Veritaban� ba�lam�n� i�eren namespace

var builder = WebApplication.CreateBuilder(args); // Uygulama yap�land�rmas�n� olu�turur

// Servisleri uygulama konteynerine ekleme
builder.Services.AddControllers(); // Controller s�n�flar�n� kullanabilmek i�in gerekli servis
builder.Services.AddEndpointsApiExplorer(); // API u� noktalar�n�n ke�fi i�in
builder.Services.AddSwaggerGen(); // Swagger dok�mantasyonu olu�turmak i�in

// Veritaban� ba�lam�n� (DbContext) PostgreSQL ile yap�land�rma
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS (Cross-Origin Resource Sharing) politikas� ekleme
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", // Politika ad�
    policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular uygulamas�n�n URL'si
              .AllowAnyHeader() // Her t�rl� HTTP ba�l���n� kabul et
              .AllowAnyMethod(); // Her t�rl� HTTP metodunu kabul et (GET, POST, PUT, DELETE, vb.)
    });
});

var app = builder.Build(); // Uygulamay� in�a et

// CORS politikas�n� uygulama
app.UseCors("AllowSpecificOrigins");

// HTTP istek i�leme hatt�n� yap�land�rma
if (app.Environment.IsDevelopment()) // Geli�tirme ortam�nda �al���yorsa
{
    app.UseSwagger(); // Swagger endpoint'ini etkinle�tir
    app.UseSwaggerUI(); // Swagger kullan�c� aray�z�n� etkinle�tir
}

app.UseHttpsRedirection(); // HTTP isteklerini HTTPS'e y�nlendir
app.UseAuthorization(); // Yetkilendirme (Authorization) middleware'ini ekle
app.MapControllers(); // Controller u� noktalar�n� haritaland�r

app.Run(); // Uygulamay� �al��t�r