namespace QuizProjectFE.Models.DTO
{
    public class QuestionCreate
    {
        public string QuestionTopic { get; set; }
        public string QuestionText { get; set; }
        public string QuestionImg { get; set; }
        public int QuizId { get; set; }
    }
}
