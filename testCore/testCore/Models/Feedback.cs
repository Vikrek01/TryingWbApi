using System.ComponentModel.DataAnnotations;

namespace testCore.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string FeedBackDescription { get; set; }=string.Empty;
        public string FeedBackBy { get; set; } = string.Empty;
    }
}
