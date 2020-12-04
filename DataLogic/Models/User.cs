using System.ComponentModel.DataAnnotations;

namespace DataLogic.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string email { get; set; }
    }
}
