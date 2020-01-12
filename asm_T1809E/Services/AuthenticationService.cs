using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace asm_T1809E.Services
{
    class AuthenticationService
    {
        private static string CONTENT_TYPE = "application/json";
        private static string LOGIN_API_URL = "https://2-dot-backup-server-002.appspot.com/_api/v2/members/authentication";
        public async Task<string> LoginTask(string email, string password)
        {
            JObject loginInfo = new JObject();
            loginInfo["email"] = email;
            loginInfo["password"] = password;
            // chuyển đối tượng member thành định dạng json.
            var loginJson = JsonConvert.SerializeObject(loginInfo);
            // quá trình đóng gói dữ liệu trước khi gửi đi.
            HttpContent contentToSend = new StringContent(loginJson,
                Encoding.UTF8, CONTENT_TYPE);
            // gọi shipper
            HttpClient httpClient = new HttpClient();
            // gửi đến đây (link), món quà này (contentToSend), chờ quá trình gửi thành công, thì lấy xác nhận từ người nhận.
            var response = await httpClient.PostAsync(LOGIN_API_URL, contentToSend);
            // doc du lieu tu ng nhan
            var stringContent = await response.Content.ReadAsStringAsync();

            return (string)JObject.Parse(stringContent)["token"];
        }

    }
}
