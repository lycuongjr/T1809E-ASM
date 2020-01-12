using asm_T1809E.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asm_T1809E.Services
{
    class SQLiteStudentService : IMemberService
    {
        

        Task<Member> IMemberService.Create(Member member)
        {
            throw new NotImplementedException();
        }

        Task<bool> IMemberService.Delete(string rollNumber)
        {
            throw new NotImplementedException();
        }

        Task<Member> IMemberService.GetDetail(string rollNumber)
        {
            throw new NotImplementedException();
        }

        Task<List<Member>> IMemberService.GetList()
        {
            throw new NotImplementedException();
        }

        Task<Member> IMemberService.Update(Member member)
        {
            throw new NotImplementedException();
        }
    }
}
