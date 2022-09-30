namespace camsacreditoauto.Entity.Models
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            Solicituds = new HashSet<Solicitud>();
        }

        public int VehiculoId { get; set; }
        public string Placa { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public string NroChasis { get; set; } = null!;
        public int MarcaId { get; set; }
        public int PatioId { get; set; }
        public string? Tipo { get; set; }
        public decimal Cilindraje { get; set; }
        public bool Avaluo { get; set; }

        public virtual Marca Marca { get; set; } = null!;
        public virtual PatioAuto Patio { get; set; } = null!;
        public virtual ICollection<Solicitud> Solicituds { get; set; }
    }
}
