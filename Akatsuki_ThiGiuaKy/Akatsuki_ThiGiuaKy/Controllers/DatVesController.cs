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
    public class DatVesController : Controller
    {
        private readonly MyDbContext _context;

        public DatVesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: DatVes
        public async Task<IActionResult> Index(string searchString)
        {
            //var searchDS = (from c in _context.DatVes
            //                    select c).ToList();
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    searchDS = searchDS.Where(s => s.MaLichChieuPhim.Contains(searchString)).ToList();
            //}
            var myDbContext = _context.DatVes.Include(d => d.LichChieuPhim);
            return View(await myDbContext.ToListAsync());           
        }

        // GET: DatVes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datVe = await _context.DatVes
                .Include(d => d.LichChieuPhim)
                .FirstOrDefaultAsync(m => m.MaDatVe == id);
            if (datVe == null)
            {
                return NotFound();
            }

            return View(datVe);
        }

        // GET: DatVes/Create
        public IActionResult Create()
        {
            ViewData["MaLichChieuPhim"] = new SelectList(_context.LichChieuPhims, "MaLichChieuPhim", "MaLichChieuPhim");
            return View();
        }

        // POST: DatVes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDatVe,SoDienThoai,MaLichChieuPhim,SoLuong,ThoiGianDat")] DatVe datVe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(datVe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLichChieuPhim"] = new SelectList(_context.LichChieuPhims, "MaLichChieuPhim", "MaLichChieuPhim", datVe.MaLichChieuPhim);
            return View(datVe);
        }

        // GET: DatVes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datVe = await _context.DatVes.FindAsync(id);
            if (datVe == null)
            {
                return NotFound();
            }
            ViewData["MaLichChieuPhim"] = new SelectList(_context.LichChieuPhims, "MaLichChieuPhim", "MaLichChieuPhim", datVe.MaLichChieuPhim);
            return View(datVe);
        }

        // POST: DatVes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDatVe,SoDienThoai,MaLichChieuPhim,SoLuong,ThoiGianDat")] DatVe datVe)
        {
            if (id != datVe.MaDatVe)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datVe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatVeExists(datVe.MaDatVe))
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
            ViewData["MaLichChieuPhim"] = new SelectList(_context.LichChieuPhims, "MaLichChieuPhim", "MaLichChieuPhim", datVe.MaLichChieuPhim);
            return View(datVe);
        }

        // GET: DatVes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datVe = await _context.DatVes
                .Include(d => d.LichChieuPhim)
                .FirstOrDefaultAsync(m => m.MaDatVe == id);
            if (datVe == null)
            {
                return NotFound();
            }

            return View(datVe);
        }

        // POST: DatVes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var datVe = await _context.DatVes.FindAsync(id);
            _context.DatVes.Remove(datVe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatVeExists(int id)
        {
            return _context.DatVes.Any(e => e.MaDatVe == id);
        }             
    }
}
