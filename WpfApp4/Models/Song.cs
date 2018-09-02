using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
