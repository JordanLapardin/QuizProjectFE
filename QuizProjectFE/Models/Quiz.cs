using System.Collections.Generic;

namespace QuizProjectFE.Models
{
    public class Quiz
    {
        // QuizID is used to collect the quiz entry and in the code is used to both collect the entry and make new entries 
        public int QuizId { get; set; }
        // QuizTitle is used similar to the QuizID and just like it used to create and collect but its done more in a bundle 
        // for example the database prevents the creation of entries without the ID and will auto generate one if missing
        public string QuizTitle { get; set; }
        // QuizTopic is the exact same as Quiztitle 
        public string QuizTopic { get; set; }
        //// CreatorName is the exact same as QuizTopic 
        public string CreatorName { get; set; }
        //// PassP is the exact same as CreatorName 
        public float PassP { get; set; }
        //public ICollection<Question> Questions { get; set; }
    }
}
