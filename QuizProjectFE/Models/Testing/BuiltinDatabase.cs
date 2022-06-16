using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizProjectFE.Models.Testing
{
    public class BuiltinDatabase
    {
        public List<Quiz> Quizzes { get; set; }

        public List<Question> questions { get; set; }

        public List<Option> Options { get; set; }

        public List<UserStuff> userStuffs { get; set; }

        public BuiltinDatabase()
        {
            Quizzes = new List<Quiz>();
            questions = new List<Question>();
            Options = new List<Option>();
            userStuffs = new List<UserStuff>();
        }
    }
}
