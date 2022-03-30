namespace QuizProjectFE.Models
{
    public class Option
    {
        public int OptionID { get; set; }
        public string OptionText { get; set; }
        public string OptionLetter { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
