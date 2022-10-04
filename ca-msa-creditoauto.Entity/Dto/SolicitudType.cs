using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace camsacreditoauto.Entity.Dto;

public class SolicitudType
{
    public int SolicitudId { get; set; }
    public DateTime FechaElaboracion { get; set; }
    public int ClienteId { get; set; }
    public int PatioId { get; set; }
    public int VehiculoId { get; set; }
    public int MesesPlazo { get; set; }
    public decimal Cuotas { get; set; }
    public decimal Entrada { get; set; }
    public int EjecutivoId { get; set; }
    public string? Observación { get; set; }
    public int EstadoId { get; set; }
}
