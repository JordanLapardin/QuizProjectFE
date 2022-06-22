using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizProjectFE.Models
{
    public class Quiz
    {
        // QuizID is used to collect the quiz entry and in the code is used to both collect the entry and make new entries 
        [Display(Name="Quiz ID")]
        public int QuizId { get; set; }
        // QuizTitle is used similar to the QuizID and just like it used to create and collect but its done more in a bundle 
        // for example the database prevents the creation of entries without the ID and will auto generate one if missing
        [Display(Name = "Quiz Title")]
        public string QuizTitle { get; set; }
        // QuizTopic is the exact same as Quiztitle 
        [Display(Name = "Quiz Topic")]
        public string QuizTopic { get; set; }
        //// CreatorName is the exact same as QuizTopic 
        [Display(Name = "Creator Name")]
        public string CreatorName { get; set; }
        //// PassP is the exact same as CreatorName 
        [Display(Name = "Pass Precentage")]
        public float PassP { get; set; }
        //public ICollection<Question> Questions { get; set; }
    }
}
