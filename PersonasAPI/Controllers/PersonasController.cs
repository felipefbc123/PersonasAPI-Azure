using Microsoft.AspNetCore.Mvc;using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;using Microsoft.EntityFrameworkCore;

using PersonasAPI.Data;using PersonasAPI.Data;

using PersonasAPI.Models;using PersonasAPI.Models;



namespace PersonasAPI.Controllersnamespace PersonasAPI.Controllers

{{

    [Route("api/[controller]")]    [Route("api/[controller]")]

    [ApiController]    [ApiController]

    public class PersonasController : ControllerBase    public class PersonasController : ControllerBase

    {    {

        private readonly ApplicationDbContext _context;        private readonly ApplicationDbContext _context;

        private readonly ILogger<PersonasController> _logger;        private readonly ILogger<PersonasController> _logger;



        public PersonasController(ApplicationDbContext context, ILogger<PersonasController> logger)        public PersonasController(ApplicationDbContext context, ILogger<PersonasController> logger)

        {        {

            _context = context;            _context = context;

            _logger = logger;            _logger = logger;

        }        }



        // GET: api/Personas        // GET: api/Personas

        [HttpGet]        [HttpGet]

        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas(string? rut = null)        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas(string? rut = null)

        {        {

            try            try

            {            {

                _logger.LogInformation($"Attempting to get personas. RUT filter: {rut ?? "none"}");                _logger.LogInformation($"Attempting to get personas. RUT filter: {rut ?? "none"}");



                IQueryable<Persona> query = _context.Personas;                IQueryable<Persona> query = _context.Personas;



                if (!string.IsNullOrEmpty(rut))                if (!string.IsNullOrEmpty(rut))

                {                {

                    query = query.Where(p => p.RUT == rut);                    query = query.Where(p => p.RUT == rut);

                }                }



                var personas = await query.ToListAsync();                var personas = await query.ToListAsync();

                _logger.LogInformation($"Retrieved {personas.Count} personas");                _logger.LogInformation($"Retrieved {personas.Count} personas");



                if (!personas.Any() && !string.IsNullOrEmpty(rut))                if (!personas.Any() && !string.IsNullOrEmpty(rut))

                {                {

                    _logger.LogWarning($"No personas found with RUT: {rut}");                    _logger.LogWarning($"No personas found with RUT: {rut}");

                    return NotFound($"No se encontró ninguna persona con el RUT: {rut}");                    return NotFound($"No se encontró ninguna persona con el RUT: {rut}");

                }                }



                return Ok(personas);                return Ok(personas);

            }

            catch (Exception ex)                return Ok(personas);

            {            }

                _logger.LogError(ex, "Error retrieving personas");            catch (Exception ex)

                return StatusCode(500, "Error interno del servidor al recuperar los datos.");            {

            }                _logger.LogError(ex, "Error retrieving personas");

        }                return StatusCode(500, "Error interno del servidor al recuperar los datos.");

            }

        // GET: api/Personas/{id}        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Persona>> GetPersona(int id)        // GET: api/Personas/{id}

        {        [HttpGet("{id}")]

            try        public async Task<ActionResult<Persona>> GetPersona(int id)

            {        {

                _logger.LogInformation($"Attempting to get persona with ID: {id}");            try

            {

                var persona = await _context.Personas.FindAsync(id);                _logger.LogInformation($"Attempting to get persona with ID: {id}");



                if (persona == null)                var persona = await _context.Personas.FindAsync(id);

                {

                    _logger.LogWarning($"Persona with ID {id} not found");                if (persona == null)

                    return NotFound($"No se encontró la persona con ID: {id}");                {

                }                    _logger.LogWarning($"Persona with ID {id} not found");

                    return NotFound($"No se encontró la persona con ID: {id}");

                _logger.LogInformation($"Successfully retrieved persona with ID: {id}");                }

                return Ok(persona);

            }                _logger.LogInformation($"Successfully retrieved persona with ID: {id}");

            catch (Exception ex)                return Ok(persona);

            {            }

                _logger.LogError(ex, $"Error retrieving persona with ID: {id}");            catch (Exception ex)

                return StatusCode(500, "Error interno del servidor al recuperar los datos.");            {

            }                _logger.LogError(ex, $"Error retrieving persona with ID: {id}");

        }                return StatusCode(500, "Error interno del servidor al recuperar los datos.");

            }

        // POST: api/Personas        }

        [HttpPost]

        public async Task<ActionResult<Persona>> PostPersona(Persona persona)        // POST: api/Personas

        {        [HttpPost]

            try        public async Task<ActionResult<Persona>> PostPersona(Persona persona)

            {        {

                _logger.LogInformation($"Attempting to create new persona with RUT: {persona.RUT}");            try

            {

                // Verificar si ya existe una persona con el mismo RUT                _logger.LogInformation($"Attempting to create new persona with RUT: {persona.RUT}");

                if (await _context.Personas.AnyAsync(p => p.RUT == persona.RUT))

                {                // Verificar si ya existe una persona con el mismo RUT

                    _logger.LogWarning($"Attempt to create duplicate persona with RUT: {persona.RUT}");                if (await _context.Personas.AnyAsync(p => p.RUT == persona.RUT))

                    return Conflict($"Ya existe una persona con el RUT: {persona.RUT}");                {

                }                    _logger.LogWarning($"Attempt to create duplicate persona with RUT: {persona.RUT}");

                    return Conflict($"Ya existe una persona con el RUT: {persona.RUT}");

                _context.Personas.Add(persona);                }

                await _context.SaveChangesAsync();

                _context.Personas.Add(persona);

                _logger.LogInformation($"Successfully created persona with ID: {persona.Id}");                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);

            }                _logger.LogInformation($"Successfully created persona with ID: {persona.Id}");

            catch (Exception ex)                return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);

            {            }

                _logger.LogError(ex, "Error creating persona");            catch (Exception ex)

                return StatusCode(500, "Error interno del servidor al crear la persona.");            {

            }                _logger.LogError(ex, "Error creating persona");

        }                return StatusCode(500, "Error interno del servidor al crear la persona.");

            }

        // PUT: api/Personas/{id}        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutPersona(int id, Persona persona)        // PUT: api/Personas/{id}

        {        [HttpPut("{id}")]

            try        public async Task<IActionResult> PutPersona(int id, Persona persona)

            {        {

                _logger.LogInformation($"Attempting to update persona with ID: {id}");            try

            {

                if (id != persona.Id)                _logger.LogInformation($"Attempting to update persona with ID: {id}");

                {

                    _logger.LogWarning($"ID mismatch. Path ID: {id}, Body ID: {persona.Id}");                if (id != persona.Id)

                    return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la solicitud.");                {

                }                    _logger.LogWarning($"ID mismatch. Path ID: {id}, Body ID: {persona.Id}");

                    return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la solicitud.");

                // Verificar si existe otra persona con el mismo RUT (excluyendo la actual)                }

                if (await _context.Personas.AnyAsync(p => p.RUT == persona.RUT && p.Id != id))

                {                // Verificar si existe otra persona con el mismo RUT (excluyendo la actual)

                    _logger.LogWarning($"Attempt to update to duplicate RUT: {persona.RUT}");                if (await _context.Personas.AnyAsync(p => p.RUT == persona.RUT && p.Id != id))

                    return Conflict($"Ya existe otra persona con el RUT: {persona.RUT}");                {

                }                    _logger.LogWarning($"Attempt to update to duplicate RUT: {persona.RUT}");

                    return Conflict($"Ya existe otra persona con el RUT: {persona.RUT}");

                _context.Entry(persona).State = EntityState.Modified;                }



                try                _context.Entry(persona).State = EntityState.Modified;

                {

                    await _context.SaveChangesAsync();                try

                    _logger.LogInformation($"Successfully updated persona with ID: {id}");                {

                }                    await _context.SaveChangesAsync();

                catch (DbUpdateConcurrencyException ex)                    _logger.LogInformation($"Successfully updated persona with ID: {id}");

                {                }

                    if (!await PersonaExists(id))                catch (DbUpdateConcurrencyException ex)

                    {                {

                        _logger.LogWarning($"Persona with ID {id} not found during update");                    if (!await PersonaExists(id))

                        return NotFound($"No se encontró la persona con ID: {id}");                    {

                    }                        _logger.LogWarning($"Persona with ID {id} not found during update");

                    throw;                        return NotFound($"No se encontró la persona con ID: {id}");

                }                    }

                    throw;

                return NoContent();                }

            }

            catch (Exception ex)                return NoContent();

            {            }

                _logger.LogError(ex, $"Error updating persona with ID: {id}");            catch (Exception ex)

                return StatusCode(500, "Error interno del servidor al actualizar la persona.");            {

            }                _logger.LogError(ex, $"Error updating persona with ID: {id}");

        }                return StatusCode(500, "Error interno del servidor al actualizar la persona.");

            }

        // DELETE: api/Personas/{id}        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletePersona(int id)        // DELETE: api/Personas/{id}

        {        [HttpDelete("{id}")]

            try        public async Task<IActionResult> DeletePersona(int id)

            {        {

                _logger.LogInformation($"Attempting to delete persona with ID: {id}");            try

            {

                var persona = await _context.Personas.FindAsync(id);                _logger.LogInformation($"Attempting to delete persona with ID: {id}");

                if (persona == null)

                {                var persona = await _context.Personas.FindAsync(id);

                    _logger.LogWarning($"Persona with ID {id} not found for deletion");                if (persona == null)

                    return NotFound($"No se encontró la persona con ID: {id}");                {

                }                    _logger.LogWarning($"Persona with ID {id} not found for deletion");

                    return NotFound($"No se encontró la persona con ID: {id}");

                _context.Personas.Remove(persona);                }

                await _context.SaveChangesAsync();

                _context.Personas.Remove(persona);

                _logger.LogInformation($"Successfully deleted persona with ID: {id}");                await _context.SaveChangesAsync();

                return NoContent();

            }                _logger.LogInformation($"Successfully deleted persona with ID: {id}");

            catch (Exception ex)                return NoContent();

            {            }

                _logger.LogError(ex, $"Error deleting persona with ID: {id}");            catch (Exception ex)

                return StatusCode(500, "Error interno del servidor al eliminar la persona.");            {

            }                _logger.LogError(ex, $"Error deleting persona with ID: {id}");

        }                return StatusCode(500, "Error interno del servidor al eliminar la persona.");

            }

        private async Task<bool> PersonaExists(int id)        }

        {

            return await _context.Personas.AnyAsync(e => e.Id == id);        private async Task<bool> PersonaExists(int id)

        }        {

    }            return await _context.Personas.AnyAsync(e => e.Id == id);

}        }

                return personas;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving personas");
                return StatusCode(500, "Error interno del servidor al recuperar los datos.");
            }

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

        // POST: api/Personas
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(Persona persona)
        {
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersona), new { id = persona.IDPersona }, persona);
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