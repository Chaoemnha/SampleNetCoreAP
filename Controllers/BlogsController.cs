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
    public class BlogsController : ControllerBase
    {
        private readonly Service<Blog> _blogService;
        public BlogsController(Service<Blog> blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        public List<Blog> GetAll()
        {
            return _blogService.GetAll();
        }
        [HttpGet("{id}")]
        public Blog GetById(int id)
        {
            return _blogService.GetById(id);
        }
        [HttpPost()]
        [Produces("application/json")]
        public void Add(string name)
        {
            Blog blog = new Blog(name);
            _blogService.Add(blog); //Dang lam
        }
        [HttpPut("{id}")]
        public Blog Update(int id, string name, bool Active)
        {
            Blog blog = _blogService.GetById(id);
            blog.Name = name;
            blog.Active = Active;
            return _blogService.Update(blog);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _blogService.DeleteById(id);
        }
    }
}
