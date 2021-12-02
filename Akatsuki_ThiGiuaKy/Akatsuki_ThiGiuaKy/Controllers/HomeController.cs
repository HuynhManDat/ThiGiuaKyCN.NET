using Akatsuki_ThiGiuaKy.Entities;
using Akatsuki_ThiGiuaKy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Akatsuki_ThiGiuaKy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["phims"] = await _context.Phims.ToListAsync();

            ViewData["raps"] = await _context.Raps.ToListAsync();

            ViewData["lichchieus"] = await _context.LichChieuPhims
                .Include(l => l.Phim)
                .Include(l => l.Rap)
                .ToListAsync();

            return View();
        }
        public async Task<IActionResult> DatVe()
        {
            var lichchieus = await _context.LichChieuPhims
                .Include(l => l.Phim)
                .Include(l => l.Rap)
                .Where(l => l.ThoiGianChieu.CompareTo(DateTime.Now) > 0)
                .ToListAsync();

            ViewData["LichChieu"] = lichchieus.Select(l => new SelectListItem
            {
                Value = l.MaLichChieuPhim.ToString(),
                Text = l.Rap.TenRap + " - " + l.Phim.TenPhim + " - " + l.ThoiGianChieu
            });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DatVe([Bind("SoDienThoai,MaLichChieuPhim,SoLuong")] DatVe datVe)
        {
            var lichchieus = await _context.LichChieuPhims
                .Include(l => l.Phim)
                .Include(l => l.Rap)
                .Where(l => l.ThoiGianChieu.CompareTo(DateTime.Now) > 0)
                .ToListAsync();

            ViewData["LichChieu"] = lichchieus.Select(l => new SelectListItem
            {
                Value = l.MaLichChieuPhim.ToString(),
                Text = l.Rap.TenRap + " - " + l.Phim.TenPhim + " - " + l.ThoiGianChieu
            });

            if (ModelState.IsValid)
            {
                var lichchieu = lichchieus
                    .FirstOrDefault(lichchieu => lichchieu.MaLichChieuPhim == datVe.MaLichChieuPhim);

                var rap = await _context.Raps.FirstOrDefaultAsync(rap => rap.MaRap == lichchieu.MaRap);

                var soluongdadat = await _context.DatVes
                    .Where(d => d.MaLichChieuPhim == datVe.MaLichChieuPhim)
                    .GroupBy(d => d.MaLichChieuPhim)
                    .Select(d => new { SoLuong = d.Sum(d => d.SoLuong) })
                    .FirstOrDefaultAsync();
                if (soluongdadat == null)
                {
                    _context.Add(datVe);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DatVe));
                }
                if ((rap.SoChoTrong - soluongdadat.SoLuong) < datVe.SoLuong)
                {
                    ViewBag.Message = "Số lượng vé có thể đặt tối đa là " + (rap.SoChoTrong - soluongdadat.SoLuong).ToString();
                    return View(datVe);
                }


            }

            return View(datVe);
        }

        public IActionResult Privacy()
            {
                return View();
            }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
