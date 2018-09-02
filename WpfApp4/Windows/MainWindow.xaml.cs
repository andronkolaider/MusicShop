using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;

namespace WpfApp4
{

    public partial class MainWindow : MetroWindow
    {
        Album newAlbum;
        public List<Album> albumList = new List<Album>();
        bool songsExist = false;
        public MainWindow()
        {
            InitializeComponent();
            MusicContext db = new MusicContext();
            albumList = db.Albums.ToList();
            musicListView.ItemsSource = db.Albums.Include("Songs").ToList();
            comboBoxGenres.ItemsSource = db.Genres.ToList();
            authorComboBox.ItemsSource = db.Authors.ToList();
            genreComboBox.ItemsSource = db.Genres.ToList();
        }

        private void comboBoxGenres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            musicListView.ItemsSource = albumList;
            List<Album> tempList = new List<Album>();
            foreach (Album item in musicListView.Items)
            {
                if (comboBoxGenres.SelectedValue == item.Genre)
                {
                    tempList.Add(item);
                }
            }
            musicListView.ItemsSource = tempList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            flyOutEditDB.IsOpen = true;
            newAlbum = new Album();
        }

        private void comboBoxGenres_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            (sender as System.Windows.Controls.ComboBox).IsDropDownOpen = false;
            (sender as System.Windows.Controls.ComboBox).SelectedIndex = -1;
            musicListView.ItemsSource = albumList;
        }

        private void selectImageButton_Click(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog image = new OpenFileDialog())
            {
                image.ShowDialog();
                if (image.FileName != null && image.FileName.Contains(".jpg"))
                {
                    newAlbum.Image = image.FileName;
                }
                else if (image.FileName.Contains(".jpg") == false)
                {
                    this.ShowMessageAsync("Alert", "Image has to be in JPEG format", MessageDialogStyle.Affirmative);
                }
            }
        }



        private void datePublishedButton_Click(object sender, RoutedEventArgs e)
        {
            flyOutEditDB.IsOpen = false;
            calendarFlyout.IsOpen = true;
        }

        private void buttonDateConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (datePublishCalendar.SelectedDate != null)
            {
                newAlbum.DatePublished = datePublishCalendar.SelectedDate.Value;
            }
            else
            {
                this.ShowMessageAsync("Alert", "Please select date", MessageDialogStyle.Affirmative);
            }
            calendarFlyout.IsOpen = false;
            flyOutEditDB.IsOpen = true;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
           if (titleTextBox.Text != "" && urlTextBox.Text != "" && genreComboBox.SelectedIndex != -1 && authorComboBox.SelectedIndex != -1 && lengthTextBox.Text != "" && datePublishCalendar.SelectedDate != null && newAlbum.Image != null && songsExist==true)
            {
                using (MusicContext db = new MusicContext())
                {
                    SecondsToMinutesConverter converter = new SecondsToMinutesConverter();
                    newAlbum.Title = titleTextBox.Text;
                    string authName = (authorComboBox.SelectedItem as Author).Name.ToString();
                    string authGenre = (genreComboBox.SelectedItem as Genre).Name;
                     newAlbum.Author = db.Authors.FirstOrDefault(x=> x.Name == authName);
                    newAlbum.Genre = db.Genres.FirstOrDefault(x => x.Name == authGenre);
                    newAlbum.Length = StringToSeconds(lengthTextBox.Text);
                    newAlbum.Price = decimal.Parse(priceTextBox.Text);
                    newAlbum.ShopUrl = urlTextBox.Text;
                }
                using (MusicContext db = new MusicContext())
                {
                    db.Albums.Add(newAlbum);
                    db.SaveChanges();
                    songsExist = false;
                    this.ShowMessageAsync("Alert", "Song successfully added");
                }
            }
        }

        private void openAddSongsWondow_Click(object sender, RoutedEventArgs e)
        {
            addSongsWindow window = new addSongsWindow();
            if (window.ShowDialog() == true)
            {
                songsExist = true;
                newAlbum.Songs = window.albumView.Songs.ToList();
            }
            else
            {
                songsExist = false;
            }
        }
        public int StringToSeconds(string s)
        {
            List<string> time=s.Split(':').ToList();
            int result = int.Parse(time[0])*60;
            result += int.Parse(time[1]);
            return result;
        }
    }
}
