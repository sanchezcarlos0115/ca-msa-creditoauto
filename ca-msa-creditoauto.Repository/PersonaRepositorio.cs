using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace camsacreditoauto.Repository;


public class PersonaRepositorio : IPersonaRepositorio
{
    private readonly CreditoAutoContext appDbContext;

    public PersonaRepositorio(CreditoAutoContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<Persona> ObtenerPersona(int personaId)
    {
        return await appDbContext.Personas
            .FirstOrDefaultAsync(e => e.PersonaId == personaId) ?? null!;
    }

    public bool ExistePersona(string identificacion)
    {
        return  appDbContext.Personas
            .Any(e => e.Identificacion == identificacion);
    }

    public async Task<int> AgregarPersona(Persona persona)
    {
        try
        {
            var result = await appDbContext.Personas.AddAsync(persona);
            await appDbContext.SaveChangesAsync();
            return result.Entity.PersonaId;
        }
        catch (Exception ex)
        {
           return 0;
        }   
    }

    public async Task<Persona> ActualizarPersona(int personaId,Persona persona)
    {
        try
        {
            var result = await appDbContext.Personas
           .FirstOrDefaultAsync(e => e.PersonaId == personaId);
            if (result is not null)
            {
                result.Nombres = persona.Nombres;
                result.Apellidos = persona.Apellidos;
                result.Edad = persona.Edad;
                result.Direccion = persona.Direccion;
                result.Telefono = persona.Telefono;
                await appDbContext.SaveChangesAsync();
            }
            return result ?? null!;
        }
        catch (Exception)
        {

            return null!;
        }
      
    }

    public async Task<bool> EliminarPersona(int personaId)
    {
        try
        {
            var result = await appDbContext.Personas
           .FirstOrDefaultAsync(e => e.PersonaId == personaId);
            if (result is not null)
            {
                appDbContext.Personas.Remove(result);
                var affect = await appDbContext.SaveChangesAsync();
                return affect > 0;
            }
            return false;
        }
        catch (Exception)
        {

            return false;
        }
       
    }
}