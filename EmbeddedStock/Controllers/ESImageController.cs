using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmbeddedStock.Data;
using EmbeddedStock.Models;

namespace EmbeddedStock.Controllers
{
    public class ESImageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ESImageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ESImage
        public async Task<IActionResult> Index()
        {
            return View(await _context.ESImages.ToListAsync());
        }

        // GET: ESImage/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eSImage = await _context.ESImages
                .SingleOrDefaultAsync(m => m.ESImageId == id);
            if (eSImage == null)
            {
                return NotFound();
            }

            return View(eSImage);
        }

        // GET: ESImage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ESImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ESImageId,ImageMimeType,Thumbnail,ImageData")] ESImage eSImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eSImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eSImage);
        }

        // GET: ESImage/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eSImage = await _context.ESImages.SingleOrDefaultAsync(m => m.ESImageId == id);
            if (eSImage == null)
            {
                return NotFound();
            }
            return View(eSImage);
        }

        // POST: ESImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ESImageId,ImageMimeType,Thumbnail,ImageData")] ESImage eSImage)
        {
            if (id != eSImage.ESImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eSImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ESImageExists(eSImage.ESImageId))
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
            return View(eSImage);
        }

        // GET: ESImage/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eSImage = await _context.ESImages
                .SingleOrDefaultAsync(m => m.ESImageId == id);
            if (eSImage == null)
            {
                return NotFound();
            }

            return View(eSImage);
        }

        // POST: ESImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var eSImage = await _context.ESImages.SingleOrDefaultAsync(m => m.ESImageId == id);
            _context.ESImages.Remove(eSImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ESImageExists(long id)
        {
            return _context.ESImages.Any(e => e.ESImageId == id);
        }
    }
}
