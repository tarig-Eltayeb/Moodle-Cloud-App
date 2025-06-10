using Microsoft.AspNetCore.Mvc;
using MoodleTrueFalse.Api.Models;    // لاستخدام QuestionData
using MoodleTrueFalse.Api.Services;  // لاستخدام XmlGenerationService
using System.Text;

namespace MoodleTrueFalse.Api.Controllers
{
    [ApiController]
    [Route("api/questions")] // هذا هو المسار الذي ستصل به الواجهة الأمامية للخادم
    public class QuestionsController : ControllerBase
    {
        private readonly XmlGenerationService _xmlService;

        // هذه الطريقة (Dependency Injection) تسمح لـ ASP.NET بتزويدنا بنسخة من الخدمة تلقائياً
        public QuestionsController(XmlGenerationService xmlService)
        {
            _xmlService = xmlService;
        }

        [HttpPost("generate-xml")] // سيتم الوصول لهذه الوظيفة عبر POST /api/questions/generate-xml
        public IActionResult GenerateXmlFile([FromBody] List<QuestionData> questions)
        {
            // 1. التحقق من المدخلات
            if (questions == null || questions.Count == 0)
            {
                return BadRequest("No questions were provided."); // إرجاع خطأ إذا كانت القائمة فارغة
            }

            // 2. استدعاء الخدمة لإنشاء مستند XML
            var xmlDoc = _xmlService.GenerateMoodleXml(questions);

            // 3. تحويل مستند XML إلى بايتات ليتم إرسالها كملف
            var memoryStream = new MemoryStream();
            xmlDoc.Save(memoryStream);
            memoryStream.Position = 0; // إعادة المؤشر إلى بداية الـ stream

            // 4. إرجاع الملف للمستخدم
            // نخبر المتصفح أن هذا ملف XML واسمه "moodle-quiz.xml"
            return File(memoryStream, "application/xml", "moodle-quiz.xml");
        }
    }
}