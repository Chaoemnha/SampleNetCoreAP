using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using BusinessAccess.Service;
using BusinessAccess.DataContract;
using BusinessAccess.Repository;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleNetCoreAP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly Service<Post> _postService;
        public PostsController(Service<Post> postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public List<Post> GetAll()
        {
            return _postService.GetAll();
        }
        [HttpGet("{id}")]
        public Post GetById(int id)
        {
            return _postService.GetById(id);
        }
        [HttpPost()]
        [Produces("application/json")]
        public void Add(string title, string content, int blogId)
        {
            Post post = new Post(title, content, blogId);
            _postService.Add(post); //Dang lam
        }
        [HttpPut("{id}")]
        public Post Update(int id, string title, string content, int blogId, bool active)
        {
            Post post = _postService.GetById(id);
            post.Title = title;
            post.Content = content;
            post.BlogId = blogId;
            post.Active = active;
            return _postService.Update(post);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _postService.DeleteById(id);
        }
    }
}
