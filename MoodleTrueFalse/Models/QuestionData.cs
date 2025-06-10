// داخل ملف QuestionData.cs

namespace MoodleTrueFalse.Api.Models
{
    // هذا هو نفس تعريف السؤال من برنامجك القديم
    // سيستخدمه الخادم لاستقبال وفهم بيانات الأسئلة
    public class QuestionData
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsTrue { get; set; }
    }
}