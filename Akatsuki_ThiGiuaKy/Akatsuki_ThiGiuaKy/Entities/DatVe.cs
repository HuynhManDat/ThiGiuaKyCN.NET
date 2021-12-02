using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Akatsuki_ThiGiuaKy.Entities
{
    public class DatVe
    {
        [Key]
        public int MaDatVe { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Số điện thoại người xem")]
        public string SoDienThoai { get; set; }

        [ForeignKey(nameof(LichChieuPhim))]
        [Display(Name = "Lịch chiếu")]
        public int MaLichChieuPhim { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Số lượng đặt")]
        public int SoLuong { get; set; } = 1;

        public LichChieuPhim LichChieuPhim { get; set; }

        [Display(Name = "Vé được đặt lúc")]
        public DateTime ThoiGianDat { get; set; }
    }
}
