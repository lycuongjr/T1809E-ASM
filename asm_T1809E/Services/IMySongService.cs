using asm_T1809E.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asm_T1809E.Services
{
    interface IMySongService
    {
        Task<MySong> Create(MySong mySong);
    }
}
