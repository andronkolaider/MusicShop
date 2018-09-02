namespace WpfApp4
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class MusicContext : DbContext
    {

        public MusicContext()
            : base("name=connString")
        {
        }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
    }
}