namespace camsacreditoauto.Entity.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Clientes = new HashSet<Cliente>();
            Ejecutivos = new HashSet<Ejecutivo>();
        }

        public int PersonaId { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public int Edad { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Ejecutivo> Ejecutivos { get; set; }
    }
}
