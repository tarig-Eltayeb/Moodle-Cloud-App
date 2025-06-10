// داخل ملف XmlGenerationService.cs

using System.Reflection;
using System.Xml.Linq; // لا تنسَ إضافة هذا السطر

namespace MoodleTrueFalse.Api.Services
{
    // هذه الفئة ستحتوي على كل المنطق الخاص بإنشاء ملف XML
    public class XmlGenerationService
    {
        // لاحظ أننا نمرر البيانات كمعامل (parameter) بدلاً من قراءتها من واجهة المستخدم
        public XDocument GenerateMoodleXml(List<Models.QuestionData> questions)
        {
            var doc = new XDocument(new XElement("quiz"));

            foreach (var q in questions)
            {
                doc.Root!.Add(
                  new XElement("question", new XAttribute("type", "truefalse"),
                    new XElement("name", new XElement("text", q.Name)),
                    new XElement("questiontext", new XAttribute("format", "html"),
                      new XElement("text",
                        new XCData($"<p dir=\"rtl\" style=\"text-align:right;\">{q.Text}</p>"))),

                    // هذا هو نفس المنطق الصحيح من برنامجك النهائي
                    new XElement("answer",
                      new XAttribute("fraction", q.IsTrue ? "100" : "0"),
                      new XElement("text", "true")
                    ),
                    new XElement("answer",
                      new XAttribute("fraction", !q.IsTrue ? "100" : "0"),
                      new XElement("text", "false")
                    ),

                    new XElement("defaultgrade", "1.0"),
                    new XElement("penalty", "1.0")));
            }

            return doc;
        }
    }
}