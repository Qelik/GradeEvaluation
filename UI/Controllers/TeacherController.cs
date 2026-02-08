using Entities;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    [IdAuthorize(Role = "Teacher")]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class TeacherController : BaseController
    {
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account", new { role = "Teacher" });
            }

            return View("Index");
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            var teacherId = (int)Session["UserId"];

            using (var reader = new StreamReader(file.InputStream))
            {
                var xml = reader.ReadToEnd();

                var parser = new ExamXmlParser();
                var teacher = parser.Parse(xml, teacherId);

                var grader = new ExamGradingService(new MathEvaluator());
                grader.Grade(teacher);

                try
                {
                    var persister = new ExamPersistenceService();
                    persister.Save(teacher);
                }
                catch (System.Exception ex)
                {
                    // replace with your logging facility
                    ViewBag.Error = "Failed to save results: " + ex.Message;
                    return View("Results", teacher);
                }

                return RedirectToAction("Results");
            }
        }

        [HttpGet]
        public ActionResult Results()
        {
            Teacher teacher = new Teacher();
            try
            {
                var persister = new ExamPersistenceService();
                teacher = persister.GetTeacherWithExams((int)Session["UserId"]);
            }
            catch (System.Exception ex)
            {
                // replace with your logging facility
                ViewBag.Error = "Failed to save results: " + ex.Message;
                return View("Results", teacher);
            }
            return View(teacher);
        }
    }
}