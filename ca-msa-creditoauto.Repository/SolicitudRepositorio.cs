using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace camsacreditoauto.Repository;

public class SolicitudRepositorio : ISolicitudRepositorio
{
    private readonly CreditoAutoContext appDbContext;

    public SolicitudRepositorio(CreditoAutoContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<bool> ValidarClienteSujetoCredito(int clienteId)
    {
        
        return await appDbContext.Clientes.AnyAsync(z => z.ClienteId == clienteId && z.SujetoCredito == true);

    }

    public async Task<bool> ExisteSolicitudClienteMismoDiaActiva(int clienteId)
    {
        //EstadoId 1 - Registrada / 2 - Despachada / 3 - Cancelada
        var objClientePatio = appDbContext.ClientePatios.FirstOrDefault(x => x.ClienteId == clienteId);
        if (objClientePatio is not null)
        {
            return await appDbContext.Solicituds.AnyAsync(z => z.ClientePatioId == objClientePatio.ClientePatioId 
            && z.EstadoId == 1 && z.FechaElaboracion.Date == DateTime.Now.Date);
        }
        return false;
    }

    public async Task<bool> ValidarSolictudActivaVehiculo(int vehiculoId)
    {
        //EstadoId 1 - Registrada / 2 - Despachada / 3 - Cancelada
        return await appDbContext.Solicituds.AnyAsync(z => z.VehiculoId == vehiculoId
        && z.EstadoId == 1);

    }

    public async Task<int> GenerarSolicitudCredito(Solicitud obj)
    {
        try
        {
            var result = await appDbContext.Solicituds.AddAsync(obj);
            await appDbContext.SaveChangesAsync();
            return result.Entity.SolicitudId;
        }
        catch (Exception)
        {
            return -1;
        }
    }

}
