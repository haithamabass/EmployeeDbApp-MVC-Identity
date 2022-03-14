

namespace EmployeeDbApp.Controllers.Content
{
    public class DepartmentsController : Controller
    {

        private readonly AppDbContext _dbContext;

        public DepartmentsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }




        // GET: DepartmentsController
        [Authorize(Roles = "SuperAdmin, Admin, Moderator")]
        public async Task <IActionResult> Index()
        {

            var departments = await _dbContext.Departments.ToListAsync();
            return View(departments);

            return View();
        }





        // GET: DepartmentsController/Create
        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult Create()
        {
            return View();
        }


        // POST: DepartmentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)
            {
                if (await _dbContext.Departments.AnyAsync(e => e.DepartmentName == model.DepartmentName))
                {
                    return BadRequest("Department already registered");
                }

                await _dbContext.Departments.AddAsync(model);
                _dbContext.SaveChanges();
                
                return RedirectToAction("Index");
            }
            
            return View("Index", model);
        }





        // GET: DepartmentsController/Edit/5
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task <IActionResult> Edit(int id)
        {
            Department data =  await _dbContext.Departments.FindAsync(id);

            return View("Edit", data);
        }





        // POST: DepartmentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(Department model)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Departments.Update(model);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            
                return View("Edit", model);
          
        }




        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task <IActionResult> Delete(int id)
        {
            
            Department data = await _dbContext.Departments.FindAsync(id);

            _dbContext.Departments.Remove(data);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}
