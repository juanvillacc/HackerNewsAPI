using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace HackerNews.API.Errors
{
    public class Error
    {
        [Required]
        [DataMember(Name = "httpCode")]
        public int? HttpCode { get; set; }
        [Required]
        [DataMember(Name = "errorCode")]
        public string? ErrorCode { get; set; }

        [Required]
        [DataMember(Name = "message")]
        public string? Message { get; set; }
    }
}
