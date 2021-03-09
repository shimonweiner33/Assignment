using Assignment.Data.Models;
using Assignment.Data.Repository;
using Assignment.Services.Posts;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Configuration;
namespace Assignment.Services.Tests
{


    /// <summary>
    /// This unit testing class verify the correct functionality of the "PostsService".
    /// </summary>
    public class PostsServiceShould
    {
        private readonly IPostsService _postsService;
        private readonly IConfiguration _configuration;
        public PostsServiceShould()
        {
            _postsService = new PostsService(new PostsRepository(_configuration));
        }

        /// <summary>
        /// Tests if the Posts list were returned from Posts table by order.
        /// The expected result is to get specific Post by id.
        /// </summary>
        [Theory]
        [MemberData(nameof(PostsInlineTestData.TestData_GetPosts), MemberType = typeof(PostsInlineTestData))]
        public async void GetPostByIdAsyncAsync_AllItemsReturned(PostsGetAllInlineTestDataInfo testDataInfo)
        {
            // Arrange
            PostsList resultData;
            bool actualResult;
            int mainRoom = 1;
            // Act
            try
            {
                resultData = await _postsService.GetAllPosts(mainRoom);
                actualResult = resultData.Posts != null && resultData.Posts.Count > 0;
            }
            catch
            {
                actualResult = false;
            }

            // Assert
            Assert.Equal(testDataInfo.ExpectedResult, actualResult);
        }

        /// <summary>
        /// Tests if post will updated or create when the data valid.
        /// The result is integer number. when actualResult > expectedNotResult - the post would be fully created or updated.  in Posts - table.
        /// </summary>
        [Theory]
        [MemberData(nameof(PostsInlineTestData.TestData_Create_Or_Update_DataValid), MemberType = typeof(PostsInlineTestData))]
        public async void CreateOrUpdatePortionsAsync_CreateOrUpdatePortions(PostCreateOrUpdateInlineTestDataInfo testDataInfo)
        {
            // Arrange

            int actualResult = 0;

            // Act
            try
            {
                actualResult = await _postsService.CreateOrUpdatePost(testDataInfo.Data);
            }
            catch
            {
                actualResult = 0;
            }
            finally
            {
                // need to Rollback;
            }
            Assert.True(testDataInfo.ExpectedNotResult < actualResult);
        }

    }

}
