using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Akatsuki_ThiGiuaKy.Entities;

namespace Akatsuki_ThiGiuaKy.Controllers
{
    public class LichChieuPhimsController : Controller
    {
        private readonly MyDbContext _context;

        public LichChieuPhimsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: LichChieuPhims
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.LichChieuPhims.Include(l => l.Phim).Include(l => l.Rap);
            return View(await myDbContext.ToListAsync());
        }

        // GET: LichChieuPhims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichChieuPhim = await _context.LichChieuPhims
                .Include(l => l.Phim)
                .Include(l => l.Rap)
                .FirstOrDefaultAsync(m => m.MaLichChieuPhim == id);
            if (lichChieuPhim == null)
            {
                return NotFound();
            }

            return View(lichChieuPhim);
        }

        // GET: LichChieuPhim/Viewers/5
        public async Task<IActionResult> Viewers(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhSachNguoiXem = await _context.DatVes
                .Include(l => l.LichChieuPhim).ThenInclude(x => x.Rap)
                .Include(l => l.LichChieuPhim).ThenInclude(x => x.Phim)
                .Where(m => m.MaLichChieuPhim == id).ToListAsync();

            ViewData["Rap"] = (await _context.LichChieuPhims
                .Include(x => x.Rap)
                .SingleOrDefaultAsync(x => x.MaLichChieuPhim == id)).Rap;

            if (danhSachNguoiXem == null)
            {
                return NotFound();
            }

            return View(danhSachNguoiXem);
        }

        // GET: LichChieuPhims/Create
        public IActionResult Create()
        {
            ViewData["MaPhim"] = new SelectList(_context.Phims, "MaPhim", "TenPhim");
            ViewData["MaRap"] = new SelectList(_context.Raps, "MaRap", "MaRap");
            return View();
        }

        // POST: LichChieuPhims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLichChieuPhim,MaRap,MaPhim,ThoiGianChieu")] LichChieuPhim lichChieuPhim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lichChieuPhim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaPhim"] = new SelectList(_context.Phims, "MaPhim", "TenPhim", lichChieuPhim.MaPhim);
            ViewData["MaRap"] = new SelectList(_context.Raps, "MaRap", "MaRap", lichChieuPhim.MaRap);
            return View(lichChieuPhim);
        }

        // GET: LichChieuPhims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichChieuPhim = await _context.LichChieuPhims.FindAsync(id);
            if (lichChieuPhim == null)
            {
                return NotFound();
            }
            ViewData["MaPhim"] = new SelectList(_context.Phims, "MaPhim", "TenPhim", lichChieuPhim.MaPhim);
            ViewData["MaRap"] = new SelectList(_context.Raps, "MaRap", "MaRap", lichChieuPhim.MaRap);
            return View(lichChieuPhim);
        }

        // POST: LichChieuPhims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLichChieuPhim,MaRap,MaPhim,ThoiGianChieu")] LichChieuPhim lichChieuPhim)
        {
            if (id != lichChieuPhim.MaLichChieuPhim)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lichChieuPhim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LichChieuPhimExists(lichChieuPhim.MaLichChieuPhim))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaPhim"] = new SelectList(_context.Phims, "MaPhim", "TenPhim", lichChieuPhim.MaPhim);
            ViewData["MaRap"] = new SelectList(_context.Raps, "MaRap", "MaRap", lichChieuPhim.MaRap);
            return View(lichChieuPhim);
        }

        // GET: LichChieuPhims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichChieuPhim = await _context.LichChieuPhims
                .Include(l => l.Phim)
                .Include(l => l.Rap)
                .FirstOrDefaultAsync(m => m.MaLichChieuPhim == id);
            if (lichChieuPhim == null)
            {
                return NotFound();
            }

            return View(lichChieuPhim);
        }

        // POST: LichChieuPhims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lichChieuPhim = await _context.LichChieuPhims.FindAsync(id);
            _context.LichChieuPhims.Remove(lichChieuPhim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LichChieuPhimExists(int id)
        {
            return _context.LichChieuPhims.Any(e => e.MaLichChieuPhim == id);
        }
    }
}
