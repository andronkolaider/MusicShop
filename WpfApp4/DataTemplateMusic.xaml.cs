using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp4
{
    partial class DataTemplateMusic : ResourceDictionary
    {
        public DataTemplateMusic()
        {
            InitializeComponent();
        }

        private void BaiButtonClick(object sender, RoutedEventArgs e)
        {
            int ID = Convert.ToInt32((sender as Button).Tag);
            using (MusicContext db = new MusicContext())
            {
                var a = db.Albums.Find(ID);           
                if(a!=null)
                {
                    Process.Start("chrome.exe", a.ShopUrl);
                }
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            int ID = Convert.ToInt32((sender as Button).Tag);
            WindowEditAlbum window = new WindowEditAlbum(ID);
            window.ShowDialog();         
              
        }
    }
}
