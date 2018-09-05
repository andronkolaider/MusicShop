using System.Collections.ObjectModel;

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
