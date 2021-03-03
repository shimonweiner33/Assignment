using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Assignment.Data.Models;
using Assignment.Data.Repository.Interface;
using System.Linq;

namespace Assignment.Data.Repository
{
    public class PostsRepository : BaseRepository, IPostsRepository
    {
        public PostsRepository(IConfiguration configuration) : base(configuration)
        {

        }


        public async Task<Post> GetPostById(int postId)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var sQuery = @"SELECT Id, Title, Author, Comment, Image, IsFavorite FROM Posts WHERE Id = @ID";

                    conn.Open();
                    var result = (await conn.QueryFirstOrDefaultAsync<Post>(sQuery, new
                    {
                        ID = postId
                    }));
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<PostsList> GeAllPosts()
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var sQuery = @"SELECT Id, Title, Author, Comment, Image, IsFavorite FROM Posts";

                    conn.Open();
                    var result = (await conn.QueryAsync<Post>(sQuery)).ToList();
                    return new PostsList() { Posts = result };
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<PostsList> GetAllPostsByParams(Post searchParams)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    var sQuery = @"SELECT Id, Title, Author, Comment, Image, IsFavorite FROM Posts WHERE 1=1";
                    sQuery = BulidQuery(searchParams, sQuery);

                    conn.Open();
                    var result = (await conn.QueryAsync<Post>(sQuery, new
                    {
                        id = searchParams.Id,
                        author = searchParams.Author,
                        comment = searchParams.Comment,
                        image = searchParams.Image,
                        title = searchParams.Title,
                    })).ToList();
                    return new PostsList() { Posts = result };
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private string BulidQuery(Post filter, string sQuery)
        {
            if (filter.Id.HasValue)
            {
                sQuery += " AND Id = @id";
            }
            if (!string.IsNullOrEmpty(filter.Author))
            {
                sQuery += " AND Author = @author";
            }
            if (!string.IsNullOrEmpty(filter.Title))
            {
                sQuery += " AND Title = @title";
            }
            if (!string.IsNullOrEmpty(filter.Image))
            {
                sQuery += " AND Image = @image";
            }
            if (!string.IsNullOrEmpty(filter.Comment))
            {
                sQuery += " AND Comment = @comment";
            }
            if (filter.IsFavorite)
            {
                sQuery += " AND IsFavorite = @isFavorite";
            }
            return sQuery;
        }

        public async Task<int> CreateOrUpdatePost(Post post)
        {
            try
            {

                var sQuery = $@"DECLARE @InsertedId int = @postId;
                                IF EXISTS (SELECT * FROM Posts
                                WHERE Id = @postId)

                                    BEGIN
                                       UPDATE Posts
                                       SET Title = @title, 
                                           Author = @author, 
                                           Comment = @comment,
                                           Image = @image,
                                           IsFavorite = @isFavorite,
                                           UpdatedOn = @now, 
                                           UpdatedBy = @user
                                       WHERE Id = @postId
                                    END
                                ELSE
                                    BEGIN
                                        INSERT INTO Posts(Title, Author, Comment, Image, IsFavorite, UpdatedOn, UpdatedBy, CreatedOn, CreatedBy)
                                        VALUES(@title, @author, @comment, @image, @isFavorite, @now, @user, @now, @user);
                                        SELECT @InsertedId = SCOPE_IDENTITY()
                                    END
                                SELECT @InsertedId;";

                using (IDbConnection conn = Connection)
                {
                    var affectedRowId = await conn.ExecuteScalarAsync(sQuery,
                                    new
                                    {
                                        postId = post.Id,
                                        title = post.Title,
                                        author = post.Author,
                                        comment = post.Comment,
                                        image = post.Image,
                                        isFavorite = post.IsFavorite,
                                        now = DateTime.Now,
                                        user = "user"
                                    });
                    int insertedId = (int)(affectedRowId);
                    return insertedId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeletePost(int postId)
        {
            try
            {
                var sQuery = $@"if EXISTS (SELECT * FROM Posts
                                WHERE Id = @postId)
                                    begin
                                       DELETE FROM Posts WHERE Id = @postId
                                    end";

                using (IDbConnection conn = Connection)
                {
                    var affectedRows = await conn.ExecuteAsync(sQuery,
                                    new
                                    {
                                        postId = postId
                                    });
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
