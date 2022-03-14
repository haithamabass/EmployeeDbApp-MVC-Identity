namespace EmployeeDbApp.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin, Moderator, Member")]
    public class UsersWithRolesController:Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<EmployeeDbAppUser> userManager;

        public UsersWithRolesController(UserManager<EmployeeDbAppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<List<string>> GetUserRoles(EmployeeDbAppUser user)
        {
            return new List<string>(await userManager.GetRolesAsync(user));
        }



        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult>Index()
        {
            var users = await userManager.Users.ToListAsync();
            var userRoles = new List<UsersWithRoles>();

            foreach (EmployeeDbAppUser user in users)
            {
                var details = new UsersWithRoles();
                details.UserId = user.Id;
                details.FirstName = user.FirstName;
                details.LastName = user.LastName;
                details.UserName = user.Email;
                details.Roles = await GetUserRoles(user);
                userRoles.Add(details);
            }


            return View(userRoles);
        }



        //Add new user
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Add(AddNewUsers model)

        {
            IEnumerable<IdentityRole> roles = roleManager.Roles.ToList();
            ViewData["Role"] = new SelectList(roles.ToList(), "Id", "Name");
            var role = roleManager.FindByIdAsync(model.Roles).Result;


            if (ModelState.IsValid)
            {
                var user = new EmployeeDbAppUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
               
                // create a new user with a default password
                var result = await userManager.CreateAsync(user, "d12356f");
                await userManager.AddToRoleAsync(user, role.Name);


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
            }

            return View(model);
        }





        // manage roles for users
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Manage(string userId)
        {
            
            //Get the user by Id 
            var user = await userManager.FindByIdAsync(userId);

            //define UserName
            ViewBag.UserNanme = user.Email;


           
            if (user == null)
            {
                
                return View("cannot be found");
            }



            var model = new List<ManageUserAndRoles>();
            var roles = roleManager.Roles;

            foreach (var role in roles)
            {
                var userRolesManage = new ManageUserAndRoles()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesManage.Selected = true;
                }
                else
                {
                    userRolesManage.Selected = false;
                }

                model.Add(userRolesManage);
            }

            return View(model);
        }





        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserAndRoles> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {

                return View();
            }
            
            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user's existing roles");
                return View(model);
            }
            result = await userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("Index");

        }




        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user != null)
            {
                await userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }







    }
}
