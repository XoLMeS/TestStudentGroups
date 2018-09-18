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
    public class GroupController : Controller
    {
        private readonly TestContext _context;

        public GroupController(TestContext context)
        {
            _context = context;
        }

        // GET: Group
        public async Task<IActionResult> Index()
        {
            return View(await _context.Groups.ToListAsync());
        }

        [HttpGet]
        public ActionResult<List<Group>> GetAll()
        {
            List<Group> groups = _context.Groups.ToList();
            foreach (Group g in groups)
            {
                int k = 0;
                foreach (Entry e in _context.Entries)
                {
                    if (g.Id == e.GroupId)
                    {
                        k++;
                    }
                }
                g.Capacity = k;
            }
            return groups;
        }
    
        
        [HttpGet("{id}", Name = "GetGroup")]
        public ActionResult<Group> GetById(long id)
        {
            var item = _context.Groups.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpGet("students", Name = "GetByGroup")]
        public List<Student> GetByGroup(long id)
        {
            var students_ids = from e in _context.Entries
                               where e.GroupId == id
                               select e.StudentId;

            var results = (from p in _context.Students
                           where students_ids.Contains(p.Id)
                           select p).Distinct().ToList();

            return results;
        }

        // GET: Group/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Group/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(Group item)
        {
            _context.Groups.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetGroup", new { id = item.Id }, item);
        }

        // GET: Group/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            return View(@group);
        }

        /* POST: Group/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Number,Capacity,Maxcap")] Group @group)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }
        */

        // GET: Group/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var @group = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(long id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
