using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebUiForStackOverflowPost.Models;

namespace WebUiForStackOverflow.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<StackOverflowPost> posts = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50275/");
                //HTTP GET
                var responseTask = client.GetAsync("api/values");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<StackOverflowPost>>();
                    readTask.Wait();

                    posts = readTask.Result;
                }
                else
                {

                    posts = Enumerable.Empty<StackOverflowPost>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(posts);
        }
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(StackOverflowPost post)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50275/api/values/");

                //HTTP POST

                var postTask = client.PostAsJsonAsync(post.title + "/" + post.description, post);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(post);

        }


        [HttpPost]
        public ActionResult Upvote(string title)
        {
            StackOverflowPost post = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50275/api/values/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync(title + "/increase", post);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(post);
        }

        [HttpPost]
        public ActionResult Downvote(string title)
        {
            StackOverflowPost post = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50275/api/values/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync(title + "/decrease", post);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(post);
        }


    }


}



