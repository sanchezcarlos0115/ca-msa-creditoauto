using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace camsacreditoauto.Entity.Dto;

public class PatioAutoType
{
   
    public int PatioId { get; set; }

    [Required]
    public string Nombre { get; set; } = null!;

    [Required]
    public string Direccion { get; set; } = null!;

    [Required]
    public string Telefono { get; set; } = null!;

    [Required]
    public int NumeroPtoVenta { get; set; }
}
