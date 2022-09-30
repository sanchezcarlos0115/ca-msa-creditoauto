using System;
using System.Collections.Generic;

namespace camsacreditoauto.Entity.Models
{
    public partial class ClientePatio
    {
        public ClientePatio()
        {
            Solicituds = new HashSet<Solicitud>();
        }

        public int ClientePatioId { get; set; }
        public int ClienteId { get; set; }
        public int PatioId { get; set; }
        public DateTime FechaAsignacion { get; set; }

        public virtual Cliente Cliente { get; set; } = null!;
        public virtual PatioAuto Patio { get; set; } = null!;
        public virtual ICollection<Solicitud> Solicituds { get; set; }
    }
}
