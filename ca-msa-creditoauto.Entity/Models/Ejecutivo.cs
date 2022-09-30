using System;
using System.Collections.Generic;

namespace camsacreditoauto.Entity.Models
{
    public partial class Ejecutivo
    {
        public Ejecutivo()
        {
            Solicituds = new HashSet<Solicitud>();
        }

        public int EjecutivoId { get; set; }
        public int PersonaId { get; set; }
        public int PatioId { get; set; }
        public string Celular { get; set; } = null!;

        public virtual PatioAuto Patio { get; set; } = null!;
        public virtual Persona Persona { get; set; } = null!;
        public virtual ICollection<Solicitud> Solicituds { get; set; }
    }
}
