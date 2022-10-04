using System.ComponentModel.DataAnnotations;

namespace camsacreditoauto.Entity.Dto;

public class SolicitudType
{
    public int SolicitudId { get; set; }
    public DateTime FechaElaboracion { get; set; }
    [Required]
    public int ClienteId { get; set; }
    [Required]
    public int PatioId { get; set; }
    [Required]
    public int VehiculoId { get; set; }
    [Required]
    public int MesesPlazo { get; set; }
    [Required]
    public decimal Cuotas { get; set; }
    [Required]
    public decimal Entrada { get; set; }
    [Required]
    public int EjecutivoId { get; set; }
    public string? Observación { get; set; }
    [Required]
    public int EstadoId { get; set; }
}
