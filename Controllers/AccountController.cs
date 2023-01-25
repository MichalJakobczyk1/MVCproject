﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCproject.Data;
using MVCproject.Migrations;
using MVCproject.Models;
using MVCproject.ViewModels;

namespace MVCproject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<Employee> userManager, SignInManager<Employee> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            //check if user is in databse
            if (user != null) 
            {
                //check if password is correct
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Home");
                    }
                }
                //password is incorrect
                else
                {
                    TempData["Error"] = "Wrong credentials, try again.";
                    return View(loginViewModel);
                }
            }
            //user not found
            TempData["Error"] = "Wrong credentials, try again.";
            return View(loginViewModel);
        }

        //public IActionResult Register()
        //{
        //    var response = new RegisterViewModel();
        //    return View(response);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        //{
        //    if (!ModelState.IsValid) return View(registerViewModel);

        //    var user = await _userManager.FindByEmailAsync(registerViewModel.Email);

        //    if (user != null)
        //    {
        //        TempData["Error"] = "This email address is already taken";
        //        return View(registerViewModel);
        //    }
        //    else
        //    {
        //        var newUser = new Employee()
        //        {
        //            Email = registerViewModel.Email,
        //            UserName = registerViewModel.Email,
        //        };

        //        var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

        //        if (newUserResponse.Succeeded)
        //        {
        //            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
        //        }

        //        return View("RegisterSuccess");
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}