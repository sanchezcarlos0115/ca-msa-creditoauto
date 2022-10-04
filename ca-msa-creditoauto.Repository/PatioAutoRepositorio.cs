using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace camsacreditoauto.Repository;


public class PatioAutoRepositorio : IPatioAutoRepositorio
{
    private readonly CreditoAutoContext appDbContext;

    public PatioAutoRepositorio(CreditoAutoContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<IEnumerable<PatioAuto>> ObtenerPatioAutos()
    {
        var objResult = await appDbContext.PatioAutos.ToListAsync();
        return objResult;
    }

    public async Task<bool> ExistePatioAuto(string nombre)
    {
        return await appDbContext.PatioAutos.AnyAsync(z => z.Nombre.ToLower() == nombre.ToLower());
    }

    public async Task<PatioAuto> ObtenerPatioAuto(int patioAutoId)
    {

        var objResult = await appDbContext.PatioAutos
                           .Where(c => c.PatioId == patioAutoId)
                           .FirstOrDefaultAsync();

        return objResult ?? null!;
    }

    public async Task<int> AgregarPatioAuto(PatioAuto obj)
    {
        try
        {
            var result = await appDbContext.PatioAutos.AddAsync(obj);
            await appDbContext.SaveChangesAsync();
            return result.Entity.PatioId;
        }
        catch (Exception)
        {

            return 0;
        }
    }

    public async Task<PatioAuto> ActualizarPatioAuto(int patioId, PatioAuto obj)
    {
        try
        {
            var result = await appDbContext.PatioAutos
                          .Where(c => c.PatioId == patioId)
                          .FirstOrDefaultAsync();
            if (result != null)
            {
                result.Nombre = obj.Nombre;
                result.Direccion = obj.Direccion;
                result.Telefono = obj.Telefono;
                result.NumeroPtoVenta = obj.NumeroPtoVenta;
                await appDbContext.SaveChangesAsync();
            }
            return result ?? null!;
        }
        catch (Exception)
        {
            return  null!;
        }
    }

   
    public async Task<bool> EliminarPatioAuto(int patioId)
    {
        try
        {
            var result = await appDbContext.PatioAutos
           .FirstOrDefaultAsync(e => e.PatioId == patioId);
            if (result is not null)
            {
                appDbContext.PatioAutos.Remove(result);
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