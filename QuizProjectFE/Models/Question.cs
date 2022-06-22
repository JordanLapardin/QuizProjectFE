using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizProjectFE.Models
{
    public class Question
    {
        [Display(Name = "Question ID")]
        public int QuestionId { get; set; }
        [Display(Name = "Question Topic")]
        public string QuestionTopic { get; set; }
        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }
        [Display(Name = "Question Image")]
        public string QuestionImg { get; set; }
        [Display(Name = "Quiz ID")]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public ICollection<Option> Options { get; set; }
    }
}
