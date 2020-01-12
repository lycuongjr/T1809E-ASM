using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asm_T1809E.Models
{
    class MySong
    {
        private string _id;
        private string _name;
        private string _url;
        private string _message;
        private string _thumbnail;


        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Url { get => _url; set => _url = value; }
        public string Message { get => _message; set => _message = value; }
        public string Thumbnail { get => _thumbnail; set => _thumbnail = value; }
    }
}
