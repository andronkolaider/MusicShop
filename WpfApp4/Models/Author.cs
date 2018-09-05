using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WpfApp4
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<Album> Albums { get; set; }
    }
}
