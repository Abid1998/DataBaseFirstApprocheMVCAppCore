using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataBaseFirstApprocheMVCAppCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataBaseFirstApprocheMVCAppCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly CrudMVCCoreContext _context;

        //constructor
        public HomeController(CrudMVCCoreContext context)
        {
            _context = context;
        }
       
    
        public async Task<IActionResult> Index()
        {
           var std = BindDDL();
            // Set Session 
            HttpContext.Session.SetString("MyKey", "Abid Khan");
            return View(std);
        }

        private StudentModel BindDDL()
        {
            StudentModel stdModel = new StudentModel();
            stdModel.StudentsList = new List<SelectListItem>();

            var data = _context.Students.ToList();

            stdModel.StudentsList.Add(new SelectListItem
            {
                Text = "Select Name",
                Value = ""
            });

            foreach (var item in data)
            {
                stdModel.StudentsList.Add(new SelectListItem
                {
                    Text = item.StudentName,
                    Value = item.Id.ToString()
                });
            }
            return stdModel;
        }

        [HttpPost]
        public IActionResult Index(StudentModel std)
        {
            var Student = _context.Students.Where(x => x.Id== std.id).FirstOrDefault();
            if (Student!=null)
            {
                ViewBag.SelectedValue = Student.StudentName;
            }
            var Mystd = BindDDL();
            return View(Mystd);
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (HttpContext.Session.GetString("MyKey") != null)
            {
                ViewBag.data = HttpContext.Session.GetString("MyKey").ToString();

            }

            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
      


        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentName,StudentGender,Age,Standard,Email,Phone")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'CrudMVCCoreContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
