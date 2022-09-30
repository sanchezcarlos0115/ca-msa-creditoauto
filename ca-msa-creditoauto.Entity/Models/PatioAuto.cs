namespace camsacreditoauto.Entity.Models
{
    public partial class PatioAuto
    {
        public PatioAuto()
        {
            ClientePatios = new HashSet<ClientePatio>();
            Ejecutivos = new HashSet<Ejecutivo>();
            Vehiculos = new HashSet<Vehiculo>();
        }

        public int PatioId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public int NumeroPtoVenta { get; set; }

        public virtual ICollection<ClientePatio> ClientePatios { get; set; }
        public virtual ICollection<Ejecutivo> Ejecutivos { get; set; }
        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}
