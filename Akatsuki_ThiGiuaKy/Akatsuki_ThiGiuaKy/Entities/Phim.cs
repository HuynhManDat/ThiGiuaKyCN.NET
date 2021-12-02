using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Akatsuki_ThiGiuaKy.Entities
{
    public class Phim
    {
        [Key]
        public int MaPhim { get; set; }

        [Required]
        [Display(Name = "Tên Phim")]
        public string TenPhim { get; set; }

        [Display(Name = "Thời Lượng")]
        public int ThoiLuong { get; set; }
        public ICollection<LichChieuPhim> LichChieuPhims { get; set; }
    }
}
