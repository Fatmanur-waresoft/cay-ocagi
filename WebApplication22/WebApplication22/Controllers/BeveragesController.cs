using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; 
using System.Linq; 
using deneme.Data; 
using deneme.Model; 

// B bir API controller'dır ve içeceklerle ilgili CRUD işlemlerini sağlar.
[ApiController]
[Route("[controller]")] // URL yönlendirmesi için temel route belirleniyor.
public class BeveragesController : ControllerBase
{
    private readonly ApplicationDbContext _context; // Veritabanı işlemleri için kullanılan context

    // Constructor, Dependency Injection ile ApplicationDbContext'i alır.
    public BeveragesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET isteği: Tüm içeceklerin listesini döndürür.
    [HttpGet(Name = "cayocagi")]
    public IEnumerable<Beverage> Get()
    {
        // Veritabanındaki tüm içecekleri listeye çevirip döndürür.
        return _context.Beverage.ToList();
    }

    // POST isteği: Yeni bir içecek ekler.
    [HttpPost(Name = "cayocagi")]
    public IActionResult Post(Beverage beverage)
    {
        // Yeni içecek veritabanına eklenir.
        _context.Beverage.Add(beverage);
        _context.SaveChanges(); // Değişiklikler kaydedilir.

        // Başarılı ekleme durumunda CreatedAtAction ile yeni kaydı döndürür.
        return CreatedAtAction(nameof(Get), new { id = beverage.id }, beverage);
    }

    // DELETE isteği: Belirtilen id'ye sahip içeceği siler.
    [HttpDelete("{id}")] 
    public IActionResult Delete(int id)
    {
        // Veritabanında belirtilen id'ye sahip içeceği bulur.
        var beverage = _context.Beverage.Find(id);
        if (beverage == null)
        {
            // İçecek bulunamazsa 404 Not Found döner.
            return NotFound();
        }

        // İçecek veritabanından silinir.
        _context.Beverage.Remove(beverage);
        _context.SaveChanges(); // Değişiklikler kaydedilir.

        // Başarılı işlem durumunda NoContent döner.
        return NoContent();
    }

    // PUT isteği: Belirtilen id'ye sahip içeceği günceller.
    [HttpPut("{id}")] // URL'deki {id} parametresini alır.
    public IActionResult Put(int id, Beverage beverage)
    {
        // URL'deki id ile gönderilen içeceğin id'si uyuşmuyorsa hata döner.
        if (id != beverage.id)
        {
            return BadRequest(); // 400 Bad Request
        }

        // Güncellenmek istenen içeceği veritabanında bulur.
        var existingBeverage = _context.Beverage.Find(id);
        if (existingBeverage == null)
        {
            // İçecek bulunamazsa 404 Not Found döner.
            return NotFound();
        }

        // Mevcut içeceğin fiyatını günceller.
        existingBeverage.price = beverage.price;

        // Güncellenmiş içecek bilgilerini veritabanında günceller.
        _context.Beverage.Update(existingBeverage);
        _context.SaveChanges(); // Değişiklikler kaydedilir.

        // Başarılı işlem durumunda NoContent döner.
        return NoContent();
    }
}
