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
    public partial class WindowEditAlbum : MetroWindow
    {
        Album editableAlbum;
        public WindowEditAlbum(int ID)
        {
            InitializeComponent();
            using (MusicContext db = new MusicContext())
            {
                editableAlbum = db.Albums.FirstOrDefault(x => x.Id == ID);
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
            addSongsWindow addSongs = new addSongsWindow(editableAlbum);
            if (addSongs.ShowDialog() == true)
            {
                editableAlbum.Songs = addSongs.albumView.Songs.ToList();
            }
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            using (MusicContext db = new MusicContext())
            {
                 Album album = db.Albums.Find(editableAlbum.Id);
                db.Albums.AddOrUpdate(editableAlbum);
                //db.Albums.FirstOrDefault(x => x.Id == editableAlbum.Id).Author = editableAlbum.Author;
                //db.Albums.FirstOrDefault(x => x.Id == editableAlbum.Id).DatePublished = editableAlbum.DatePublished;
                //db.Albums.FirstOrDefault(x => x.Id == editableAlbum.Id).Genre = editableAlbum.Genre;
                //db.Albums.FirstOrDefault(x => x.Id == editableAlbum.Id).Image = editableAlbum.Image;
                //db.Albums.FirstOrDefault(x => x.Id == editableAlbum.Id).Length = editableAlbum.Length;
                //db.Albums.FirstOrDefault(x => x.Id == editableAlbum.Id).Price = editableAlbum.Price;
                //db.Albums.FirstOrDefault(x => x.Id == editableAlbum.Id).ShopUrl = editableAlbum.ShopUrl;
                //db.Albums.FirstOrDefault(x => x.Id == editableAlbum.Id).Title = editableAlbum.Title;
                //db.Albums.FirstOrDefault(x => x.Id == editableAlbum.Id).Songs = editableAlbum.Songs; //!!!!!!!!!!!!
              db.Entry(album).CurrentValues.SetValues(editableAlbum);
                db.SaveChanges();
               
            }

            this.Close();
        }
    }
}
