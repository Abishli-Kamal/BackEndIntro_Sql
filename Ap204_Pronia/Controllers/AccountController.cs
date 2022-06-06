using Ap204_Pronia.Models;
using Ap204_Pronia.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ap204_Pronia.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly SignInManager<AppUser> _signInManager;
            
        public AccountController(UserManager<AppUser> usermanager, SignInManager<AppUser> signInManager)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = new AppUser
            {
              
                Firstname = registerVm.Firstname,
                Lastname = registerVm.Lastname,
                Email = registerVm.Email,  
                UserName = registerVm.Username
            };
            if (registerVm.IHaveReadIAccept==true)
            {
                IdentityResult result = await _usermanager.CreateAsync(user, registerVm.Password);
                if (!result.Succeeded)
                {

                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View();
                }

            }
            else
            {
                ModelState.AddModelError("", "You must accept the terms");
                return View();

            }

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Login(LoginVm login)
        {
            AppUser user = await _usermanager.FindByNameAsync(login.Username);
            if (user == null) return View();
            //IdentityResult result = await _usermanager.CreateAsync(user,loginVm.Password);
           
            if (login.RememberMe)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, true, true);

                if (!result.Succeeded)
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "You have been dismissed for 5 minutes");
                        return View();
                    }   
                    ModelState.AddModelError("", "Username or Password is incorrect");
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
                    {

                    }
                    ModelState.AddModelError("", "Username or Password is incorrect");
                    return View();

                }
            }

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }

}