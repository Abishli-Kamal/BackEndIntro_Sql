
using Ap204_Pronia.Models;
using Ap204_Pronia.Utilities;
using Ap204_Pronia.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ap204_Pronia.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVm register)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = new AppUser
            {
                Firstname = register.Firstname,
                Lastname = register.Lastname,
                Email = register.Email,
                UserName = register.Username
            };
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View();
                }
            }
            await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVm login)
        {
            AppUser user = await _userManager.FindByNameAsync(login.Username);
            if (user == null) return View();

            IList<string> roles = await _userManager.GetRolesAsync(user);
            string role = roles.FirstOrDefault(r => r == Roles.Admin.ToString());
            if (role == null)
            {

                ModelState.AddModelError("", "Please contact with admins");
                return View();

            }
            else
            {
                if (login.RememberMe)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, true, true);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Username or Password Incorrect");
                        return View();
                    }

                }
                else
                {

                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);
                    if (!result.Succeeded)
                    {
                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError("", "You have been dismissed for 5 minutes");
                            return View();
                        }
                        ModelState.AddModelError("", "Username or Password Incorrect");
                        return View();
                    }

                }
            }

            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public async Task<IActionResult> Edit()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound();

            EditUserVM edit = new EditUserVM
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Username = user.UserName,

            };
            return View(edit);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(EditUserVM user)
        {
            AppUser existeduser = await _userManager.FindByNameAsync(User.Identity.Name);
            bool result = user.Password == null && user.ConfirmPassword == null && user.CurrenPassword != null;
            EditUserVM edit = new EditUserVM
            {
                Firstname = existeduser.Firstname,
                Lastname = existeduser.Lastname,
                Email = existeduser.Email,
                Username = existeduser.UserName,

            };
            if (!ModelState.IsValid) return View(edit);
            if (user.Email == null && existeduser.Email != null)
            {
                ModelState.AddModelError("", "You can not change your email");
                return View(edit);
            }

            if (result)
            {
                existeduser.Firstname = user.Firstname;
                existeduser.Lastname = user.Lastname;
                existeduser.UserName = user.Username;
                await _userManager.UpdateAsync(existeduser);

            }
            else
            {
                existeduser.Firstname = user.Firstname;
                existeduser.Lastname = user.Lastname;
                existeduser.UserName = user.Username;

                if (user.CurrenPassword == user.Password)
                {
                    ModelState.AddModelError("", "You can not change password with the same password ");
                    return View();
                }
                IdentityResult resultedit = await _userManager.ChangePasswordAsync(existeduser, user.CurrenPassword, user.Password);

                if (!resultedit.Succeeded)
                {
                    foreach (IdentityError error in resultedit.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(edit);
                }
            }

            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Register", "Account");
        }
        public async Task CreateRole()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Member.ToString() });
            await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Admin.ToString() });
            await _roleManager.CreateAsync(new IdentityRole { Name = Roles.SuperAdmin.ToString() });
        }
    }
}
