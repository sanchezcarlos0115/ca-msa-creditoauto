using camsacreditoauto.Domain.Interfaces;
using camsacreditoauto.Entity.Models;
using camsacreditoauto.Repository.Context;

using Microsoft.EntityFrameworkCore;

namespace camsacreditoauto.Infrastructure.Services
{
    public class PostImplementacion : IPost
    {
        private readonly BlogContext _context;

        public PostImplementacion(BlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _context.Post.ToListAsync();
        }
    }
}