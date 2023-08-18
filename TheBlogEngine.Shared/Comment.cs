namespace TheBlogEngine.Shared;

public class Comment
{
    public Guid CommentId { get; set; }
    
    public string? Content { get; set; }
    public string? CommentBy { get; set; }
    public DateTime CommentDate { get; set; }
    
    // Foreign Key
    public int BlogPostId { get; set; }
    public Blog Blog { get; set; }
}