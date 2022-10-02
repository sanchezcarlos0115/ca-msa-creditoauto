using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace camsacreditoauto.Entity.Dto;

public class ClientePatioDto
{
    public int ClientePatioId { get; set; }
    public int ClienteId { get; set; }
    public int PatioId { get; set; }
    public DateTime FechaAsignacion { get; set; }
}
