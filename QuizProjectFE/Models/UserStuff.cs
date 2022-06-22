using System.ComponentModel.DataAnnotations;

namespace QuizProjectFE.Models
{
    public class UserStuff
    {
        //public int UserStuffID { get; set; }
        public string UserName { get; set; }
        [Display(Name = "Password")]
        public string UserPassword { get; set; }
    }
}
