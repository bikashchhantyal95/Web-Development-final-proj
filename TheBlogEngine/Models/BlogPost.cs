using System;
using System.ComponentModel.DataAnnotations;

namespace TheBlogEngine.Models
{
	public class BlogPost
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Content { get; set; }

		public DateTime PublishedDate { get; set; }

		[Required]
        public string Author { get; set; }
	}
}

