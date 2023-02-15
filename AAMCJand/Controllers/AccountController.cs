using AAMCJand.Data.Static;
using AAMCJand.Data;
using AAMCJand.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using AAMCJand.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Data;
using Novacode;
using Microsoft.AspNetCore.Authorization;

namespace AAMCJand.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _appDbContext;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
        }
        //public async Task<IActionResult> Users()
        //{
        //    //AccountVM accountVM = new AccountVM();
        //    //var users = await _appDbContext.Users.ToListAsync();
        //    //accountVM.Accounts = users.ToList();
        //    //return View(accountVM);

        //    var users = await _appDbContext.Users.ToListAsync();
        //    return View(users);
        //}
        

        public IActionResult Users()
        {
            var data =  _appDbContext.Users.ToList();
            
            return View(data);
        }
            
            //return Json(new { data = _appDbContext.Users.ToList() });
            //var data = await _appDbContext.Users.ToListAsync();
            //var json = JsonConvert.SerializeObject(data);
            //return Content(json, "application/json");

            //var users = await _appDbContext.Users.ToListAsync();
            //return View(users);
        
        public IActionResult Login() => View(new LoginVM());
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);
            var user = await _userManager.FindByNameAsync(loginVM.EmailAddress);
            //var user = _userManager.Users.FirstOrDefault(x => x.NormalizedEmail == loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Medicines");
                    }
                }
                TempData["Error"] = "Wrong Credentials";
                return View(loginVM);
            }
            TempData["Error"] = "Wrong Credentials";
            return View(loginVM);
        }
        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public JsonResult IsEmailInUse(RegisterVM registerVM,string email)
        {
            var user =  _userManager.FindByNameAsync(registerVM.EmailAddress);
            var user1 =  _userManager.Users.Where(e=>e.Email==email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use");
            }
        }

        public IActionResult Register() => View(new RegisterVM());
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            var user = await _userManager.FindByNameAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "Email already in use";
                return View(registerVM);
            }
            var newUser = new ApplicationUser()
            {

                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
                LockoutEnabled = false,
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (newUserResponse.Succeeded) await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return View("RegisterCompleted");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Medicines");
        }
    }
}
