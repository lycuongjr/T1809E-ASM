using asm_T1809E.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asm_T1809E.Services
{
    class SongService
    {
        public ObservableCollection<Song> LoadSongs()
        {
            ObservableCollection<Song> list = new ObservableCollection<Song>();
            list.Add(new Song()
            {
                Name = "Tình không xót xa",
                Link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui829/TinhThoiXotXa-LamTruong-2453487.mp3",
                Likes = 109,
                Description = "Tình thôi xót xa",
                Thumbnail = "https://avatar-nct.nixcdn.com/singer/avatar/2018/06/07/7/2/a/a/1528338325576_600.jpg"
            });
            list.Add(new Song()
            {
                Name = "Đi đu đưa đi",
                Link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui988/DiDuDuaDi-BichPhuong-6059493.mp3",
                Likes = 200,
                Description = "Tình chưa xót xa",
                Thumbnail = "http://giadinh.mediacdn.vn/thumb_w/640/2019/9/4/bich-phuong-trong-di-du-dua-difgfdgfdgd-1567596484678552798701.jpg"
            });
            list.Add(new Song()
            {
                Name = "Phía sau một cô gái",
                Likes = 200,
                Thumbnail = "http://phapluatnet.vn/Uploads/Originals/2017/5/28/20170528172129_sobin-hoang-son.jpg",
                Link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui929/PhiaSauMotCoGaiBeat-SoobinHoangSon-4632332.mp3"
            });
            list.Add(new Song()
            {
                Name = "Về Quê Ngoại",
                Likes = 100,
                Thumbnail = "https://avatar-nct.nixcdn.com/singer/avatar/2017/02/06/3/4/7/f/1486358917132_600.jpg",
                Link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui211/VeQueNgoai-QuangLe_3vg4w.mp3?st=LU5TMv6erWZEaOwGIa7bYg&e=1540064634&download=true"
            });
            return list;
        }
    }
}
