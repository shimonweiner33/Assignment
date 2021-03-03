using Assignment.Data.Models;
using Assignment.Hubs;
using Assignment.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    //    [ApiController, Route("api/[controller]")]

    [ApiController]
    [Route("[controller]")]
    public class AssignmentController : ControllerBase
    {
        private IPostsService _postsService;
        private readonly ILogger<AssignmentController> _logger;
        private readonly IHubContext<MessageHub> _messageHubContex;

        public AssignmentController(IPostsService postsService, IHubContext<MessageHub> messageHubContex)
        {
            this._postsService = postsService;
            this._messageHubContex = messageHubContex;
        }

        //[HttpGet, Route("GetPostById")]
        //public Task<Post> GetPostById(int postId)
        //{
        //    return postsService.GetPostById(postId);
        //}

        [HttpGet, Route("GeAllPosts")]
        public Task<PostsList> GeAllPosts()
        {
            return _postsService.GetAllPosts();
        }
        [HttpGet, Route("GetAllPostsByParams")]
        public Task<PostsList> GetAllPostsByParams(Post searchParams)
        {
            return _postsService.GetAllPostsByParams(searchParams);
        }

        [HttpPost, Route("CreateOrUpdatePost")]
        public async Task<int> CreateOrUpdatePost(Post post)
        {
            int InsertedId =  await _postsService.CreateOrUpdatePost(post);
            post.Id = InsertedId;
            //broadcast the message to the clients
            _messageHubContex.Clients.All.SendAsync("UpdatePost", post);

            return InsertedId;
        }

        [HttpPost, Route("DeletePost")]
        public async Task<bool> DeletePost([FromBody] int postId)
        {
            var result = await _postsService.DeletePost(postId);
            return result;
        }

        [HttpGet, Route("SendMessage")]
        public async Task<bool> SendMessage(string message)
        {
            //broadcast the message to the clients
            _messageHubContex.Clients.All.SendAsync("Send", message);
            return true;
        }
    }
}
