using System;
using System.Net;
using System.Web.Http;

namespace Bridge.Controllers
{
    [RoutePrefix("api/exams")]
    public class ExamController : ApiController
    {
        // POST api/exams/upload?teacherId=123
        [HttpPost]
        [Route("upload")]
        public IHttpActionResult UploadAndSaveXml([FromUri] int teacherId)
        {
            if (teacherId <= 0)
                return Content(HttpStatusCode.BadRequest, "teacherId is required and must be > 0.");

            string xml;
            try
            {
                xml = Request.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrWhiteSpace(xml))
                    return Content(HttpStatusCode.BadRequest, "Request body is empty.");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Failed to read request body: " + ex.Message);
            }

            try
            {
                var parser = new ExamXmlParser();
                var teacher = parser.Parse(xml, teacherId);

                var grader = new ExamGradingService(new MathEvaluator());
                grader.Grade(teacher);

                var persister = new ExamPersistenceService();
                persister.Save(teacher);

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/exams/teacher/123
        [HttpGet]
        [Route("teacher/{teacherId:int}")]
        public IHttpActionResult GetTeacherExams(int teacherId)
        {
            if (teacherId <= 0)
                return Content(HttpStatusCode.BadRequest, "teacherId must be > 0.");

            try
            {
                var persister = new ExamPersistenceService();
                var teacher = persister.GetTeacherWithExams(teacherId);

                if (teacher == null)
                    return NotFound();

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/exams/student/456
        [HttpGet]
        [Route("student/{studentId:int}")]
        public IHttpActionResult GetStudentExams(int studentId)
        {
            if (studentId <= 0)
                return Content(HttpStatusCode.BadRequest, "studentId must be > 0.");

            try
            {
                var persister = new ExamPersistenceService();
                var grouped = persister.GetExamsForStudent(studentId);

                return Ok(grouped);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/exams/teacher/123/exams  -> returns exams only
        [HttpGet]
        [Route("teacher/{teacherId:int}/exams")]
        public IHttpActionResult GetExamsForTeacher(int teacherId)
        {
            if (teacherId <= 0)
                return Content(HttpStatusCode.BadRequest, "teacherId must be > 0.");

            try
            {
                var persister = new ExamPersistenceService();
                var teacher = persister.GetTeacherWithExams(teacherId);
                if (teacher == null)
                    return NotFound();

                var exams = teacher.Exams ?? new System.Collections.Generic.List<Entities.Exam>();
                return Ok(exams);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
