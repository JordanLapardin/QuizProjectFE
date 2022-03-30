namespace QuizProjectFE.Models.DTO
{
    public class OptionCreate
    {
        public string OptionText { get; set; }
        public string OptionLetter { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
