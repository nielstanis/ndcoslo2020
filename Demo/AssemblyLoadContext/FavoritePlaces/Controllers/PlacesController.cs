using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FavoritePlaces.Models;
using Services;

namespace FavoritePlaces.Controllers
{
    public class PlacesController : Controller
    {
        private readonly PlacesDbContext _context;
        private readonly IPDFService _pdfService;

        public PlacesController(PlacesDbContext context, IPDFService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }

        // GET: Places
        public async Task<IActionResult> Index()
        {
            return View(await _context.Places.ToListAsync());
        }

        // GET: Places/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _context.Places
                .FirstOrDefaultAsync(m => m.ID == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // GET: Places/Create
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult>Report(int? id)
        {
            if (id == null)
            {
                return View("Index");
            }

            var place = await _context.Places
                .FirstOrDefaultAsync(m => m.ID == id);

            if (place != null)
            {
                // AwesomePDF.PDFGenerator generator = new AwesomePDF.PDFGenerator("PDF");
                // var result = generator.GeneratePDF(place.Name, place.Image, place.Text);
                var result = _pdfService.GeneratePDF(place.Name, place.Image, place.Text);
                return new FileContentResult(System.IO.File.ReadAllBytes(result), "application/pdf");
            }

            return View("Index");
        }

        // POST: Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Image,Text")] Place place)
        {
            if (ModelState.IsValid)
            {
                _context.Add(place);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(place);
        }

        // GET: Places/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _context.Places.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }
            return View(place);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Image,Text")] Place place)
        {
            if (id != place.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(place);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceExists(place.ID))
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
            return View(place);
        }

        // GET: Places/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _context.Places
                .FirstOrDefaultAsync(m => m.ID == id);
                
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var place = await _context.Places.FindAsync(id);
            _context.Places.Remove(place);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> File(string l = "https://www.vvvamersfoort.nl/uploads/cache/medium/uploads/media/5d5d31417914d/dsc-3471.jpg")
        {
            var httpClient = new System.Net.Http.HttpClient();
            string location = httpClient.GetType().Assembly.CodeBase; 

            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine($"System.Net.Http.HttpClient: {location}");
            System.Console.ResetColor();

            var data = await httpClient.GetStreamAsync(l);
            return new FileStreamResult(data, "image/jpg");
        }

        private bool PlaceExists(int id)
        {
            return _context.Places.Any(e => e.ID == id);
        }
    }
}
