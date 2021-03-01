using Assignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Services.Posts
{
    public interface IPostsService
    {
        Task<Post> GetPostById(int postId);
        Task<PostsList> GetAllPosts();
        Task<PostsList> GetAllPostsByParams(Post post);
        Task<bool> CreateOrUpdatePost(Post post);
        Task<bool> DeletePost(int postId);
    }
}
