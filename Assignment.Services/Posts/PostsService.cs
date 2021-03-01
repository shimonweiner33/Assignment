using Assignment.Data.Models;
using Assignment.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Services.Posts
{
    public class PostsService : IPostsService
    {
        private IPostsRepository postRepository;
        public PostsService(IPostsRepository postRepository)
        {
            this.postRepository = postRepository;
        }


        public async Task<PostsList> GetAllPosts()
        {
            var posts = await postRepository.GeAllPosts();

            return posts;
        }
        public async Task<PostsList> GetAllPostsByParams(Post searchParams)
        {
            var posts = await postRepository.GetAllPostsByParams(searchParams);

            return posts;
        }

        public async Task<Post> GetPostById(int postId)
        {
            Post post = await postRepository.GetPostById(postId);

            return post;
        }
        public async Task<bool> CreateOrUpdatePost(Post post)
        {
            var result = await postRepository.CreateOrUpdatePost(post);
            return result;
        }

        public async Task<bool> DeletePost(int postId)
        {
            bool result = await postRepository.DeletePost(postId);
            return result;
        }

    }
}
