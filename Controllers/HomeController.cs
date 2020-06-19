using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginRegTest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LoginRegTest.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }

        public  User GetUser()
        {
            return _context.Users.FirstOrDefault( u =>  u.UserID == HttpContext.Session.GetInt32("userId"));
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if(!_isLogged()){
                return View();
            }
                
            // List<User> AllUsers = _context.Users.ToList();

            
            return RedirectToAction("Dashboard");
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            if(!_isLogged())
                return RedirectToAction("Index");
            
            int uid = (int)HttpContext.Session.GetInt32("uid");
            ViewBag.Uid = uid;

            List<Plan> plans = _context.Plans
            .Include(p => p.Organizer)
            .Include(p => p.Guests)
            // .Where(u=>u.UserID==uid)
            .ToList();

            return View("Dashboard",plans);
        }

        [HttpGet("plan/{planId}/{status}")]
        public IActionResult TogglePlan(int planId, string status)
        {
           if(!_isLogged())
                return RedirectToAction("Index");

            int uid = (int)HttpContext.Session.GetInt32("uid");

            if(status == "join")
            {
                Reservation reservation = new Reservation();
                reservation.UserID = uid;
                reservation.PlanID = planId;
                _context.Reservations.Add(reservation);
            }
            else if(status == "leave")
            {
                Reservation backout = _context.Reservations.FirstOrDefault( w => w.UserID == uid && w.PlanID == planId );
                _context.Reservations.Remove(backout);
            }
            _context.SaveChanges();
            return RedirectToAction("dashboard");
        }

        [HttpGet("wedding/add")]
        public IActionResult AddWedding()
        {
            if(!_isLogged())
                return RedirectToAction("Index");
            
            int uid = (int)HttpContext.Session.GetInt32("uid");

            return View();
        }


        [HttpGet("wedding/{planId}")]
        public IActionResult WeddingInfo(int planId)
        {
            if(!_isLogged())
                return RedirectToAction("Index");

            int uid = (int)HttpContext.Session.GetInt32("uid");

            var wedding = _context.Plans
                .Include(p => p.Guests)
                .ThenInclude(rsrv => rsrv.Guest)
                .FirstOrDefault(p => p.PlanID == planId);
            
            return View(wedding);
        }

        [HttpGet("plan/{planId}/delete")]
        public IActionResult DeletePlan(int planId)
        {
            if(!_isLogged())
                return RedirectToAction("Index");
            
            int uid = (int)HttpContext.Session.GetInt32("uid");
            Plan rPlan = _context.Plans.FirstOrDefault( p => p.PlanID == planId);
            _context.Plans.Remove(rPlan);
            _context.SaveChanges();
            return RedirectToAction("dashboard");
        }

        [HttpPost("wedding/save")]
        public IActionResult SaveWedding(Plan plan)
        {
            if(!_isLogged())
                return RedirectToAction("Index");
            
            int uid = (int)HttpContext.Session.GetInt32("uid");

            if(ModelState.IsValid){

                plan.UserID = uid;
                _context.Plans.Add(plan);
                _context.SaveChanges();

                return RedirectToAction("Dashboard");
            }

            return View("AddWedding");
        }




        [HttpGet("logout")]
        public IActionResult Logout()
        {
            if(_isLogged()){
               HttpContext.Session.Remove("uid");
            }
            return RedirectToAction("Index");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser user)
        {
            if(ModelState.IsValid){
                
            // If inital ModelState is valid, query for a user with provided email
            var userInDb = _context.Users.FirstOrDefault(u => u.Email == user.LoginEmail);
            // If no user exists with provided email
            if(userInDb == null)
            {
                // Add an error to ModelState and return to View!
                ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                return View("Index");
            }
            
            // Initialize hasher object
            var hasher = new PasswordHasher<LoginUser>();
            
            // verify provided password against hash stored in db
            var result = hasher.VerifyHashedPassword(user, userInDb.Password, user.LoginPassword);
            
            // result can be compared to 0 for failure
            if(result == 0)
            {
                // handle failure (this should be similar to how "existing email" is handled)
                ModelState.AddModelError("Login", "Invalid Email/Password");
                return View("Index");
            }

                HttpContext.Session.SetInt32("uid",userInDb.UserID);
                return RedirectToAction("Dashboard");
            }
            else
                return View("Index");
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid){
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Success");
            }
            else
                return View("Index");
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            return View("Success");
        }

        private bool _isLogged() {
            return HttpContext.Session.GetInt32("uid")!=null;
        }

    }
}
