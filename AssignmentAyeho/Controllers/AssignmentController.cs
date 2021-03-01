using Assignment.Data.Models;
using Assignment.Services.Posts;
using Microsoft.AspNetCore.Mvc;
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
        private IPostsService postsService;
        private readonly ILogger<AssignmentController> _logger;

        public AssignmentController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        //[HttpGet, Route("GetPostById")]
        //public Task<Post> GetPostById(int postId)
        //{
        //    return postsService.GetPostById(postId);
        //}

        [HttpGet, Route("GeAllPosts")]
        public Task<PostsList> GeAllPosts()
        {
            return postsService.GetAllPosts();
        }
        [HttpGet, Route("GetAllPostsByParams")]
        public Task<PostsList> GetAllPostsByParams(Post searchParams)
        {
            return postsService.GetAllPostsByParams(searchParams);
        }

        [HttpPost, Route("CreateOrUpdatePost")]
        public async Task<bool> CreateOrUpdatePost(Post post)
        {
            var result = await postsService.CreateOrUpdatePost(post);

            return result;
        }

        [HttpPost, Route("DeletePost")]
        public async Task<bool> DeletePost([FromBody] int postId)
        {
            var result = await postsService.DeletePost(postId);
            return result;
        }
    }
}
