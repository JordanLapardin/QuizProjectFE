using System.ComponentModel.DataAnnotations;

namespace QuizProjectFE.Models
{
    public class Option
    {
        [Display(Name = "Option ID")]
        public int OptionID { get; set; }
        [Display(Name = "Option Text")]
        public string OptionText { get; set; }
        [Display(Name = "Option Letter")]
        public string OptionLetter { get; set; }
        [Display(Name = "Is Correct or Not")]
        public bool IsCorrect { get; set; }
        [Display(Name = "Question ID")]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
