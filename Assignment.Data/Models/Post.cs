using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Data.Models
{
    public class Post : ModelBase
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public int RoomNum { get; set; }
        public string Title  { get; set; }
        public string Author { get; set; }
        public string Comment { get; set; }
        public string Image { get; set; }
        public bool IsFavorite { get; set; }
    }
    public class PostsList
    {
        public List<Post> Posts { get; set; }
    }
}
