using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Diagnostics;
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
    class SongComparer : IEqualityComparer<Song>
    {
        public bool Equals(Song x, Song y)
        {
            if (x.Id == y.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(Song obj)
        {
            return obj.GetHashCode();
        }
    }
    public partial class EditAlbumWindow : MetroWindow
    {
        Album editableAlbum;
        public EditAlbumWindow(int ID)
        {
            InitializeComponent();
            using (MusicContext db = new MusicContext())
            {
                editableAlbum = db.Albums.Include("Songs").FirstOrDefault(x => x.Id == ID);
                AuthorsComboBox.ItemsSource = db.Authors.ToList();
                GenreComboBox.ItemsSource = db.Genres.ToList();
            }
            AuthorsComboBox.SelectedItem = editableAlbum.Author;
            TitleTextBox.Text = editableAlbum.Title;
            LengthtextBox.Text = SecondsToString(editableAlbum.Length);
            AlbumImage.Source = new BitmapImage(new Uri(editableAlbum.Image));
            GenreComboBox.SelectedItem = editableAlbum.Genre;
            PriceTextBox.Text = Math.Round(editableAlbum.Price, 2).ToString();
            DatePublishedCalendar.DisplayDate = editableAlbum.DatePublished;
            oldDatePublished.Content = "Previous date: " + editableAlbum.DatePublished.Date.ToString().Split(' ')[0];
            URLTextBox.Text = editableAlbum.ShopUrl;
        }

        public string SecondsToString(int seconds)
        {
            string stringResult = TimeSpan.FromSeconds(seconds).ToString();
            return stringResult.Remove(0, 3);
        }

        private void ButtonChangeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                editableAlbum.Image = fileDialog.FileName;
                AlbumImage.Source = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void ButtonOpenURL_Click(object sender, RoutedEventArgs e)
        {
            if (URLTextBox.Text != "")
            {
                Process.Start("chrome.exe", URLTextBox.Text);
            }
        }

        private void EditAlbumSongs_Click(object sender, RoutedEventArgs e)
        {
            EditSongsWindow window = new EditSongsWindow(editableAlbum);
            window.ShowDialog();
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            using (MusicContext db = new MusicContext())
            {
                Album album = db.Albums.Find(editableAlbum.Id);
                if (album != null)
                {
                    if (editableAlbum.Songs.Count == 0)
                    {
                        album.Songs.Clear();
                    }
                    else
                    {
                        album.Songs.Clear();
                        album.Songs = editableAlbum.Songs;
                    }
                }
                db.SaveChanges();
            }
            this.Close();
        }
    }
}
