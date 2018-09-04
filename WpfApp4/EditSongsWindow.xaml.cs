using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>


    public partial class EditSongsWindow : MetroWindow
    {
        public ObservableCollection<Song> songs = new ObservableCollection<Song>();
        Album editableAlbum;
        public EditSongsWindow(Album album)
        {
            InitializeComponent();
            editableAlbum = album;

            if (editableAlbum.Songs != null)
            {
                foreach (var item in editableAlbum.Songs)
                {
                    songs.Add(item);
                }
            }
            SongsListView.ItemsSource = songs;
        }

        private void ButtonAddSong_Click(object sender, RoutedEventArgs e)
        {
            songs.Add(new Song());
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (songs.Count == 0)
            {
                editableAlbum.Songs.Clear();
                //   songs.Clear();
            }
            else
            {
              
                 editableAlbum.Songs = songs.ToList();
            }

            this.Close();
        }

        private void ButtonRemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            if (SongsListView.SelectedIndex != -1)
            {
                songs.Remove(SongsListView.SelectedItem as Song);
            }
        }
    }
}
