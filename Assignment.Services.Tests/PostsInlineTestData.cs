using Assignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Services.Tests
{
    public static class PostsInlineTestData
    {
        private static readonly List<PostsGetAllInlineTestDataInfo> _getPosts = new List<PostsGetAllInlineTestDataInfo>()
        {
            new PostsGetAllInlineTestDataInfo() {
                ExpectedResult = true
            },
        };
        private static readonly List<PostCreateOrUpdateInlineTestDataInfo> _updateOrCreatePostsAndDataValid = new List<PostCreateOrUpdateInlineTestDataInfo>()
        {
                new PostCreateOrUpdateInlineTestDataInfo() {
                       Data = new Post()
                       {
                          Author = "Author Test",
                          Comment = "Comment Test",
                          Image = "Image Test",
                          IsFavorite = true,
                          Title = "Title Test"
                       },
                       ExpectedNotResult = 0
            },
                new PostCreateOrUpdateInlineTestDataInfo() {
                       Data = new Post()
                       {
                          Id = 1,
                          Author = "Author Test",
                          Comment = "Comment Test",
                          Image = "Image Test",
                          IsFavorite = true,
                          Title = "Title Test"
                       },
                       ExpectedNotResult = 0
            },
        };
        //private static readonly List<PostCreateOrUpdateInlineTestDataInfo> _updateIdsExistAndDataInvalid = new List<PostCreateOrUpdateInlineTestDataInfo>()
        //{
        //        new PostCreateOrUpdateInlineTestDataInfo() {
        //               Data = new Post()
        //               {

        //               },
        //               ExpectedResult = false
        //        }

        //};

        public static IEnumerable<object[]> TestData_GetPosts
        {
            get
            {
                foreach (var v in _getPosts)
                    yield return new object[] { v };
            }
        }
        public static IEnumerable<object[]> TestData_Create_Or_Update_DataValid
        {
            get
            {
                foreach (var v in _updateOrCreatePostsAndDataValid)
                    yield return new object[] { v };
            }
        }
        //for invalid test
        //public static IEnumerable<object[]> TestData_Create_Or_Update_DataInvalid
        //{
        //    get
        //    {
        //        foreach (var v in _updateIdsExistAndDataInvalid)
        //            yield return new object[] { v };
        //    }
        //}
    }

    public class PostsGetAllInlineTestDataInfo
    {
        public bool ExpectedResult { get; set; }
    }
    //public class PostByIdGetInlineTestDataInfo
    //{
    //    public int PostId { get; set; }
    //    public bool ExpectedResult { get; set; }
    //}
    public class PostCreateOrUpdateInlineTestDataInfo
    {
        public Post Data { get; set; }
        public int ExpectedNotResult { get; set; }
    }
}
