/*using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using deneme.Data;
using deneme.Model; // Orderdrink sınıfını dahil etmek için

[ApiController]
[Route("[controller]")]
public class OrderdrinksController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrderdrinksController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "getOrderdrinks")]
    public IEnumerable<Orderdrink> Get()
    {
        return _context.Orderdrink.ToList();
    }

    [HttpPost(Name = "createOrderdrink")]
    public IActionResult Post(Orderdrink orderdrink)
    {
        _context.Orderdrink.Add(orderdrink);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = orderdrink.id }, orderdrink);
    }
    [HttpPut("{id}")]
    public IActionResult Put(int id, Orderdrink orderdrink)
    {
        if (id != orderdrink.id)
        {
            return BadRequest();
        }

        var existingOrderdrink = _context.Orderdrink.Find(id);
        if (existingOrderdrink == null)
        {
            return NotFound();
        }

        // Gerekli alanları güncelle
        existingOrderdrink.piece = orderdrink.piece;
       
        // Diğer alanlar burada güncellenebilir

        _context.Orderdrink.Update(existingOrderdrink);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var orderdrink = _context.Orderdrink.Find(id);
        if (orderdrink == null)
        {
            return NotFound();
        }

        _context.Orderdrink.Remove(orderdrink);
        _context.SaveChanges();

        return NoContent();
    }




}*/