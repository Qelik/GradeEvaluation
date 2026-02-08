using System.Collections.Generic;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    [IdAuthorize(Role = "Student")]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class StudentController : BaseController
    {
        
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account", new { role = "Student" });
            }

            return View("Index");
        }

        [HttpGet]
        public ActionResult Results()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account", new { role = "Student" });
            var persister = new ExamPersistenceService();
            var model = persister.GetExamsForStudent((int)Session["UserId"]);

            return View(model);
        }
    }
}