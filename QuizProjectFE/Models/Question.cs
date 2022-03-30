using System.Collections.Generic;

namespace QuizProjectFE.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionTopic { get; set; }
        public string QuestionText { get; set; }
        public string QuestionImg { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public ICollection<Option> Options { get; set; }
    }
}
