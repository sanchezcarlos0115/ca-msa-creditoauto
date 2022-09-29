using camsacreditoauto.Entity.Models;
using Microsoft.EntityFrameworkCore;



namespace camsacreditoauto.Repository.Context
{
    public partial class BlogContext : DbContext
    {
        public BlogContext()
        {
        }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public virtual DbSet<Post> Post { get; set; } = null!;

       
    }
}
