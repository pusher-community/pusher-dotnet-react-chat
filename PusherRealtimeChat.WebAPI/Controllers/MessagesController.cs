using PusherRealtimeChat.WebAPI.Models;
using PusherServer;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PusherRealtimeChat.WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class MessagesController : ApiController
    {
        private static List<ChatMessage> messages =
            new List<ChatMessage>()
            {
                new ChatMessage
                {
                    AuthorTwitterHandle = "Pusher",
                    Text = "Hi there! 😘"
                },
                new ChatMessage
                {
                    AuthorTwitterHandle = "Pusher",
                    Text = "Welcome to your chat app"
                }
            };

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, messages);
        }

        public HttpResponseMessage Post(ChatMessage message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, 
                    "Invalid input");
            }
            messages.Add(message);
            var pusher = new Pusher(
                "YOUR PUSHER APP IDj",
                "YOUR PUSHER APP KEY",
                "YOUR PUSHER APP SECRET");
            pusher.Trigger(
                channelName: "messages", 
                eventName: "new_message", 
                data: new
            {
                AuthorTwitterHandle = message.AuthorTwitterHandle,
                Text = message.Text
            });
            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}