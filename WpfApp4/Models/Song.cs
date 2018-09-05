using System.ComponentModel.DataAnnotations;

namespace WpfApp4
{
    public class Song
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Length { get; set; }
    }
}
