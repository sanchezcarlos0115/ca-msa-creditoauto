using System.ComponentModel.DataAnnotations;

namespace camsacreditoauto.Entity.Dto;

public class VehiculoType
{
    public int VehiculoId { get; set; }
    [Required]
    public string Placa { get; set; } = null!;
    [Required]
    public string Modelo { get; set; } = null!;
    [Required]
    public string NroChasis { get; set; } = null!;
    [Required]
    public int MarcaId { get; set; }
    [Required]
    public int PatioId { get; set; }
    public string? Tipo { get; set; }
    [Required]
    public decimal Cilindraje { get; set; }
    [Required]
    public bool Avaluo { get; set; }
}
