using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreCodeFirst.Entities
{
    [Table("DonHang")]
    public class DonHang
    {
        [Key]
        public Guid MaDH { get; set; }
        public DateTime NgayDat { get; set; }
        public string NguoiDat { get; set; }
        public ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DonHang()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }
    }
}
