using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieCollection.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieDbContext _context;

        public HomeController(ILogger<HomeController> logger, MovieDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        // Creating the view for the index or hom page, so it returns the page. 
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // Creating the view for the addmovie page, so it returns the page. Get and post help to validate the database enteries
        // and control what is shown to the user and the post is to keep info private. 
        public IActionResult AddMovie()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovie([Bind("MovieId,Category,Title,Year,Director,Rating,Edited,LentTo,Notes")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MovieList));
            }
            return View("MovieList");
        }


        // GET: Movies returns list of movies with options to edit and delete
        public async Task<IActionResult> MovieList()
        {
            return View(await _context.Movies.ToListAsync());
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MovieList));
        }


        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Category,Title,Year,Director,Rating,Edited,LentTo,Notes")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MovieList));
            }
            return View(movie);
        }

        // Returns the view for the MyPodcasts page, so it returns the page. 
        public IActionResult MyPodcasts()
        {
            return View();
        }

        // Returns the view for the Privacy page, so it returns the page. 
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}