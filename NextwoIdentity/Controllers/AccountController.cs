using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Next2Identity.Models.ViewModels;
using NextwoIdentity.Models;
using NextwoIdentity.Models.ViewModels;
using System.Diagnostics;

namespace NextwoIdentity.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Configuration
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private RoleManager<IdentityRole> roleManager;


        public AccountController(UserManager<IdentityUser> _userManager,
            SignInManager<IdentityUser> _signInManager,
            RoleManager<IdentityRole> _roleManager)
        {
            userManager= _userManager;
            signInManager= _signInManager;
            roleManager=_roleManager;
        }
        #endregion


        #region Users
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Email,
                    Email= model.Email,
                    PhoneNumber=model.Phone
                };
                var result = await userManager.CreateAsync(user, model.Password!);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }

            return View(model);
        }

        [AllowAnonymous]

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync
                    (model.Email!, model.Password!, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid user name or password");
                return View(model); 
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
        #endregion

        #region Roles
        [Authorize(Roles ="Administrator")]
        public IActionResult CreateRole()
        {
           
            return View();  
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name=model.RoleName
                };
                var result=await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model); 
        }
        [Authorize(Roles = "Administrator")]

        public IActionResult RolesList()
        {
            return View(roleManager.Roles);
        }


        
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("RoleList");
            }
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("RoleList");
            }
            EditRoleViewModel model = new EditRoleViewModel { RoleId = role.Id, RoleName = role.Name };

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(model.RoleId!);
                if (role == null)
                {
                    return RedirectToAction(nameof(ErrorPage));
                }
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);

        }

        public IActionResult ErrorPage()
        {
            return View();
        }

        #endregion
    }

}
