[TestClass]
public class XmlParserTests
{
    [TestMethod]
    public void Parse_ValidXml_ReturnsTeacherWithStudents()
    {
        string xml = @"
<Teacher ID='11111'>
  <Students>
    <Student ID='123'>
      <Exam Id='1'>
        <Task id='1'>2+2 = 4</Task>
      </Exam>
    </Student>
  </Students>
</Teacher>";

        var parser = new ExamXmlParser();
        var teacher = parser.Parse(xml);

        Assert.AreEqual("11111", teacher.Id);
        Assert.AreEqual(1, teacher.Students.Count);
    }
}