using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WpfApp4
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual List<Album> Albums { get; set; }
    }
}
