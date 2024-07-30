using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PhoneNumberTypeController : ControllerBase
{
    private readonly AppDbContext _context;

    public PhoneNumberTypeController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PhoneNumberType>>> GetAll()
    {
        return await _context.PhoneNumberTypes.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PhoneNumberType>> GetById(int id)
    {
        var phoneNumberType = await _context.PhoneNumberTypes.FindAsync(id);
        if (phoneNumberType == null)
        {
            return NotFound();
        }
        return phoneNumberType;
    }

    [HttpPost]
    public async Task<ActionResult<PhoneNumberType>> Add(PhoneNumberType phoneNumberType)
    {
        _context.PhoneNumberTypes.Add(phoneNumberType);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = phoneNumberType.PhoneNumberTypeID }, phoneNumberType);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PhoneNumberType phoneNumberType)
    {
        if (id != phoneNumberType.PhoneNumberTypeID)
        {
            return BadRequest();
        }

        _context.Entry(phoneNumberType).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.PhoneNumberTypes.Any(e => e.PhoneNumberTypeID == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var phoneNumberType = await _context.PhoneNumberTypes.FindAsync(id);
        if (phoneNumberType == null)
        {
            return NotFound();
        }

        _context.PhoneNumberTypes.Remove(phoneNumberType);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
