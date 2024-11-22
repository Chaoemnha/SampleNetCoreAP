using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;

namespace SampleNetCoreAP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Blog> Get()
        {
            return GetBlogs();
        }
        [HttpGet("{BlogId}", Name = "Get")]
        public Blog Get(int id)
        {
            return GetBlogs().Find(e => e.Id == id);
        }
        [HttpPost]
        [Produces("application/json")]
        public Blog Post([FromBody] Blog blog)
        {
            return new Blog();
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
