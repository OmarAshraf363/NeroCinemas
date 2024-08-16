using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nero.Models;
using Nero.ViewModel;

namespace Nero.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly  SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        //public async Task<IActionResult> AddRole()
        //{
        //   await roleManager.CreateAsync(new IdentityRole("Admin"));
        //    await roleManager.CreateAsync(new IdentityRole("Customar"));
        //    return Content("addedd");
        //}
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AppUserVM model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    UserName = model.Name,
                    Email = model.Email,
                    Address = model.Address,

                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user,"Customar");
                    return RedirectToAction("Index", "Home");

                }
            }
            return View(model);
        }
        public  IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
              var user= await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                   var found= await userManager.CheckPasswordAsync(user, model.Password);
                    //check Password
                    if (found)
                    {
                        await signInManager.SignInAsync(user, model.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Wrong Email Or Password");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Wrong Email Or Password");
                }
                
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {

          await  signInManager.SignOutAsync();
            return RedirectToAction("Login"); ;
        }
        public IActionResult Contact()
        {
            return View();
        }

    }
}
