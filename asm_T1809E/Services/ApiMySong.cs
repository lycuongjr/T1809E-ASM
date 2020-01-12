using asm_T1809E.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace asm_T1809E.Services
{
    class ApiMySong : IMySongService
    {
        private string API = "http://2-dot-backup-server-001.appspot.com/upload-file-handle";
        private async Task<MySong> PostMember(MySong mySong)
            
        {

            // chuyen doi tuong member thanh dang json
            var mySongJson = JsonConvert.SerializeObject(mySong);

            HttpContent contentTosent = new StringContent(mySongJson, Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(API, contentTosent);
            var stringContent = await response.Content.ReadAsStringAsync();
            var returnMySong = JsonConvert.DeserializeObject<MySong>(stringContent);
            Debug.WriteLine(returnMySong.Id);
            return returnMySong;

        }
        public Task<MySong> Create(MySong mySong)
        {
            throw new NotImplementedException();
        }
    }
}
