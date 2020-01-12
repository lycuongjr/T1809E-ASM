using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace asm_T1809E.Models
{
    class Song
    {
        private string _id;
        private string _name;
        private string _thumbnail;
        private string _singer;
      
        private string _link;
        private int _likes;
        private string description;


        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Thumbnail { get => _thumbnail; set => _thumbnail = value; }
        public string Singer { get => _singer; set => _singer = value; }
       
        public string Link { get => _link; set => _link = value; }
        public int Likes { get => _likes; set => _likes = value; }
        public string Description { get => description; set => description = value; }
    }
}

