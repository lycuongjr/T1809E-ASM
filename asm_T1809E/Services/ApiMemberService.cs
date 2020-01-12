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
    class ApiMemberService : IMemberService
    {
        private static string REGISTER_API_URL = "https://2-dot-backup-server-002.appspot.com/_api/v2/members";
        private static string CONTENT_TYPE = "application/json";

        private async Task<Member> PostMember(Member member)

        {
          
            // chuyen doi tuong member thanh dang json
            var studentJson = JsonConvert.SerializeObject(member);

            HttpContent contentTosent = new StringContent(studentJson, Encoding.UTF8, CONTENT_TYPE);

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(REGISTER_API_URL, contentTosent);
            var stringContent = await response.Content.ReadAsStringAsync();
            var returnStudent = JsonConvert.DeserializeObject<Member>(stringContent);
            Debug.WriteLine(returnStudent.id);
            return returnStudent;

        }
       
     
        Task<List<Member>> IMemberService.GetList()
        {
            throw new NotImplementedException();
        }

        Task<Member> IMemberService.GetDetail(string rollNumber)
        {
            throw new NotImplementedException();
        }

        Task<Member> IMemberService.Update(Member member)
        {
            throw new NotImplementedException();
        }

        Task<bool> IMemberService.Delete(string rollNumber)
        {
            throw new NotImplementedException();
        }

        public Task<Member> Create(Member member)
        {
            Debug.WriteLine("Called here");
            return PostMember(member);
        }
    }
}
