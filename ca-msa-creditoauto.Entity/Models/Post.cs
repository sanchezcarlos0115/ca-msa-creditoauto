using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace camsacreditoauto.Entity.Models
{
    
    public class Post
    {
        [KeyAttribute]
        public int PosId { get; set; }
        public string? Titulo { get; set; }
        public string? Detalle { get; set; }
    }
}
