using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Akatsuki_ThiGiuaKy.Entities
{
    public class Rap
    {
        [Key]
        public int MaRap { get; set; }

        [Display(Name = "Tên Rạp")]
        public string TenRap { get; set; }

        [Display(Name = "Số Chỗ Trống")]
        public int SoChoTrong { get; set; }
        public ICollection<LichChieuPhim> LichChieuPhims { get; set; }
    }
}
