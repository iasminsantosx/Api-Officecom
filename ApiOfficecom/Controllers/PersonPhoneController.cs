using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PersonPhoneController : ControllerBase
{
    private readonly AppDbContext _context;

    public PersonPhoneController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonPhone>>> GetAll()
    {
        return await _context.PersonPhones.ToListAsync();
    }

    // Atualizado para buscar pelo BusinessEntityID e PhoneNumberTypeID
    [HttpGet("{businessEntityId}/{phoneNumberTypeId}")]
    public async Task<ActionResult<PersonPhone>> GetById(int businessEntityId, int phoneNumberTypeId)
    {
        var personPhone = await _context.PersonPhones
            .FirstOrDefaultAsync(pp => pp.BusinessEntityID == businessEntityId && pp.PhoneNumberTypeID == phoneNumberTypeId);

        if (personPhone == null)
        {
            return NotFound();
        }

        return personPhone;
    }

    [HttpPost]
    public async Task<ActionResult<PersonPhone>> Add(PersonPhone personPhone)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.PersonPhones.Add(personPhone);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { businessEntityId = personPhone.BusinessEntityID, phoneNumberTypeId = personPhone.PhoneNumberTypeID }, personPhone);
    }

    [HttpPut("{businessEntityId}/{phoneNumberTypeId}")]
    public async Task<IActionResult> Update(int businessEntityId, int phoneNumberTypeId, PersonPhone personPhone)
    {
        if (businessEntityId != personPhone.BusinessEntityID || phoneNumberTypeId != personPhone.PhoneNumberTypeID)
        {
            return BadRequest();
        }

        _context.Entry(personPhone).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.PersonPhones.Any(e => e.BusinessEntityID == businessEntityId && e.PhoneNumberTypeID == phoneNumberTypeId))
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

    // Atualizado para excluir pelo BusinessEntityID e PhoneNumberTypeID
    [HttpDelete("{businessEntityId}/{phoneNumberTypeId}")]
    public async Task<IActionResult> Delete(int businessEntityId, int phoneNumberTypeId)
    {
        var personPhone = await _context.PersonPhones
            .FirstOrDefaultAsync(pp => pp.BusinessEntityID == businessEntityId && pp.PhoneNumberTypeID == phoneNumberTypeId);

        if (personPhone == null)
        {
            return NotFound();
        }

        _context.PersonPhones.Remove(personPhone);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
