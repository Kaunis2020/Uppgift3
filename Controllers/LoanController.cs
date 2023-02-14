using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uppgift3.Models;

namespace Uppgift3.Controllers
{
    public class LoanController : Controller
    {
        private readonly DatabaseContext _context;

        public LoanController(DatabaseContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        // GET: Loan
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Loan.Include(l => l.Borrower).Include(l => l.CD);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Loan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.Borrower)
                .Include(l => l.CD)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loan/Create
        public IActionResult Create()
        {
            if (_context.Borrower != null && _context.Borrower.Count() > 0)
                ViewData["BorrowerID"] = new SelectList(_context.Borrower, "ID", "ListString");
            else
                ViewBag.Message1 = "Inga låntagare inlagda";
            if (_context.CD != null && _context.CD.Count() > 0)
            {
                var loan = new Loan
                {
                    CDList = new List<SelectListItem>()
                };
                var cds = _context.CD.Include(x => x.Artist).Where(x => x.ArtistID == x.Artist.ID).AsEnumerable().GroupBy(x => x.Artist.Name);
                foreach (var item in cds)
                {
                    var optionGroup = new SelectListGroup() { Name = item.Key.ToString() };
                    foreach (var _cd in item)
                    {
                        loan.CDList.Add(new SelectListItem() { Value = _cd.ID.ToString(), Text = _cd.Title, Group = optionGroup });
                    }
                }
                return View(loan);
            }
            else
                ViewBag.Message2 = "Inga CD-skivor inlagda";
            return View();
        }

        // POST: Loan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BorrowerID,CD_ID,LoanDate,BackDate")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BorrowerID"] = new SelectList(_context.Borrower, "ID", "Name", loan.BorrowerID);
            ViewData["CD_ID"] = new SelectList(_context.CD, "ID", "Title", loan.CD_ID);
            return View(loan);
        }

        // GET: Loan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["BorrowerID"] = new SelectList(_context.Borrower, "ID", "Name", loan.BorrowerID);
            ViewData["CD_ID"] = new SelectList(_context.CD, "ID", "Title", loan.CD_ID);
            return View(loan);
        }

        // POST: Loan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BorrowerID,CD_ID,LoanDate,BackDate")] Loan loan)
        {
            if (id != loan.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.ID))
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
            ViewData["BorrowerID"] = new SelectList(_context.Borrower, "ID", "Name", loan.BorrowerID);
            ViewData["CD_ID"] = new SelectList(_context.CD, "ID", "Title", loan.CD_ID);
            return View(loan);
        }

        // GET: Loan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.Borrower)
                .Include(l => l.CD)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loan.FindAsync(id);
            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loan.Any(e => e.ID == id);
        }
    }
}
