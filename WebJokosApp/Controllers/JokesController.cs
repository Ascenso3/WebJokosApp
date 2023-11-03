using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebJokosApp.Data;
using WebJokosApp.Models;

namespace WebJokosApp.Controllers
{
    public class JokesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JokesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jokes
        public async Task<IActionResult> Index()
        {
              return _context.Jokes != null ? 
                          View(await _context.Jokes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Jokes'  is null.");
        }


        // GET: ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }


        // POST: ShowSearchForm
        //public string ShowFormResultados(String SearchFrase)
        //{
        //return View();
        // return "Termo de pesquisa é " + SearchFrase;
        //}

        public async Task<IActionResult> ShowFormResultados(string SearchFrase)
        {
            return _context.Jokes != null ?
                        View("Index", await _context.Jokes.Where(j =>j. JokesPergunta.Contains(SearchFrase)).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Jokes'  is null.");
        }

        // GET: Jokes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jokes == null)
            {
                return NotFound();
            }

            var jokes = await _context.Jokes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jokes == null)
            {
                return NotFound();
            }

            return View(jokes);
        }

        // GET: Jokes/Create
        public IActionResult Create()
        {
            return View();
        }

        //GET: Jokes/Create
        [Authorize]


        // POST: Jokes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,JokesPergunta,JokesResposta")] Jokes jokes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jokes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jokes);
        }

        // GET: Jokes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jokes == null)
            {
                return NotFound();
            }

            var jokes = await _context.Jokes.FindAsync(id);
            if (jokes == null)
            {
                return NotFound();
            }
            return View(jokes);
        }

        // POST: Jokes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Edit(int id, [Bind("ID,JokesPergunta,JokesResposta")] Jokes jokes)
        {
            if (id != jokes.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jokes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JokesExists(jokes.ID))
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
            return View(jokes);
        }

        // GET: Jokes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jokes == null)
            {
                return NotFound();
            }

            var jokes = await _context.Jokes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jokes == null)
            {
                return NotFound();
            }

            return View(jokes);
        }

        // POST: Jokes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]

            public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jokes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Jokes'  is null.");
            }
            var jokes = await _context.Jokes.FindAsync(id);
            if (jokes != null)
            {
                _context.Jokes.Remove(jokes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JokesExists(int id)
        {
          return (_context.Jokes?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
