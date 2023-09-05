namespace ConsumeRestAPI.Models
{
    public class CommentModel
    {
        public List<Comment> Comments{ get; set; }

        public int CurrentPageIndex { get; set; }

        public int PageCount { get; set; }
    }

    public class Comment
    {
        public int postId { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string body { get; set; }
    }
}
