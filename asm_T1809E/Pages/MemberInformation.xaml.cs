using asm_T1809E.Models;
using asm_T1809E.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class MemberInformation : Page
    {
        private MemberService _memberService = new MemberService();
        public MemberInformation()
        {
            this.InitializeComponent();
            LoadMemberInformation();
        }

        private async void LoadMemberInformation()
        {
            Member member = await _memberService.GetMemberInformation(Login.Token);
            if (member == null){
                this.Frame.Navigate(typeof(Pages.Login));
            }
            else
            {
                txt_fullname.Text = member.firstName + "-" + member.lastName;
                txt_email.Text = member.email;
                txt_address.Text = member.address;
                txt_phone.Text = member.phone;
                this.img_avatar.Source = new BitmapImage(new Uri(member.avatar, UriKind.Absolute));
                Debug.WriteLine(member);
            }         
        }
    }
}
