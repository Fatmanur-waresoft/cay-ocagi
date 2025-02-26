using Microsoft.AspNetCore.Mvc; // ASP.NET Core MVC için gerekli namespace
using System.Collections.Generic; // Koleksiyon türleri için gerekli namespace
using System.Linq; // LINQ işlemleri için gerekli namespace
using deneme.Data; // Veri tabanı bağlantısı için kullanılan namespace
using deneme.Model; // Order model sınıfını içeren namespace

// Bu sınıf, bir API controller'dır ve siparişlerle ilgili CRUD işlemlerini sağlar.
[ApiController]
[Route("[controller]")] // URL yönlendirmesi için temel route belirleniyor.
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context; // Veritabanı işlemleri için kullanılan context

    // Constructor, Dependency Injection ile ApplicationDbContext'i alır.
    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET isteği: Tüm siparişlerin listesini döndürür.
    [HttpGet(Name = "getOrders")] // Bu endpoint, tüm siparişleri getirir.
    public IEnumerable<Order> Get()
    {
        // Veritabanındaki tüm siparişleri listeye çevirip döndürür.
        return _context.Order.ToList();
    }

    // POST isteği: Yeni bir sipariş oluşturur.
    [HttpPost(Name = "createOrder")] // Bu endpoint, yeni sipariş eklemek için kullanılır.
    public IActionResult Post(Order order)
    {
        // Yeni sipariş veritabanına eklenir.
        _context.Order.Add(order);
        _context.SaveChanges(); // Değişiklikler veritabanına kaydedilir.

        // Yeni sipariş oluşturulduğunda 201 Created döner.
        return CreatedAtAction(nameof(Get), new { id = order.id }, order);
    }

    // DELETE isteği: Belirtilen id'ye sahip siparişi siler.
    [HttpDelete("{id}")] // URL'deki {id} parametresini alır.
    public IActionResult Delete(int id)
    {
        // Veritabanında belirtilen id'ye sahip siparişi bulur.
        var order = _context.Order.Find(id);
        if (order == null)
        {
            // Sipariş bulunamazsa 404 Not Found döner.
            return NotFound();
        }

        // Sipariş veritabanından silinir.
        _context.Order.Remove(order);
        _context.SaveChanges(); // Değişiklikler veritabanına kaydedilir.

        // Başarılı işlem durumunda NoContent döner.
        return NoContent();
    }

    // PUT isteği: Belirtilen id'ye sahip siparişi günceller.
    [HttpPut("{id}")] // URL'deki {id} parametresini alır.
    public IActionResult Put(int id, Order order)
    {
        // URL'deki id ile gönderilen siparişin id'si uyuşmuyorsa hata döner.
        if (id != order.id)
        {
            return BadRequest(); // 400 Bad Request
        }

        // Güncellenmek istenen siparişi veritabanında bulur.
        var existingOrder = _context.Order.Find(id);
        if (existingOrder == null)
        {
            // Sipariş bulunamazsa 404 Not Found döner.
            return NotFound();
        }

        // Mevcut siparişin notlarını ve durumunu günceller.
        existingOrder.notes = order.notes;
        existingOrder.situation = order.situation;
        // Diğer güncellenebilir alanlar buraya eklenebilir.

        // Güncellenmiş sipariş bilgilerini veritabanında günceller.
        _context.Order.Update(existingOrder);
        _context.SaveChanges(); // Değişiklikler kaydedilir.

        // Başarılı işlem durumunda NoContent döner.
        return NoContent();
    }
}
