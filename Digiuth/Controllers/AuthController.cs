using Digiuth.Models;
using Digiuth.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiuth.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthController(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();

            AppUser appUser = await _userManager.FindByEmailAsync(login.Email);

            if (appUser == null)
            {
                ModelState.AddModelError("", "Email or Password is wrong!!");
                return View(login);
            }

            var signinResult = await _signInManager.PasswordSignInAsync(appUser, login.Password, true, true);

            if (!signinResult.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is wrong!!");
                return View(login);
            }

            if (signinResult.IsLockedOut)
            {
                ModelState.AddModelError("", "The account is locked Out");
                return View(login);
            }

            var role = (await _userManager.GetRolesAsync(appUser))[0];

            if (role == "Admin")
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();

            AppUser newUser = new AppUser
            {
                FullName = register.FullName,
                UserName = register.UserName,
                Email = register.Email
            };

            IdentityResult identityResult = await _userManager.CreateAsync(newUser, register.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }
            await _userManager.AddToRoleAsync(newUser, "User");

            await _signInManager.SignInAsync(newUser, true);

            return RedirectToAction("Index", "Home");
        }

        //public async Task CreateRole()
        //{
        //    if (!await _roleManager.RoleExistsAsync("Admin"))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    }
        //    if (!await _roleManager.RoleExistsAsync("User"))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
        //    }
        //}
    }
}
