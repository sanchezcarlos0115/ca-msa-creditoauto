using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace camsacreditoauto.Infrastructure.Services;


public class AsignarClienteImplementacion : Repository<ClientePatio>, IAsignarCliente
{
   
    public AsignarClienteImplementacion(CreditoAutoContext context) : base(context)
    {

    }

    public async Task<bool> ExistAsync(int id)
    {
        return await _context.ClientePatios.FirstOrDefaultAsync(c => c.ClientePatioId == id) != null;
    }

   
}
