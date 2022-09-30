using System;
using System.Collections.Generic;

namespace camsacreditoauto.Entity.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Solicituds = new HashSet<Solicitud>();
        }

        public int EstadoId { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Solicitud> Solicituds { get; set; }
    }
}
