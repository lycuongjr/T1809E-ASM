using asm_T1809E.Models;
using asm_T1809E.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace asm_T1809E
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TestMusic : Page
    {
        private bool _isPlaying = false; 

        private int _currentIndex = 0;
        private Song currentSong;
        private string CurrentTag = "";
        private string currentUploadUrl;
        private StorageFile fileMp3;
        private IMySongService _service;

        private ObservableCollection<Song> listSong;

        internal ObservableCollection<Song> ListSong { get => listSong; set => listSong = value; }


        public TestMusic()
        {
            this.ListSong = new ObservableCollection<Song>();
            this.ListSong.Add(new Song()
            {
                Name = "Vệt Nắng Cuối Trời",
                Singer = "Hoàng Bách",
                Thumbnail = "http://nguoi-noi-tieng.com/photo/tieu-su-ca-si-nhac-si-hoang-bach-9979.jpg",
                Link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui154/VetNangCuoiTroi-HoangBach_3553y.mp3"
            });
            this.ListSong.Add(new Song()
            {
                Name = "Phía sau một cô gái",
                Singer = "Sobin Hoàng Sơn",
                Thumbnail = "http://phapluatnet.vn/Uploads/Originals/2017/5/28/20170528172129_sobin-hoang-son.jpg",
                Link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui929/PhiaSauMotCoGaiBeat-SoobinHoangSon-4632332.mp3"
            });
            this.ListSong.Add(new Song()
            {
                Name = "Đường về Thanh Hóa",
                Singer = "Lý Cường Singer",
                Thumbnail = "https://scontent.fhan5-5.fna.fbcdn.net/v/t1.0-9/42173481_708980126134329_1523122255020687360_n.jpg?_nc_cat=108&oh=b296def90a90e8fcbb62c5d952109ca5&oe=5C552AB8",
                Link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui794/DuongVeThanhHoa-AnhTho_4ebup.mp3"
            });
            this.ListSong.Add(new Song()
            {
                Name = "Về Quê Ngoại",
                Singer = "Quang Lê",
                Thumbnail = "https://avatar-nct.nixcdn.com/singer/avatar/2017/02/06/3/4/7/f/1486358917132_600.jpg",
                Link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui211/VeQueNgoai-QuangLe_3vg4w.mp3?st=LU5TMv6erWZEaOwGIa7bYg&e=1540064634&download=true"
            });
            this.ListSong.Add(new Song()
            {
                Name = "Trộm nhìn nhau",
                Singer = "Hoài Lâm",
                Thumbnail = "https://upload.wikimedia.org/wikipedia/commons/c/c0/Ch%C3%A2n_dung_Ho%C3%A0i_L%C3%A2m.jpg",
                Link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui934/TromNhinNhau-HoaiLam-4677141.mp3"
            });
            this.ListSong.Add(new Song()
            {
                Name = "Nhớ mãi một miền quê",
                Singer = "Anh Thơ",
                Thumbnail = "https://anh.24h.com.vn/upload/4-2014/images/2014-10-13/1413196089-anhtho.jpg",
                Link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui969/HongKong1-NguyenTrongTai-5663439_hq.mp3"
            });
            this.InitializeComponent();
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Song song = new Song()
            {
                Name = txt_SongName.Text,
                Thumbnail = txt_Thumbnail.Text,
                Singer = txt_Singer.Text,
                Link = txt_Link.Text
            };
            this.ListSong.Add(song);
            txt_SongName.Text = string.Empty;
            txt_Thumbnail.Text = string.Empty;
            txt_Singer.Text = string.Empty;
            txt_Link.Text = string.Empty;
            
        }
       
        private void Choosed_Song(object sender, TappedRoutedEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            Song choosed = panel.Tag as Song;
            _currentIndex = this.MyListSong.SelectedIndex;
            Uri mp3Link = new Uri(choosed.Link);
            MyPlayer.Source = mp3Link;           
            Song_Name.Text = this.ListSong[_currentIndex].Name + " - " + ListSong[_currentIndex].Singer;
            
            //Play_Song();
        }
        private void PlayButton_Clicked(object sender, RoutedEventArgs e)
        {

            if (MyListSong.ItemsSource == null) return;
            if (currentSong == null)
            {
                MyListSong.SelectedIndex = 0;
            }

            if (_isPlaying)
            {
                MyPlayer.Pause();
                PlayButton.Icon = new SymbolIcon(Symbol.Play);
                Player_Status.Text = "Paused";
                SongPlaying.Source = new BitmapImage(new Uri(ListSong[_currentIndex].Thumbnail));
                _isPlaying = false;
            }
            else
            {
                MyPlayer.Play();
                PlayButton.Icon = new SymbolIcon(Symbol.Pause);
                Player_Status.Text = "Now Playing:";
                SongPlaying.Source = new BitmapImage(new Uri(ListSong[_currentIndex].Thumbnail));
                _isPlaying = true;
            }


        }
            private void Next_OnClick(object sender, RoutedEventArgs e)
        {
            
            _currentIndex += 1;
            if (_currentIndex >= this.ListSong.Count)
            {
                _currentIndex = 0;
            }
            Uri mp3Link = new Uri(this.ListSong[_currentIndex].Link);
            this.MyPlayer.Source = mp3Link;
            this.Song_Name.Text = this.ListSong[_currentIndex].Name + " - " + this.ListSong[_currentIndex].Singer;
            SongPlaying.Source = new BitmapImage(new Uri(ListSong[_currentIndex].Thumbnail));
            this.MyListSong.SelectedIndex = _currentIndex;
            
        }

        private void Previous_OnClick(object sender, RoutedEventArgs e)
        {
            _currentIndex -= 1;
            if (_currentIndex < 0)
            {
                _currentIndex = this.ListSong.Count - 1;
            }
            Uri mp3Link = new Uri(this.ListSong[_currentIndex].Link);
            this.MyPlayer.Source = mp3Link;
            this.Song_Name.Text = this.ListSong[_currentIndex].Name + " - " + this.ListSong[_currentIndex].Singer;
            SongPlaying.Source = new BitmapImage(new Uri(ListSong[_currentIndex].Thumbnail));
            this.MyListSong.SelectedIndex = _currentIndex;   
        }
        private void AppbarButton_Click(object sender, RoutedEventArgs e)
        {
            AppBarButton appBar = sender as AppBarButton;
            if (CurrentTag == appBar.Tag.ToString())
            {
                return;
            }
            switch (appBar.Tag.ToString())
            {

                case "Home":
                    CurrentTag = "Home";
                    MyFrame.Navigate(typeof(MainPage));
                    break;
                case "Audio":
                    CurrentTag = "Audio";
                    MySong.Navigate(typeof(MySong));
                    break;
                case "Register":
                    CurrentTag = "Register";
                    Frame.Navigate(typeof(Pages.RegisterForm));
                    break;
                case "Add":
                    CurrentTag = "Add";
                    CreateSong.Navigate(typeof(MySong));
                    break;             
                default:
                    break;
            }
           
        }
        private void Submit_Click_MySong(object sender, RoutedEventArgs e)
        {

            MySong mySong = new MySong
            {
                Name = MySongName.Text,
                Url = MyLink.Text,
                Message = Message.Text,
                Thumbnail = MyThumbnail.Text

            };
            _service.Create(mySong);

        }
        private async void Choose_File(object sender, RoutedEventArgs e)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
            //// Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".mp3" });
            //// Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document";
            var file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                FileHandleService.WriteToFile(file, "Hello World");
            }

            HttpClient httpClient = new HttpClient();
            currentUploadUrl = await httpClient.GetStringAsync("https://2-dot-backup-server-002.appspot.com/get-upload-token");
            Debug.WriteLine("Upload url:" + currentUploadUrl);
            HttpUploadFile(currentUploadUrl, "myFile", "music/mp3");


        }
        public async void HttpUploadFile(string url, string paramName, string contentType)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";

            Stream rs = await wr.GetRequestStreamAsync();
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", paramName, "path_file", contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            // write file.
            Stream fileStream = await fileMp3.OpenStreamForReadAsync();
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);

            WebResponse wresp = null;
            try
            {
                wresp = await wr.GetResponseAsync();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                //Debug.WriteLine(string.Format("File uploaded, server response is: @{0}@", reader2.ReadToEnd()));
                //string imgUrl = reader2.ReadToEnd();
                //Uri u = new Uri(reader2.ReadToEnd(), UriKind.Absolute);
                //Debug.WriteLine(u.AbsoluteUri);
                //ImageUrl.Text = u.AbsoluteUri;
                //MyAvatar.Source = new BitmapImage(u);
                //Debug.WriteLine(reader2.ReadToEnd());
                string UrlFile = reader2.ReadToEnd();
                MyLink.Text = UrlFile;
                Debug.WriteLine(UrlFile);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error uploading file", ex.StackTrace);
                Debug.WriteLine("Error uploading file", ex.InnerException);
                if (wresp != null)
                {
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }
    }
}

