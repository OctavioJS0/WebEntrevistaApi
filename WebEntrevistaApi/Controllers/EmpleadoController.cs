using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebEntrevistaApi.Context;
using WebEntrevistaApi.Models;

namespace WebEntrevistaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmpleadoController : ControllerBase
{
    private readonly EntrevistaDbContext _entrevistaDbContext;

    public EmpleadoController(EntrevistaDbContext entrevistaDbContext)
    {
        _entrevistaDbContext = entrevistaDbContext;
    }
    
    // Método GET, obtiene todos los datos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
    {
        return await _entrevistaDbContext.Empleados.ToListAsync();
    }

    // Método GET, obtiene por id, solo uno
    [HttpGet("{id}")]
    public async Task<ActionResult<Empleado>> GetEmpleadoById(int id)
    {
        var empleado = await _entrevistaDbContext.Empleados.FindAsync(id);

        if (empleado is null)
        {
            return NotFound();
        }

        return empleado;
    }

    // Método POST, crea un empleado
    [HttpPost]
    public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
    {
        _entrevistaDbContext.Empleados.Add(empleado);
        await _entrevistaDbContext.SaveChangesAsync();

        return CreatedAtAction("GetEmpleadoById", new { id = empleado.Id }, empleado);
    }

    // Método PUT, modifica un empleado
    [HttpPut]
    public async Task<ActionResult> PutEmpleado(int id, Empleado empleado)
    {
        if (id != empleado.Id)
        {
            return BadRequest();
        }

        _entrevistaDbContext.Entry(empleado).State = EntityState.Modified;

        try
        {
            await _entrevistaDbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmpleadoExists(id))
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

    // Método DELETE, elimina un empleado
    [HttpDelete]
    public async Task<ActionResult> DeleteEmpleado(int id)
    {
        var empleado = await _entrevistaDbContext.Empleados.FindAsync(id);
        if (empleado is null)
        {
            return NotFound();
        }

        _entrevistaDbContext.Remove(empleado);
        await _entrevistaDbContext.SaveChangesAsync();

        return NoContent();
    }

    // Excepción de PUT
    private bool EmpleadoExists(int id)
    {
        return _entrevistaDbContext.Empleados.Any(e => e.Id == id);
    }
}