using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonasAPI.Data;
using PersonasAPI.Models;

namespace PersonasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Personas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas(string? rut = null)
        {
            if (rut == null)
            {
                return await _context.Personas.ToListAsync();
            }

            var personas = await _context.Personas
                .Where(p => p.RUT == rut)
                .ToListAsync();

            if (!personas.Any())
            {
                return NotFound($"No se encontraron personas con el RUT {rut}");
            }

            return personas;
        }


        // GET: api/Personas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }



        // PUT: api/Personas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, Persona persona)
        {
            if (id != persona.IDPersona)
            {
                return BadRequest();
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
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

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.IDPersona == id);
        }
    }
}