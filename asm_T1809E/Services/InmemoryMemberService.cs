using asm_T1809E.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asm_T1809E.Services
{
    class InmemoryMemberService : IMemberService
    {
        private static List<Member> _members = new List<Member>();
        public Task<Member> Create(Member member)
        {
            _members.Add(member);
            return Task.FromResult(member);
        }

        public Task<List<Member>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<Member> GetDetail(string rollNumber)
        {
            throw new NotImplementedException();
        }

        public Task<Member> Update(Member member)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string rollNumber)
        {
            throw new NotImplementedException();
        }
    }
}
