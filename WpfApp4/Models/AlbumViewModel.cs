using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
    public class AlbumViewModel
    {
        public ObservableCollection<Song> Songs { get; set; }
       
        public AlbumViewModel()
        {
            Songs = new ObservableCollection<Song>();
        }

    }
}
