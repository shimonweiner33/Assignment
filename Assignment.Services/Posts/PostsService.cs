using Assignment.Data.Models;
using Assignment.Data.Repository.Interface;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Services.Posts
{
    public class PostsService : IPostsService
    {
        private readonly Serilog.ILogger _logger;
        private readonly IPostsRepository postRepository;
        public PostsService(IPostsRepository postRepository)
        {
            _logger = Log.ForContext<PostsService>();

            this.postRepository = postRepository;
        }


        /// <summary>
        /// Gets all Posts from table dbo.Posts.
        /// </summary>
        /// <param name="roomNum">current room to get the post to</param>
        /// <returns><see cref="PostsList"/> in case of success. otherwise an <see cref="Exception"/> is thrown.</returns>
        public async Task<PostsList> GetAllPosts(int roomNum)
        {
            PostsList result = new PostsList();
            try
            {
                result = await postRepository.GetAllPosts(roomNum);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"GetAllPosts('{roomNum}')  failed");
                throw;
            }
            _logger.Debug($"GetAllPosts('{roomNum}')  result={result}");
            return result;
        }

        /// <summary>
        /// Gets all Posts By Params from table dbo.Posts.
        /// </summary>
        /// <param name="searchParams">Parameters to filter by</param>
        /// <returns><see cref="PostsList"/> in case of success. otherwise an <see cref="Exception"/> is thrown.</returns>
        public async Task<PostsList> GetAllPostsByParams(Post searchParams)
        {
            PostsList result = new PostsList();
            try
            {
                result = await postRepository.GetAllPostsByParams(searchParams);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"GetAllPostsByParams('{searchParams}')  failed");
                throw;
            }
            _logger.Debug($"GetAllPostsByParams('{searchParams}')  result={result}");
            return result;
        }

        /// <summary>
        /// Gets all Posts By Id.
        /// </summary>
        /// <param name="postId">Post id to filter by</param>
        /// <returns><see cref="Post"/> in case of success. otherwise an <see cref="Exception"/> is thrown.</returns>
        public async Task<Post> GetPostById(int postId)
        {
            Post result = null;
            try
            {
                result = await postRepository.GetPostById(postId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"GetPostById('{result}')  failed");
                throw;
            }
            _logger.Debug($"GetPostById('{postId}')  result={result}");
            return result;
        }

        /// <summary>
        /// Create or update post in dbo.Posts - table.
        /// </summary>
        /// <param name="post">Post to update or create in dbo.Posts - table</param>
        /// <returns><see cref="int"/> inserted or update post id - in case of success. otherwise an <see cref="Exception"/> is thrown.</returns>
        public async Task<int> CreateOrUpdatePost(Post post)
        {
            int result = 0;
            try
            {
                result = await postRepository.CreateOrUpdatePost(post);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"CreateOrUpdatePost('{post}')  failed");
                throw;
            }
            _logger.Debug($"CreateOrUpdatePost('{post}')  result={result}");
            return result;
        }

        public async Task<bool> DeletePost(int postId)
        {
            bool result = false;
            try
            {
                result = await postRepository.DeletePost(postId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"DeletePost('{postId}')  failed");
                throw;
            }
            _logger.Debug($"DeletePost('{postId}')  result={result}");
            return result;
        }
    }
}
