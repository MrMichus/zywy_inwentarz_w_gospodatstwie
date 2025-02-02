﻿using Inwentarz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Inwentarz.ViewModels;

namespace Inwentarz.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly InwentarzDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, InwentarzDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Imie = model.Imie,
                    Nazwisko = model.Nazwisko,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, model.Password);


                //// DLA UZYTKOWNIKA

                //if (result.Succeeded)
                //{
                //    // Dodanie roli "Klient" dla nowego użytkownika
                //    if (!await _userManager.IsInRoleAsync(user, "Uzytkownik"))
                //    {
                //        await _userManager.AddToRoleAsync(user, "Uzytkownik");
                //    }


                //TYLKO DLA PRACOWNIKOW
                if (result.Succeeded)
                {
                    // Dodanie roli "Klient" dla nowego użytkownika
                    if (!await _userManager.IsInRoleAsync(user, "Pracownik"))
                    {
                        await _userManager.AddToRoleAsync(user, "Pracownik");
                    }


                    var pracownik = new Pracownik
                    {
                        Imie = model.Imie,
                        Nazwisko = model.Nazwisko,
                        Email = model.Email,
                        Telefon = model.PhoneNumber,
                        ApplicationUserId = user.Id,
                        Stanowisko = " Opiekun",//Administrator,Weterynarz
                        DataZatrudnienia = DateOnly.FromDateTime(DateTime.Now)
                    };


                    _context.Pracownik.Add(pracownik);
                    await _context.SaveChangesAsync();




                    //



                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var roles = await _userManager.GetRolesAsync(user);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Błędny login lub hasło.");
            }

            return View(model);
        }




    }
}
