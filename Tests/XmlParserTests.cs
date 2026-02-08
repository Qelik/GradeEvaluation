using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class XmlParserTests
{
    [TestMethod]
    public void Parse_ValidXml_ReturnsTeacherWithStudents()
    {
        string xml = @"
<Teachers>
    <Teacher ID = ""11111"">
        <Students>
            <Student ID=""1111"">
                <Exam Id=""1"">
                    <Task id = ""1""> 2+3/6-4 = 74 </Task >
                    <Task id = ""2""> 6*2+3-4 = 22 </Task >
                </Exam>
            </Student>
            <Student ID=""1112"">
                <Exam Id=""2"">
                    <Task id = ""1""> 2+3/6-4 = 74 </Task >
                    <Task id = ""2""> 6*2+3-4 = 11 </Task >
                </Exam>
            </Student>
            <Student ID=""1113"">
                <Exam Id=""3"">
                    <Task id = ""1""> 2+3/6-4 = -1.5 </Task >
                    <Task id = ""2""> 6*2+3-4 = 11 </Task >
                </Exam>
            </Student>
        </Students >
    </Teacher>
    <Teacher ID = ""22222"">
        <Students>
            <Student ID=""1111"">
                <Exam Id=""1"">
                    <Task id = ""1""> 2+3/6-4 = 74 </Task >
                    <Task id = ""2""> 6*2+3-4 = 22 </Task >
                </Exam>
            </Student>
            <Student ID=""1112"">
                <Exam Id=""2"">
                    <Task id = ""1""> 2+3/6-4 = 74 </Task >
                    <Task id = ""2""> 6*2+3-4 = 11 </Task >
                </Exam>
            </Student>
            <Student ID=""1113"">
                <Exam Id=""3"">
                    <Task id = ""1""> 2+3/6-4 = -1.5 </Task >
                    <Task id = ""2""> 6*2+3-4 = 11 </Task >
                </Exam>
            </Student>
        </Students >
    </Teacher>
</Teachers>";

        var parser = new ExamXmlParser();
        // parser.Parse requires the logged-in teacher id -> pass 11111
        var teacher = parser.Parse(xml, 11111);

        Assert.IsNotNull(teacher);
        Assert.AreEqual(11111, teacher.TeacherId);
        Assert.IsNotNull(teacher.Students);
        Assert.AreEqual(3, teacher.Students.Count);
    }
}