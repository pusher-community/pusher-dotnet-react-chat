using System.ComponentModel.DataAnnotations;

namespace PusherRealtimeChat.WebAPI.Models
{
    public class ChatMessage
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public string AuthorTwitterHandle { get; set; }
    }
}