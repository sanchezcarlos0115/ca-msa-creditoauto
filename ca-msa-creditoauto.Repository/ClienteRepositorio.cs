using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace camsacreditoauto.Repository;


public class ClienteRepositorio : IClienteRepositorio
{
    private readonly CreditoAutoContext appDbContext;

    public ClienteRepositorio(CreditoAutoContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Cliente>> ObtenerClientes()
    {
        var clientes = await appDbContext.Clientes
                           .Include(p => p.Persona).ToListAsync();
        return clientes;
    }

    public async Task<Cliente> ObtenerCliente(int clienteId)
    {

        var cliente = await appDbContext.Clientes
                           .Where(c => c.ClienteId == clienteId)
                           .Include(p => p.Persona)
                           .FirstOrDefaultAsync();

        return cliente ?? null!;
    }

    public async Task<int> AgregarCliente(Cliente cliente)
    {
        try
        {
            var result = await appDbContext.Clientes.AddAsync(cliente);
            await appDbContext.SaveChangesAsync();
            return result.Entity.ClienteId;
        }
        catch (Exception)
        {

            return 0;
        }
       
    }

    public async Task<Cliente> ActualizarCliente(int clienteId,Cliente cliente)
    {
        try
        {
            var result = await appDbContext.Clientes
                          .Where(c => c.ClienteId == clienteId)
                          .Include(p => p.Persona)
                          .FirstOrDefaultAsync();
            if (result != null)
            {
                result.PersonaId = cliente.PersonaId;
                result.FechaNacimiento = cliente.FechaNacimiento;
                result.EstadoCivil = cliente.EstadoCivil;
                result.IdentificacionConyuge = cliente.IdentificacionConyuge;
                result.NombresConyuge = cliente.NombresConyuge;
                result.SujetoCredito = cliente.SujetoCredito;
                await appDbContext.SaveChangesAsync();
            }

            return result ?? null!;
        }
        catch (Exception)
        {

            return  null!;
        }
        
    }

   
    public async Task<bool> EliminarCliente(int clienteId)
    {
        try
        {
            var result = await appDbContext.Clientes
           .FirstOrDefaultAsync(e => e.ClienteId == clienteId);
            if (result is not null)
            {
                appDbContext.Clientes.Remove(result);
                var result2 = await appDbContext.Personas.FirstOrDefaultAsync(e => e.PersonaId == result.PersonaId);
                if (result2 is not null)
                    appDbContext.Personas.Remove(result2);
                
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