using camsacreditoauto.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace camsacreditoauto.Domain.Interfaces;

public interface IAsignarClienteInfraestructure : IRepository<ClientePatio>
{
    Task<bool> ExistAsync(int id);
}