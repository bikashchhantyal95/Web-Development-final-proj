using System;
using System.ComponentModel.DataAnnotations;

namespace TheBlogEngine.Shared
{
	public class Blog
	{
		public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Content { get; set; }

        public DateTime PublishedDate { get; set; }
        
        public string Author { get; set; }
        
        public ICollection<Blog> Comments { get; set; }
    }
}

