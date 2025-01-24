﻿using GuestBookSPA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace GuestBookSPA.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserContext _context;

        public AccountController(UserContext context)
        {
            _context = context;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                if(_context.Users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                var users = _context.Users.Where(a => a.Login == logon.Login);
                if (users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                var user = users.First();
                string? salt = user.Salt;

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + logon.Password);

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (user.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                HttpContext.Session.SetString("Name", user.Login); // создание сессионной переменной

                HttpContext.Session.SetString("Id", user.Id.ToString());

                return RedirectToAction("Index", "Home");
            }
            return View(logon);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel reg)
        {

            if (ModelState.IsValid)
            {
                var users = _context.Users.Where(a => a.Login == reg.Login);

                if (users.ToList().Count != 0)
                { 
                    ModelState.AddModelError("", "User with this login already exists!");
                    return View(reg);
                }

                Users user = new Users();
                    user.Login = reg.Login;

                    byte[] saltbuf = new byte[16];

                    RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                    randomNumberGenerator.GetBytes(saltbuf);

                    StringBuilder sb = new StringBuilder(16);
                    for (int i = 0; i < 16; i++)
                        sb.Append(string.Format("{0:X2}", saltbuf[i]));
                    string salt = sb.ToString();

                    //переводим пароль в байт-массив  
                    byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);

                    //вычисляем хеш-представление в байтах  
                    byte[] byteHash = SHA256.HashData(password);

                    StringBuilder hash = new StringBuilder(byteHash.Length);
                    for (int i = 0; i < byteHash.Length; i++)
                        hash.Append(string.Format("{0:X2}", byteHash[i]));

                    user.Password = hash.ToString();
                    user.Salt = salt;
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return RedirectToAction("Login");

            }
            return View(reg);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}