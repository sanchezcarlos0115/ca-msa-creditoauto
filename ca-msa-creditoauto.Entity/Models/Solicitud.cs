namespace camsacreditoauto.Entity.Models
{
    public partial class Solicitud
    {
        public int SolicitudId { get; set; }
        public DateTime FechaElaboracion { get; set; }
        public int ClientePatioId { get; set; }
        public int VehiculoId { get; set; }
        public int MesesPlazo { get; set; }
        public decimal Cuotas { get; set; }
        public decimal Entrada { get; set; }
        public int EjecutivoId { get; set; }
        public string? Observación { get; set; }
        public int EstadoId { get; set; }

        public virtual ClientePatio ClientePatio { get; set; } = null!;
        public virtual Ejecutivo Ejecutivo { get; set; } = null!;
        public virtual Estado Estado { get; set; } = null!;
        public virtual Vehiculo Vehiculo { get; set; } = null!;
    }
}
