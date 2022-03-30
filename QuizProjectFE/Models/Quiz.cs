using System.Collections.Generic;

namespace QuizProjectFE.Models
{
    public class Quiz
    {
        public int QuizId { get; set; }

        public string QuizTitle { get; set; }

        public string QuizTopic { get; set; }

        public string CreatorName { get; set; }
        public float PassP { get; set; }
        //public ICollection<Question> Questions { get; set; }
    }
}
