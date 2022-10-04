using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace camsacreditoauto.Repository;


public class ClientePatioRepositorio : IClientePatioRepositorio
{
    private readonly CreditoAutoContext appDbContext;

    public ClientePatioRepositorio(CreditoAutoContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<IEnumerable<ClientePatio>> ObtenerClientePatios()
    {
        var objResult = await appDbContext.ClientePatios.ToListAsync();
        return objResult;
    }

    public async Task<bool> ExisteClienteAsignado(int clienteId)
    {
        return await appDbContext.ClientePatios.AnyAsync(z => z.ClienteId == clienteId);
    }

    public async Task<bool> ExistePatioAutoAsignado(int patioId)
    {
        return await appDbContext.ClientePatios.AnyAsync(z => z.PatioId == patioId);
    }

    public async Task<ClientePatio> ObtenerClientePatio(int id)
    {

        var objResult = await appDbContext.ClientePatios
                           .Where(c => c.ClientePatioId == id)
                           .FirstOrDefaultAsync();

        return objResult ?? null!;
    }

    public async Task<int> AgregarClientePatio(ClientePatio obj)
    {
        try
        {
            var result = await appDbContext.ClientePatios.AddAsync(obj);
            await appDbContext.SaveChangesAsync();
            return result.Entity.ClientePatioId;
        }
        catch (Exception)
        {

            return 0;
        }
    }

    public async Task<ClientePatio> ActualizarClientePatio(int id, ClientePatio obj)
    {
        try
        {
            var result = await appDbContext.ClientePatios
                          .Where(c => c.ClientePatioId == id)
                          .FirstOrDefaultAsync();
            if (result != null)
            {
                result.ClienteId = obj.ClienteId;
                result.PatioId = obj.PatioId;
                result.FechaAsignacion = DateTime.Now;
                await appDbContext.SaveChangesAsync();
            }
            return result ?? null!;
        }
        catch (Exception)
        {
            return  null!;
        }
    }

   
    public async Task<bool> EliminarClientePatio(int id)
    {
        try
        {
            var result = await appDbContext.ClientePatios
           .FirstOrDefaultAsync(e => e.ClientePatioId == id);
            if (result is not null)
            {
                appDbContext.ClientePatios.Remove(result);
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