
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBlogEngine.API.Data;
using TheBlogEngine.Shared;

namespace TheBlogEngine.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;

        public BlogPostController(IBlogRepository blogRepository)
        {
            this._blogRepository = blogRepository;
        }

        // GET: api/BlogPost
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogList()
        {
            try
            {
                var result = await _blogRepository.GetBlogs();
                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(); //Return NotFoundResult if no blogs are found
                }
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    
    // GET: api/BlogPost/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Blog>> GetBlog(int id)
    {
      // if (_context.BlogList == null)
      // {
      //     return NotFound();
      // }
      // var blog = await _context.BlogList.FindAsync(id);
      //
      // if (blog == null)
      // {
      //     return NotFound();
      // }
      //
      // return blog;
      try
      {
          var result = await _blogRepository.GetBlogById(id);
          if (result == null) return NotFound();
          return Ok(result);
      }
      catch(Exception)
      {
          return StatusCode(StatusCodes.Status500InternalServerError,
              "Error retrieving data from database.");
      }
      
    }
    
    // PUT: api/BlogPost/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Blog>> PutBlog(int id, Blog blog)
    {
        try
        {
            if (id != blog.Id)
            {
                return BadRequest("Blog ID mismatch.");
            }

            var blogExists = await _blogRepository.BlogExists(id);
            if (!blogExists)
            {
                return NotFound($"Blog with Id {id} not found");
            }
            return await _blogRepository.UpdateBlog(blog);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
        }
    }
    
    // POST: api/BlogPost
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Blog>> PostBlog(Blog blog)
    {
        try
        {
            if (blog == null)
            {
                return Problem("Entity set 'BlogDbContext.BlogList'  is null.");
            }

            var createdBlog = await _blogRepository.AddBlog(blog);
    
            return CreatedAtAction("GetBlog", new { id = blog.Id }, blog);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating  new employee.");
        }
    }
    
    // DELETE: api/BlogPost/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteBlog(int id)
    {
        try
        {
            var blogExists = await _blogRepository.BlogExists(id);
            if (!blogExists)
                return NotFound($"Blog with Id {id} not found");
            await _blogRepository.DeleteBlog(id);
            return NoContent();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting blog");
        }
    }
    
    
    }
}
