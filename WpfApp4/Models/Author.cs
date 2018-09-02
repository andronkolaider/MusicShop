using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
