using Microsoft.EntityFrameworkCore; // Entity Framework Core için gerekli namespace
using deneme.Model; // Model sınıflarını içeren namespace

namespace deneme.Data
{
    // Bu sınıf, veritabanı bağlamını (database context) temsil eder ve Entity Framework Core kullanılarak yapılandırılmıştır.
    public class ApplicationDbContext : DbContext
    {
        // Constructor, Dependency Injection ile veritabanı bağlantı ayarlarını alır.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Beverage tablosunu temsil eder.
        public DbSet<Beverage> Beverage { get; set; }

        // Order tablosunu temsil eder.
        public DbSet<Order> Order { get; set; }

        // Room tablosunu temsil eder.
        public DbSet<Room> Room { get; set; }
    }
}





/*public DbSet<Orderdrink> Orderdrink { get; set; } // Orderdrink tablosunu ekle*/