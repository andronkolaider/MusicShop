using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
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
            using (MusicContext db = new MusicContext())
            {
                albumList = db.Albums.ToList();
                musicListView.ItemsSource = db.Albums.Include("Songs").ToList();
                authorComboBox.ItemsSource = db.Authors.ToList();
                genreComboBox.ItemsSource = db.Genres.ToList();
            }

        }

        private void ComboBoxSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            musicListView.ItemsSource = albumList;
            List<Album> tempList = new List<Album>();
            foreach (Album item in musicListView.Items)
            {          
                if (comboBoxSearch.SelectedItem is Author)
                {
                    if ((comboBoxSearch.SelectedItem as Author).Id == item.Author.Id)
                    {
                        tempList.Add(item);
                    }
                }
                else if (comboBoxSearch.SelectedItem is Genre)
                {
                    if ((comboBoxSearch.SelectedItem as Genre).Id == item.Genre.Id)
                    {
                        tempList.Add(item);
                    }
                }
                else if (comboBoxSearch.SelectedItem is Album)
                {
                   
                    if ((comboBoxSearch.SelectedItem as Album).Title == item.Title)
                    {
                        tempList.Add(item);
                    }
                }
            }
            musicListView.ItemsSource = tempList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            flyOutEditDB.IsOpen = true;
            newAlbum = new Album();
        }

        private void ComboBoxSearch_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            (sender as System.Windows.Controls.ComboBox).IsDropDownOpen = false;
            (sender as System.Windows.Controls.ComboBox).SelectedIndex = -1;
            musicListView.ItemsSource = albumList;
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
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
                    this.ShowMessageAsync("Alert", "Image has to be in JPEG format");
                }
            }
        }

        private void DatePublishedButton_Click(object sender, RoutedEventArgs e)
        {
            flyOutEditDB.IsOpen = false;
            calendarFlyout.IsOpen = true;
        }

        private void ButtonDateConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (datePublishCalendar.SelectedDate != null)
            {
                newAlbum.DatePublished = datePublishCalendar.SelectedDate.Value;
            }
            else
            {
                this.ShowMessageAsync("Alert", "Please select date");
            }
            calendarFlyout.IsOpen = false;
            flyOutEditDB.IsOpen = true;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (titleTextBox.Text != "" && urlTextBox.Text != "" && genreComboBox.SelectedIndex != -1 && authorComboBox.SelectedIndex != -1 && lengthTextBox.Text != "" && datePublishCalendar.SelectedDate != null && newAlbum.Image != null && songsExist == true)
            {
                using (MusicContext db = new MusicContext())
                {
                    newAlbum.Title = titleTextBox.Text;
                    string authName = (authorComboBox.SelectedItem as Author).Name.ToString();
                    string authGenre = (genreComboBox.SelectedItem as Genre).Name;
                    newAlbum.Author = db.Authors.FirstOrDefault(x => x.Name == authName);
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

        private void OpenAddSongsWondow_Click(object sender, RoutedEventArgs e)
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
            List<string> time = s.Split(':').ToList();
            int result = int.Parse(time[0]) * 60;
            result += int.Parse(time[1]);
            return result;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (MusicContext db = new MusicContext())
            {
                albumList = db.Albums.ToList();
                musicListView.ItemsSource = db.Albums.Include("Songs").ToList();
                comboBoxSearch.ItemsSource = db.Genres.ToList();
                authorComboBox.ItemsSource = db.Authors.ToList();
                genreComboBox.ItemsSource = db.Genres.ToList();
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            using (MusicContext db = new MusicContext())
            {
                if ((sender as System.Windows.Controls.RadioButton).Tag.ToString() == "Author")
                {
                    comboBoxSearch.ItemsSource = db.Authors.ToList();
                    comboBoxSearch.DisplayMemberPath = "Name";
                }
                else if ((sender as System.Windows.Controls.RadioButton).Tag.ToString() == "Genre")
                {
                    comboBoxSearch.ItemsSource = db.Genres.ToList();
                    comboBoxSearch.DisplayMemberPath = "Name";
                }
                else if ((sender as System.Windows.Controls.RadioButton).Tag.ToString() == "Title")
                {
                    comboBoxSearch.ItemsSource = db.Albums.ToList();
                    comboBoxSearch.DisplayMemberPath = "Title";
                }
            }
        }

        private void DatePublishCalendar_GotFocus(object sender, RoutedEventArgs e)
        {
            buttonDateConfirm.Focus();
        }
    }
}
