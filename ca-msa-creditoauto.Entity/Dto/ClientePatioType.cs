using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace camsacreditoauto.Entity.Dto;

public class ClientePatioType
{
   
    public int ClientePatioId { get; set; }

    [Required]
    public int ClienteId { get; set; }

    [Required]
    public int PatioId { get; set; }

    [JsonIgnore]
    public DateTime FechaAsignacion { get; set; }

}
