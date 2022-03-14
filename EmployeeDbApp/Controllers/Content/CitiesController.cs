

namespace EmployeeDbApp.Controllers.Content
{
    public class CitiesController : Controller
    {

        private readonly AppDbContext _dbContext;

        public CitiesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        // GET: CitysController
        [Authorize(Roles = "SuperAdmin, Admin, Moderator")]
        public async Task<IActionResult> Index()
        {

            var cites = await _dbContext.Cites.ToListAsync();
            return View(cites);
        }





        // GET: CitysController/Create
        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult Create()
        {
            return View();
        }


        // POST: CitysController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task <IActionResult> Create(City model)
        {
            if(ModelState.IsValid)
            {
                if (await _dbContext.Cites.AnyAsync(e => e.CityName == model.CityName))
                {
                    return BadRequest("city already registered");
                }


                var results =  await _dbContext.Cites.AddAsync(model);
                _dbContext.SaveChanges();
                
                return RedirectToAction("Index");
            }
            
            
                return View("Index", model);
            
        }





        // GET: CitysController/Edit/5
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            City data = await _dbContext.Cites.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }


            return View("Edit", data);
        }




        // POST: CitysController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(City model)
        {


            if (ModelState.IsValid)
            {

                 _dbContext.Cites.Update(model);
                _dbContext.SaveChanges();
                

                return RedirectToAction("Index");
            }
            

                return View("Edit", model);
            
        }







        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task <IActionResult> Delete(int id)
        {
            
            City data = await _dbContext.Cites.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            _dbContext.Cites.Remove(data);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}
