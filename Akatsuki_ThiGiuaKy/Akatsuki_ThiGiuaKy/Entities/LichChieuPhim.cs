using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Akatsuki_ThiGiuaKy.Entities
{
    public class LichChieuPhim
    {
        [Key]
        public int MaLichChieuPhim { get; set; }

        [ForeignKey(nameof(Rap))]
        [Display(Name = "Rap")]
        public int MaRap { get; set; }

        public Rap Rap { get; set; }

        [ForeignKey(nameof(Phim))]
        [Display(Name = "Phim")]
        public int MaPhim { get; set; }

        public Phim Phim { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Thời gian chiếu phim")]
        public DateTime ThoiGianChieu { get; set; }

        public ICollection<DatVe> DatVes { get; set; }
    }
}
