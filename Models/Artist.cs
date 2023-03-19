using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
    }
}