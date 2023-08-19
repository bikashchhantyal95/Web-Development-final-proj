using Microsoft.EntityFrameworkCore;
using TheBlogEngine.Shared;

namespace TheBlogEngine.API.Data;

public class BlogDbContext: DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }

    public DbSet<Blog?> BlogList => Set<Blog>();

    public DbSet<TheBlogEngine.Shared.Comment>? Comment { get; set; }
}