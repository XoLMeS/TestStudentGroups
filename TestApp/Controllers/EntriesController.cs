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
    public class EntriesController : Controller
    {
        private readonly TestContext _context;

        public EntriesController(TestContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Entry>> GetAll()
        {
            return _context.Entries.ToList();
        }

        [HttpGet("{id}", Name = "GetEntry")]
        public ActionResult<Entry> GetById(long id)
        {
            var item = _context.Entries.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }


        [HttpPost]
        [Route("create")]
        public IActionResult Create(Entry item)
        {
            int k = 0;
            foreach (Entry e in _context.Entries)
            {
                if (e.GroupId == item.GroupId)
                {
                    k++;
                }
            }
            if (k >= _context.Groups.Find(item.GroupId).Maxcap)
            {
                return StatusCode(400,"Maxcap reached");
            }
            _context.Entries.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetEntry", new { id = item.Id }, item);
        }
    }
}
