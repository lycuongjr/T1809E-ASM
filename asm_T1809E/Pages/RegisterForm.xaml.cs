using asm_T1809E.Models;
using asm_T1809E.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
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

namespace asm_T1809E.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterForm : Page
    {
        private IMemberService _service;
        private int _choosedGender = 2;
        private string currentUploadUrl;
        private StorageFile photo;

        public RegisterForm()
        {
            this.InitializeComponent();
            this._service = new ApiMemberService();

        }
        private void doReset_Click(object sender, RoutedEventArgs e)
        {

            this.t_firstName.Text = string.Empty;
            this.t_lastName.Text = string.Empty;
            this.t_phone.Text = string.Empty;
            this.t_email.Text = string.Empty;
            this.t_passWord.Password = string.Empty;
            this.t_avatarUrl.Text = string.Empty;
            this.t_address.Text = string.Empty;



        }

        private void doSubmit_Click(object sender, RoutedEventArgs e)
        {
            // lay data tu` form, chuyen thanh` doi tuong member tuong ung'
            var member = new Member()
            {
                firstName = t_firstName.Text,
                lastName = t_lastName.Text,
                email = t_email.Text,
                password = t_passWord.Password,
                address = t_address.Text,
                avatar = t_avatarUrl.Text,
                gender = _choosedGender,
                phone = t_phone.Text,


            };
            // validate data

            var errors = member.CheckValidate();
            if (errors.Count > 0)
            {
                // thông báo lỗi nếu có.
            }
         
            Debug.WriteLine("Create success! " + member.id);
            _service.Create(member); 
        }
    
        private void Gender_Choose(object sender, RoutedEventArgs e)
            {
                var chooseRadioButton = (RadioButton)sender;
            if (chooseRadioButton == null)
            {
                return;
            }
            switch (chooseRadioButton.Tag)
            {
                case "Male":
                    _choosedGender = 1;
                    break;
                case "Female":
                    _choosedGender = 0;
                    break;
                case "Other":
                    _choosedGender = 2;
                    break;
            }
        }

        private async void Choose_Image(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            this.photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (this.photo == null)
            {
                // User cancelled photo capture
                return;
            }
            HttpClient httpClient = new HttpClient();
            currentUploadUrl = await httpClient.GetStringAsync("https://2-dot-backup-server-002.appspot.com/get-upload-token");
            Debug.WriteLine("Upload url:" + currentUploadUrl);
            HttpUploadFile(currentUploadUrl, "myFile", "image/png");


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
            Stream fileStream = await this.photo.OpenStreamForReadAsync();
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
                string imageUrl = reader2.ReadToEnd();
                t_avatar.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                t_avatarUrl.Text = imageUrl;
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

        private static CameraCaptureUIMode GetPhoto()
        {
            return CameraCaptureUIMode.Photo;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;

        }
      
        private void GotoList_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ListStudent));

        }
    }
}
