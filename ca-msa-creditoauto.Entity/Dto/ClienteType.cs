using System.ComponentModel.DataAnnotations;

namespace camsacreditoauto.Entity.Dto;

public class ClienteType
{
    public int ClienteId { get; set; }

    [Required]
    public string Identificacion { get; set; } = null!;

    [Required]
    public string Nombres { get; set; } = string.Empty;

    [Required]
    public string Apellidos { get; set; } = string.Empty;

    [Required]
    public int Edad { get; set; }

    [Required]
    public string Direccion { get; set; } = null!;

    [Required]
    public string Telefono { get; set; } = null!;

    [Required]
    public DateTime FechaNacimiento { get; set; }

    [Required]
    public string EstadoCivil { get; set; } = null!;

    [Required]
    public string? IdentificacionConyuge { get; set; }

    [Required]
    public string? NombresConyuge { get; set; }

    [Required]
    public bool SujetoCredito { get; set; }
}
