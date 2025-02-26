using Microsoft.AspNetCore.Mvc; // ASP.NET Core MVC için gerekli namespace
using System.Collections.Generic; // Koleksiyon türleri için gerekli namespace
using System.Linq; // LINQ işlemleri için gerekli namespace
using deneme.Data; // Veri tabanı bağlantısı için kullanılan namespace
using deneme.Model; // Room model sınıfını içeren namespace

// Bu sınıf, bir API controller'dır ve odalarla (Rooms) ilgili CRUD işlemlerini sağlar.
[ApiController]
[Route("[controller]")] // URL yönlendirmesi için temel route belirleniyor.
public class RoomsController : ControllerBase
{
    private readonly ApplicationDbContext _context; // Veritabanı işlemleri için kullanılan context

    // Constructor, Dependency Injection ile ApplicationDbContext'i alır.
    public RoomsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET isteği: Tüm odaların listesini döndürür.
    [HttpGet(Name = "getRooms")] // Bu endpoint, tüm odaları getirir.
    public IEnumerable<Room> Get()
    {
        // Veritabanındaki tüm odaları listeye çevirip döndürür.
        return _context.Room.ToList();
    }

    // POST isteği: Yeni bir oda oluşturur.
    [HttpPost(Name = "createRoom")] // Bu endpoint, yeni bir oda eklemek için kullanılır.
    public IActionResult Post(Room room)
    {
        // Yeni oda veritabanına eklenir.
        _context.Room.Add(room);
        _context.SaveChanges(); // Değişiklikler veritabanına kaydedilir.

        // Yeni oda oluşturulduğunda 201 Created döner.
        return CreatedAtAction(nameof(Get), new { id = room.id }, room);
    }

    // DELETE isteği: Belirtilen id'ye sahip odayı siler.
    [HttpDelete("{id}")] // URL'deki {id} parametresini alır.
    public IActionResult Delete(int id)
    {
        // Veritabanında belirtilen id'ye sahip odayı bulur.
        var room = _context.Room.Find(id);
        if (room == null)
        {
            // Oda bulunamazsa 404 Not Found döner.
            return NotFound();
        }

        // Oda veritabanından silinir.
        _context.Room.Remove(room);
        _context.SaveChanges(); // Değişiklikler veritabanına kaydedilir.

        // Başarılı işlem durumunda NoContent döner.
        return NoContent();
    }

    // PUT isteği: Belirtilen id'ye sahip odayı günceller.
    [HttpPut("{id}")] // URL'deki {id} parametresini alır.
    public IActionResult Put(int id, Room room)
    {
        // URL'deki id ile gönderilen odanın id'si uyuşmuyorsa hata döner.
        if (id != room.id)
        {
            return BadRequest(); // 400 Bad Request
        }

        // Güncellenmek istenen odayı veritabanında bulur.
        var existingRoom = _context.Room.Find(id);
        if (existingRoom == null)
        {
            // Oda bulunamazsa 404 Not Found döner.
            return NotFound();
        }

        // Mevcut odanın adını günceller.
        existingRoom.name = room.name;

        // Güncellenmiş oda bilgilerini veritabanında günceller.
        _context.Room.Update(existingRoom);
        _context.SaveChanges(); // Değişiklikler kaydedilir.

        // Başarılı işlem durumunda NoContent döner.
        return NoContent();
    }
}
