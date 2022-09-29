using camsacreditoauto.Entity.Models;

namespace camsacreditoauto.Domain.Interfaces
{
    public interface IPost    
    {
       Task<IEnumerable<Post>> GetPosts();
        
    }
}