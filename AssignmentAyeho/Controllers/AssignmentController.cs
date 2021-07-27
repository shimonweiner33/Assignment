using Assignment.Data.Models;
using Assignment.Services.Hubs;
using Assignment.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Assignment.Services.Rooms;
using Assignment.Common.Enums;
namespace Assignment.Controllers
{
    /// <summary>
    /// AssignmentController provides operations with PostsService that works with - dbo.Posts table.
    /// and with RoomsService that works with - dbo.Rooms and dbo.Rooms_UserConnectinons tables.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AssignmentController : ControllerBase
    {
        private readonly IPostsService _postsService;
        private readonly Serilog.ILogger _logger;
        private readonly IRoomsService _roomsService;

        private readonly IHubContext<MessageHub> _messageHubContex;

        public AssignmentController(IPostsService postsService, IHubContext<MessageHub> messageHubContex, IRoomsService roomsService)
        {
            this._postsService = postsService;
            this._messageHubContex = messageHubContex;
            this._roomsService = roomsService;
            _logger = Log.ForContext<AssignmentController>();
        }




        /// <summary>
        /// Gets all posts from Posts table by room number.
        /// user not must login
        /// </summary>
        /// <param name="roomNum">current room to get the post to</param>
        /// <returns>Result - the model asked as PostsList.</returns>
        [HttpGet, Route("GetAllPosts")]
        public async Task<PostsList> GetAllPosts(int roomNum)
        {
            PostsList result = null;

            try
            {
                result =  await _postsService.GetAllPosts(roomNum);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"GetAllPosts ('{roomNum}') failed  => exception:{ex.Message}");
                return null;
            }
            return result;
        }
        [HttpGet, Route("GetAllPostsByParams")]
        public Task<PostsList> GetAllPostsByParams(Post searchParams)
        {

            try
            {
                return _postsService.GetAllPostsByParams(searchParams);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"GetAllPostsByParams ('{searchParams}') failed  => exception:{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates or create a specific Post.
        /// user must login.
        /// </summary>
        /// <param name="post"></param>
        /// <returns>A Inserted postId as int. if update the InsertedId > 0 .</returns>
        [Authorize]
        [HttpPost, Route("CreateOrUpdatePost")]
        public async Task<int> CreateOrUpdatePost(Post post)
        {
            int InsertedId = 0;
            try
            {
                post.UserName = User.Identity.Name;
                InsertedId = await _postsService.CreateOrUpdatePost(post);
                post.Id = InsertedId;
                post.UpdatedBy = HttpContext.User.Identity.Name;

                if (post.RoomNum != (int)(RoomType.MainRoom))
                {
                    var room = await _roomsService.GetRoom(post.RoomNum);
                    foreach (var UserConnectinon in room.UserConnectinons)
                    {
                        await _messageHubContex.Groups.AddToGroupAsync(UserConnectinon.UserConnectinonId, room.RoomName);
                    }
                    await _messageHubContex.Clients.Group(room.RoomName).SendAsync("CreateOrUpdatePost", post);
                }
                if (post.RoomNum == (int)(RoomType.MainRoom))
                {
                    await _messageHubContex.Clients.All.SendAsync("CreateOrUpdatePost", post);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"CreateOrUpdatePost ('{post}') failed  => exception:{ex.Message}");
                throw;
            }
            return InsertedId;
        }

        /// <summary>
        /// DeleteRuleById method Delete AddSubtractStaffRule by id and his items.
        /// user must login.
        /// </summary>
        /// <remarks> See project's JSON folder for example of JSON incoming and result.</remarks>
        ///<returns>Returns <see cref="bool"/> : true if success.</returns>
        [Authorize]
        [HttpPost, Route("DeletePost")]
        public async Task<bool> DeletePost([FromBody] int postId)
        {
            bool result = false;
            try
            {
                result = await _postsService.DeletePost(postId);
                if (result)
                {
                    await _messageHubContex.Clients.All.SendAsync("DeletePost", postId);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"DeletePost ('{postId}') failed  => exception:{ex.Message}");
                throw;
            }
            return result;
        }

        //[HttpGet, Route("SendMessage")]
        //public async Task<bool> SendMessage(string message)
        //{
        //    //broadcast the message to the clients
        //    await _messageHubContex.Clients.All.SendAsync("Send", message);
        //    return true;
        //}

        //[HttpGet, Route("GetPostById")]
        //public Task<Post> GetPostById(int postId)
        //{
        //    return postsService.GetPostById(postId);
        //}
    }
}
