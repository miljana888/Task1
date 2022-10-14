using NotesApplication.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NotesApplication.Models
{
    public class Note
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string Text { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
      //  [System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "text(24)")]
        public Color color { get; set; }

       
    }
   
}
