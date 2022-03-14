using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace EmployeeDbApp.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
       

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
           
        }




        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }





        public ActionResult AddRole()
        {
            return View(new IdentityRole());
        }


        [HttpPost]
        public async Task<IActionResult> AddRole(IdentityRole role )
        {

             var result = await _roleManager.CreateAsync(role);


            if (result.Succeeded)
            {

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(role);

        }

        




        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            // Find the role by Role ID
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var model = new IdentityRole
            {
                Name = role.Name
            };

            return View(model);
        }



       
        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                return NotFound();
            }
            else
            {
                role.Name = model.Name;

                // Update the Role using UpdateAsync
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }




        
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Index");
            }
        }





    }
}
