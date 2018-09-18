using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly TestContext _context;

        public StudentController(TestContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Student>> GetAll()
        {
            return _context.Students.ToList();
        }


        [HttpGet("{id}", Name = "GetStudent")]
        public ActionResult<Student> GetById(long id)
        {
            var item = _context.Students.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(Student item)
        {
            _context.Students.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetStudent", new { id = item.Id }, item);
        }

       
    }
}
