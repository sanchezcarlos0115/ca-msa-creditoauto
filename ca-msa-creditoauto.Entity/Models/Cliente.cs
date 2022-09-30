using System;
using System.Collections.Generic;

namespace camsacreditoauto.Entity.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            ClientePatios = new HashSet<ClientePatio>();
        }

        public int ClienteId { get; set; }
        public int PersonaId { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string EstadoCivil { get; set; } = null!;
        public string? IdentificacionConyuge { get; set; }
        public string? NombresConyuge { get; set; }
        public bool SujetoCredito { get; set; }

        public virtual Persona Persona { get; set; } = null!;
        public virtual ICollection<ClientePatio> ClientePatios { get; set; }
    }
}
