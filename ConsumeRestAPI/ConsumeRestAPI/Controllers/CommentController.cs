using ConsumeRestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Threading;

namespace ConsumeRestAPI.Controllers
{
    public class CommentController : Controller
    {
        string url = "https://jsonplaceholder.typicode.com/comments";
        private List<Comment> comments = new List<Comment>();
        private HttpClient _client;

        public CommentController(HttpClient client)
        {
            _client = client;
        }

        public async Task<Comment> GetProductsAsync()
        {
            Comment comment = null;

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                comment = await client.GetAsync<Comment>(request);
            }

            return comment!;
        }

        public IActionResult Index()
        {
            return View(this.GetRestAPIData(1));
        }

        [HttpPost]
        public IActionResult Index(int currentPageIndex)
        {
            return View(this.GetRestAPIData(currentPageIndex));
        }

        public CommentModel GetRestAPIData(int currentPage)
        {
            int maxRows = 10;
            CommentModel commentModel = new CommentModel();

            var cmt = GetProductsAsync();

            commentModel.Comments = (from customer in comments 
                                     select customer)
            .OrderBy(c => c.postId)
            .Skip((currentPage - 1) * maxRows)
            .Take(maxRows).ToList();

            double pageCount = (double)((decimal)commentModel.Comments.Count() / Convert.ToDecimal(maxRows));
            commentModel.PageCount = (int)Math.Ceiling(pageCount);

            commentModel.CurrentPageIndex = currentPage;

            return commentModel;
        }

    }
}
