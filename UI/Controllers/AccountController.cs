using Entities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login(string role)
        {
            // Validate role
            if (role != "Teacher" && role != "Student")
                role = "Student"; // safe default

            // Store intent in session (not logged in yet)
            Session["LoginRole"] = role;

            // Set display values for the view
            ViewBag.LoginTitle = role == "Teacher"
                ? "Professor Login"
                : "Student Login";

            ViewBag.IdLabel = role == "Teacher"
                ? "Professor ID"
                : "Student ID";

            return View();
        }

        [HttpPost]
        public ActionResult Login(int id)
        {
            if (id <= 0)
            {
                ViewBag.Error = "Invalid ID";
                return View();
            }

            var role = Session["LoginRole"] as string ?? "Student";

            try
            {
                using (var db = new SchoolDbContext())
                {
                    if (role == "Teacher")
                    {
                        var teacher = db.Teachers.FirstOrDefault(t => t.TeacherId == id);
                        if (teacher == null)
                        {
                            ViewBag.Error = "Teacher not found";
                            return View();
                        }

                        // Save identity into session using internal PKs
                        Session["UserId"] = teacher.TeacherId;
                        Session["UserRole"] = "Teacher";
                        Session["DisplayName"] = $"Professor {id}";

                        return RedirectToAction("Index", "Teacher");
                    }
                    else // Student
                    {
                        var student = db.Students.FirstOrDefault(s => s.StudentId == id);
                        if (student == null)
                        {
                            ViewBag.Error = "Student not found";
                            return View();
                        }

                        Session["UserId"] = student.StudentId;
                        Session["UserRole"] = "Student";
                        Session["DisplayName"] = $"Student {id}";

                        return RedirectToAction("Index", "Student");
                    }
                }
            }
            catch (Exception ex)
            {
                // Replace with your logging facility as needed
                ViewBag.Error = "Login failed: " + ex.Message;
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }

        private string GetRoleFromId(int id)
        {
            // SIMPLE INTERVIEW ASSUMPTION
            // Teachers have IDs >= 10000
            return id >= 10000 ? "Teacher" : "Student";
        }
    }
}