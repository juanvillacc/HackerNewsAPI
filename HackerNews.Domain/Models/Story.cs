using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain.Models
{
    public class Story
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Link { get; set; }
    }
}
