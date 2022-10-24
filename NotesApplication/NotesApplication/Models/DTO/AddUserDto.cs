using System.ComponentModel.DataAnnotations;

namespace NotesApplication.Models.DTO
{
    public class AddUserDto
    {
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
