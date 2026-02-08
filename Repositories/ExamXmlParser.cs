using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Entities;

public class ExamXmlParser
{
    public Teacher Parse(string xml, int loggedInTeacherId)
    {
        if (string.IsNullOrWhiteSpace(xml))
            throw new ArgumentException("XML is empty");

        var doc = XDocument.Parse(xml);

        var teachersRoot = doc.Root;
        if (teachersRoot == null || teachersRoot.Name != "Teachers")
            throw new Exception("Root element <Teachers> is missing");

        var teacherElement = teachersRoot
            .Elements("Teacher")
            .FirstOrDefault(t =>
                ParseIntAttr(t, "ID") == loggedInTeacherId);

        if (teacherElement == null)
            throw new Exception("Logged-in teacher not found in XML");

        var teacher = new Teacher
        {
            TeacherId = loggedInTeacherId,
            Students = new List<Student>()
        };

        var studentsElement = teacherElement.Element("Students");
        if (studentsElement == null)
            return teacher;

        foreach (var studentElement in studentsElement.Elements("Student"))
        {
            var student = new Student
            {
                StudentId = ParseIntAttr(studentElement, "ID"),
                Exams = new List<Exam>()
            };

            foreach (var examElement in studentElement.Elements("Exam"))
            {
                var exam = new Exam
                {
                    Id = ParseIntAttr(examElement, "Id"),
                    Tasks = new List<Task>()
                };

                foreach (var taskElement in examElement.Elements("Task"))
                {
                    var parts = taskElement.Value.Split('=');
                    if (parts.Length != 2)
                        continue;

                    exam.Tasks.Add(new Task
                    {
                        Id = ParseIntAttr(taskElement, "id"),
                        Expression = parts[0].Trim(),
                        ExpectedResult = decimal.Parse(
                            parts[1].Trim(),
                            CultureInfo.InvariantCulture)
                    });
                }

                student.Exams.Add(exam);
            }

            teacher.Students.Add(student);
        }

        return teacher;
    }

    private int ParseIntAttr(XElement element, string attrName)
    {
        var attr = element.Attribute(attrName);
        if (attr == null)
            throw new Exception($"Missing attribute '{attrName}'");

        return int.Parse(attr.Value);
    }
}