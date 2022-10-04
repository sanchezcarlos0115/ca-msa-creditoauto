using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace camsacreditoauto.Repository;

public class VehiculoRepositorio : IVehiculoRepositorio
{
    private readonly CreditoAutoContext appDbContext;

    public VehiculoRepositorio(CreditoAutoContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Vehiculo>> ObtenerVehiculos()
    {
        var objResult = await appDbContext.Vehiculos
            .Include(v=> v.Marca)
            .Include(p=> p.Patio).ToListAsync();
        return objResult;
    }

    public async Task<IEnumerable<Vehiculo>> ObtenerVehiculosFiltrado(string marca, string modelo,bool todos)
    {
        var objResult = await appDbContext.Vehiculos
            .Include(v => v.Marca)
            .Include(p => p.Patio)
            .Where(f=> f.Marca.Nombre == marca || f.Modelo == modelo || todos == true).ToListAsync();
        return objResult;
    }

    public async Task<bool> ValidarExistenciaVehiculoMismaPlaca(string placa)
    {
        return await appDbContext.Vehiculos.AnyAsync(v => v.Placa == placa);
    }

    public async Task<Vehiculo> ObtenerVehiculo(int vehiculoId)
    {

        var cliente = await appDbContext.Vehiculos
                           .Where(v => v.VehiculoId == vehiculoId)
                           .Include(p => p.Patio)
                           .Include(m=> m.Marca)
                           .FirstOrDefaultAsync();

        return cliente ?? null!;
    }


    public async Task<int> AgregarVehiculo(Vehiculo obj)
    {
        try
        {
            var result = await appDbContext.Vehiculos.AddAsync(obj);
            await appDbContext.SaveChangesAsync();
            return result.Entity.VehiculoId;
        }
        catch (Exception)
        {

            return -1;
        }
    }

    public async Task<Vehiculo> ActualizarVehiculo(int id, Vehiculo obj)
    {
        try
        {
            var result = await appDbContext.Vehiculos
                          .Where(c => c.VehiculoId == id)
                          .FirstOrDefaultAsync();
            if (result != null)
            {
                result.Avaluo = obj.Avaluo;
                result.Cilindraje = obj.Cilindraje;
                result.Modelo = obj.Modelo;
                result.MarcaId = obj.MarcaId;
                result.NroChasis = obj.NroChasis;
                result.PatioId = obj.PatioId;
                result.Placa = obj.Placa;
                result.Tipo = obj.Tipo;
                await appDbContext.SaveChangesAsync();
            }
            return result ?? null!;

        }
        catch (Exception)
        {
            return  null!;
        }
    }

   
    public async Task<bool> EliminarVehiculo(int id)
    {
        try
        {
            var result = await appDbContext.Vehiculos
           .FirstOrDefaultAsync(e => e.VehiculoId == id);
            if (result is not null)
            {
                appDbContext.Vehiculos.Remove(result);
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