using Microsoft.EntityFrameworkCore; // Entity Framework Core için gerekli namespace
using Microsoft.Extensions.DependencyInjection; // Dependency Injection (DI) için gerekli namespace
using Microsoft.Extensions.Hosting; // Host yapýlandýrmasý için gerekli namespace
using deneme.Data; // Veritabaný baðlamýný içeren namespace

var builder = WebApplication.CreateBuilder(args); // Uygulama yapýlandýrmasýný oluþturur

// Servisleri uygulama konteynerine ekleme
builder.Services.AddControllers(); // Controller sýnýflarýný kullanabilmek için gerekli servis
builder.Services.AddEndpointsApiExplorer(); // API uç noktalarýnýn keþfi için
builder.Services.AddSwaggerGen(); // Swagger dokümantasyonu oluþturmak için

// Veritabaný baðlamýný (DbContext) PostgreSQL ile yapýlandýrma
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS (Cross-Origin Resource Sharing) politikasý ekleme
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", // Politika adý
    policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular uygulamasýnýn URL'si
              .AllowAnyHeader() // Her türlü HTTP baþlýðýný kabul et
              .AllowAnyMethod(); // Her türlü HTTP metodunu kabul et (GET, POST, PUT, DELETE, vb.)
    });
});

var app = builder.Build(); // Uygulamayý inþa et

// CORS politikasýný uygulama
app.UseCors("AllowSpecificOrigins");

// HTTP istek iþleme hattýný yapýlandýrma
if (app.Environment.IsDevelopment()) // Geliþtirme ortamýnda çalýþýyorsa
{
    app.UseSwagger(); // Swagger endpoint'ini etkinleþtir
    app.UseSwaggerUI(); // Swagger kullanýcý arayüzünü etkinleþtir
}

app.UseHttpsRedirection(); // HTTP isteklerini HTTPS'e yönlendir
app.UseAuthorization(); // Yetkilendirme (Authorization) middleware'ini ekle
app.MapControllers(); // Controller uç noktalarýný haritalandýr

app.Run(); // Uygulamayý çalýþtýr