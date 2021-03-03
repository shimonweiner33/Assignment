using Assignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Repository.Interface
{
    public interface IPostsRepository
    {
        Task<Post> GetPostById(int postId);
        Task<PostsList> GeAllPosts();
        Task<PostsList> GetAllPostsByParams(Post post);
        Task<int> CreateOrUpdatePost(Post post);
        Task<bool> DeletePost(int postId);
    }
}
