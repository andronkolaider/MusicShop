using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for addSongsWindow.xaml
    /// </summary>
    /// 
    public partial class addSongsWindow : MetroWindow
    {
        public AlbumViewModel albumView;

        public addSongsWindow()
        {
            InitializeComponent();
            this.DataContext = albumView;
        }

        private void ButtonAddSong_Click(object sender, RoutedEventArgs e)
        {
            albumView.Songs.Add(new Song());
        }

        private void ButtonDeleteSong_Click(object sender, RoutedEventArgs e)
        {
            if (SongsListView.Items.Count > 0)
            {
                albumView.Songs.RemoveAt(albumView.Songs.Count - 1);
            }
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            bool OK = false;
            if (SongsListView.Items.Count > 0)
            {
                foreach (Song item in SongsListView.Items)
                {
                    if (item.Length != -1)
                    {
                        OK = true;
                    }
                    else
                    {
                        this.DialogResult = false;
                        break;
                    }
                }
                if (OK == true)
                {
                    this.DialogResult = true;
                    this.Close();
                }
            }
            else
            {
                this.ShowMessageAsync("Alert", "Album needs to have at least one song");
            }
        }

        public int StringToSeconds(string s)
        {
            if (s.Contains(':') || s.Contains(',') || s.Contains('.'))
            {
                List<string> time = s.Split(':').ToList();
                int result = int.Parse(time[0]) * 60;
                result += int.Parse(time[1]);
                this.ShowMessageAsync("", result.ToString());
                return result;
            }
            else
            {
                this.ShowMessageAsync("Error", "Wrong time format");
                return -1;
            }
        }
         
    }
}
