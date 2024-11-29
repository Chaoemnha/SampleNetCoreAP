using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using BusinessAccess.Service;
using BusinessAccess.DataContract;
using BusinessAccess.Repository;
using AutoMapper;

namespace SampleNetCoreAP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        public List<Blog> Get()
        {
            return _blogService.getAllBlogs();
        }
        [HttpGet("{BlogId}", Name = "Get")]
        public Blog Get(int id)
        {
            return _blogService.getBlogById(id);
        }
        [HttpPost]
        [Produces("application/json")]
        public Blog Post([FromBody] Blog blog)
        {
            return _blogService.addABlog(blog); //Dang lam
        }
        [HttpPut("{BlogId}")]
        public void Put(int BlogId, [FromBody] Blog Blog)
        {
            //
        }
        [HttpDelete("{BlogId}")]
        public void Delete(int id)
        {
        }
        private List<Blog> GetBlogs()
        {
            return new List<Blog>()
            {
                new Blog()
                {
                    Id = 1,
                    Active = true,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    Name = "Test",
                    Posts = new List<Post>()
                },
            };
        }
    }
}
