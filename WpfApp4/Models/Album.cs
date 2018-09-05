using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp4
{
    public class Album
    {
        public int Id { get; set; }
        [Required]
        public virtual Author Author { get; set; }                 //???
        [Required]
        public string Title { get; set; }
        [Required]
        public int Length { get; set; }
        public string Image { get; set; }
        [Required]
        public virtual Genre Genre { get; set; }
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DatePublished { get; set; }
        public string ShopUrl { get; set; }
        [Required]
        public virtual List<Song> Songs { get; set; }
    }
}
