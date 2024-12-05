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
        private readonly Service<Blog> _blogService;
        public BlogController(Service<Blog> blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        public List<Blog> Get()
        {
            return _blogService.GetAll();
        }
        [HttpGet("{BlogId}", Name = "Get")]
        public Blog Get(int id)
        {
            return _blogService.GetById(id);
        }
        //[HttpPost]
        //[Produces("application/json")]
        //public Blog Post([FromBody] Blog blog)
        //{
        //    return _blogService.addABlog(blog); //Dang lam
        //}
        [HttpPut]
        public void Put([FromBody] Blog Blog)
        {
            _blogService.Add(Blog);
        }
        [HttpDelete("{BlogId}")]
        public void Delete(int id)
        {
            _blogService.DeleteById(id);
        }
    }
}
