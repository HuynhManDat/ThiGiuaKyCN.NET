using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akatsuki_ThiGiuaKy.Entities
{
    public class MyDbContext : DbContext
    {
        public DbSet<Rap> Raps { get; set; }

        public DbSet<LichChieuPhim> LichChieuPhims { get; set; }

        public DbSet<Phim> Phims { get; set; }

        public DbSet<DatVe> DatVes { get; set; }

        public MyDbContext(DbContextOptions options) : base(options)
        { 

        }
    }
}
