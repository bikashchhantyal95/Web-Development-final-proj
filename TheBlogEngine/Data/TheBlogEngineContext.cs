using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheBlogEngine.Shared;

namespace TheBlogEngine.Data
{
    public class TheBlogEngineContext : DbContext
    {
        public TheBlogEngineContext (DbContextOptions<TheBlogEngineContext> options)
            : base(options)
        {
        }

        public DbSet<TheBlogEngine.Shared.Blog> Blog { get; set; } = default!;
    }
}
