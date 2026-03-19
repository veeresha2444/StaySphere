using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaySphere.API.Data;
using StaySphere.API.Models;
using StaySphere.API.DTOs;

namespace StaySphere.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomestaysController : ControllerBase
{
    private readonly AppDbContext _context;

    public HomestaysController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? location,
        [FromQuery] decimal? maxPrice,
        [FromQuery] bool? isAvailable)
    {
        var query = _context.Homestays.AsQueryable();

        if (!string.IsNullOrEmpty(location))
            query = query.Where(h => h.Location.Contains(location));

        if (maxPrice.HasValue)
            query = query.Where(h => h.PricePerNight <= maxPrice.Value);

        if (isAvailable.HasValue)
            query = query.Where(h => h.IsAvailable == isAvailable.Value);

        var homestays = await query.ToListAsync();
        return Ok(homestays);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var homestay = await _context.Homestays.FindAsync(id);
        if (homestay == null)
            return NotFound();
        return Ok(homestay);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateHomestayDto dto)
    {
        var homestay = new Homestay
        {
            Name = dto.Name,
            Location = dto.Location,
            PricePerNight = dto.PricePerNight,
            IsAvailable = dto.IsAvailable,
            Description = dto.Description
        };

        _context.Homestays.Add(homestay);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = homestay.Id }, homestay);
    }
    [HttpDelete("{id}")]
[Authorize(Roles = "Admin")]
public async Task<IActionResult> Delete(int id)
{
    var homestay = await _context.Homestays.FindAsync(id);
    if (homestay == null)
        return NotFound();

    _context.Homestays.Remove(homestay);
    await _context.SaveChangesAsync();

    return NoContent();
}
}