using TheBlogEngine.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TheBlogEngine.API.Data;

public interface IBlogRepository
{
    Task<IEnumerable<Blog?>> GetBlogs();
    Task<Blog?> GetBlogById(int id);
    Task<Blog?> AddBlog(Blog blog);
    Task<Blog> UpdateBlog(Blog blog);
    Task DeleteBlog(int id);
    Task<bool> BlogExists(int id);
}
    
public class BlogRepository: IBlogRepository
{
    private readonly BlogDbContext _blogDbContext;

    public BlogRepository(BlogDbContext blogDbContext)
    {
        this._blogDbContext = blogDbContext;
    }
    
    public async Task<IEnumerable<Blog?>> GetBlogs()
    {
        return await _blogDbContext.BlogList.ToListAsync();
    }

    public async Task<Blog?> GetBlogById(int id)
    {
        return await _blogDbContext.BlogList.FirstOrDefaultAsync(b => b != null && b.Id == id);
    }

    public async Task<Blog?> AddBlog(Blog blog)
    {
        var result = await _blogDbContext.BlogList.AddAsync(blog);
        await _blogDbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Blog> UpdateBlog(Blog blog)
    {
        var result = await _blogDbContext.BlogList.FirstOrDefaultAsync(b => b.Id == blog.Id);

        if (result != null)
        {
            result.Title = blog.Title;
            result.Content = blog.Content;

            await _blogDbContext.SaveChangesAsync();
            return result;
        }

        return null;
    }

    public async Task DeleteBlog(int id)
    {
        var result = await _blogDbContext.BlogList.FirstOrDefaultAsync(b => b.Id == id);
        if (result != null)
        {
            _blogDbContext.BlogList.Remove(result);
            await _blogDbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> BlogExists(int id)
    {
        return await _blogDbContext.BlogList.AnyAsync(b => b.Id == id);
    }
}