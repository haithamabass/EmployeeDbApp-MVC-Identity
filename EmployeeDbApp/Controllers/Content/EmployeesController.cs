using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;


namespace EmployeeDbApp.Controllers.Content
{
    public class EmployeesController : Controller
    {

        private readonly AppDbContext _dbContext;


        public EmployeesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        //get all employees method
        private List<Employee> GetEmployees()
        {
            return (from Employee in _dbContext.Employees
                    join City in _dbContext.Cites on Employee.CityId equals City.CityId
                    join Department in _dbContext.Departments on Employee.DepartmentId equals Department.DepartmentId
                    select new Employee
                    {
                        EmployeeId = Employee.EmployeeId,
                        EmployeeName = Employee.EmployeeName,
                        DOB = Employee.DOB,
                        HiringDate = Employee.HiringDate,
                        GrossSalary = Employee.GrossSalary,
                        NetSalary = Employee.NetSalary,
                        CityId = Employee.CityId,
                        CityName = City.CityName,
                        DepartmentId = Employee.DepartmentId,
                        DepartmentName = Department.DepartmentName,
                        JobTitle= Employee.JobTitle,
                        Email = Employee.Email,
                        PhoneNumber = Employee.PhoneNumber

                    }).ToList();


        }




        // gett all 
        [Authorize(Roles = "SuperAdmin, Admin, Moderator")]
        // GET: EmployeesController
        public async Task <IActionResult> Index(string sortOrder, string Search)
        {


            var employees = GetEmployees();


            //sort method

            ViewData["EmployeeName"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DOB"] = sortOrder == "dateOfBirth" ? "birth_date_desc" : "dateOfBirth";
            ViewData["HiringDate"] = sortOrder == "dateOfHiring" ? "hire_date_desc" : "dateOfHiring";
            ViewData["GrossSalary"] = sortOrder == "grossSalary" ? "grossSalary_desc" : "grossSalary";
            ViewData["NetSalary"] = sortOrder == "netSalary" ? "NetSalary_desc" : "netSalary";
            ViewData["CityName"] = string.IsNullOrEmpty(sortOrder) ? "city_desc" : "city_asc";
            ViewData["DepartmentName"] = string.IsNullOrEmpty(sortOrder) ? "department_desc" : "";
            ViewData["JobTitle"] = string.IsNullOrEmpty(sortOrder) ? "jobT_desc" : "";


            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(s => s.EmployeeName).ToList();
                    break;
                case "dateOfBirth":
                    employees = employees.OrderBy(s => s.DOB).ToList();
                    break;
                case "birth_date_desc":
                    employees = employees.OrderByDescending(s => s.DOB).ToList();
                    break;

                case "dateOfHiring":
                    employees = employees.OrderBy(s => s.HiringDate).ToList();
                    break;

                case "hire_date_desc":
                    employees = employees.OrderByDescending(s => s.HiringDate).ToList();
                    break;

                case "grossSalary":
                    employees = employees.OrderBy(s => s.GrossSalary).ToList();
                    break;

                case "grossSalary_desc":
                    employees = employees.OrderByDescending(s => s.GrossSalary).ToList();
                    break;

                case "NetSalary_desc":
                    employees = employees.OrderByDescending(s => s.NetSalary).ToList();
                    break;

                case "netSalary":
                    employees = employees.OrderBy(s => s.NetSalary).ToList();
                    break;

                case "jobT_desc":
                    employees = employees.OrderByDescending(s => s.JobTitle).ToList();
                    break;


                default:
                    employees = employees.OrderBy(s => s.EmployeeName).ToList();
                    break;
            }


            // search method

            if (!string.IsNullOrEmpty(Search))
            {
                employees = employees.Where(e => e.EmployeeName.Contains(Search) || e.DepartmentName.Contains(Search) ||
                e.CityName.Contains(Search) || e.GrossSalary.ToString().Contains(Search) || e.NetSalary.ToString().Contains(Search)).ToList();
            }
           

            return View(employees);
        }








        // GET: EmployeesController/Create
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Create()
        {
          
            ViewBag.City =  _dbContext.Cites.ToList();
            ViewBag.Department =  _dbContext.Departments.ToList();


            return View();
        }


        // POST: EmployeesController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Employee model)
        {

            ModelState.Remove("City");
            ModelState.Remove("CityName");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");


            if (ModelState.IsValid)
            {
                if(await _dbContext.Employees.AnyAsync(e => e.Email == model.Email))
                {
                    return BadRequest("email  already used");
                }

              
                 await _dbContext.Employees.AddAsync(model);
                 _dbContext.SaveChanges();
                 return RedirectToAction("index");


            }

                ViewBag.City = _dbContext.Cites.ToList();
                ViewBag.Department = _dbContext.Departments.ToList();
                return View("Create", model);
        }




        // GET: EmployeesController/Edit/5
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task <IActionResult> Edit(int id)
        {

            Employee data = await _dbContext.Employees.FindAsync(id);

            ViewBag.City = _dbContext.Cites.ToList();
            ViewBag.Department = _dbContext.Departments.ToList();
            return View("Edit", data);
        }




        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(Employee model)
        {


            ModelState.Remove("City");
            ModelState.Remove("CityName");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");

            if (ModelState.IsValid)
            {

                _dbContext.Employees.Update(model);
                _dbContext.SaveChanges();
                return RedirectToAction("index");
            }

            ViewBag.City = _dbContext.Cites.ToList();
            ViewBag.Department = _dbContext.Departments.ToList();
            return View("Edit", model);


        }



        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task <IActionResult> Delete(int id)
        {
            Employee data = await _dbContext.Employees.FindAsync(id);
                _dbContext.Remove(data);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            
            
        }











    }
}
