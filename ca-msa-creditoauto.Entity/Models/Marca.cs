namespace camsacreditoauto.Entity.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }

        public int MarcaId { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}
