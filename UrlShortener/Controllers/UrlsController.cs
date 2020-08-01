using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    public class UrlsController : Controller
    {
        private readonly UrlShortenerContext _context;

        public UrlsController(UrlShortenerContext context)
        {
            _context = context;
        }

        // GET: Urls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Urls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RedirectUrl")] Url url)
        {
            if (ModelState.IsValid)
            {
                string randomString = RandomString(8).ToLower();
                url.IdString = randomString;
                _context.Add(url);
                await _context.SaveChangesAsync();
                return RedirectToAction("ThankYou", new {idString = randomString});
            }
            return View(url);
        }

        public IActionResult Get(string idString)
        {
            if (UrlExists(idString))
            {
                var url = _context.Url.FirstOrDefault(e => e.IdString == idString);
                return Redirect(url.RedirectUrl);
            }
            else
                return Content("System could not find url");
        }

        public IActionResult ThankYou(string idString)
        {
            ViewData["Link"] = "https://localhost:44361/Urls/Get?idString=" + idString;
            return View();
        }

        private bool UrlExists(int id)
        {
            return _context.Url.Any(e => e.Id == id);
        }

        private bool UrlExists(string idString)
        {
            return _context.Url.Any(e => e.IdString == idString);
        }

        private readonly Random _random = new Random();

      
        // Generates a random string with a given size.    
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):   
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
