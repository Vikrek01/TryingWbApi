using System.ComponentModel.DataAnnotations;

namespace testCore.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Department { get; set; } = string.Empty;
    }
      
}
