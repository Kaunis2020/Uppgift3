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
    public class CDsController : Controller
    {
        private readonly DatabaseContext _context;

        public CDsController(DatabaseContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        [Route("CDS/SearchCDByArtist/{name}")]
        public IActionResult SearchCDByArtist(string name)
        {
            List<CD> templist = new List<CD>();
            List<CD> sendlist = new List<CD>();
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                /* Splittrar med tomma mellanrum;  */
                string[] keywords = name.Split(null);
                foreach (string wor in keywords)
                {
                    templist = _context.CD.Include(c => c.Artist).Where(cd => cd.Artist.Name.ToLower().Contains(wor.ToLower())).ToList();
                    if (templist.Count > 0)
                    {
                        foreach (CD ny in templist)
                        {
                            sendlist.Add(ny);
                        }
                    }
                }
                if (sendlist.Count > 0)
                {
                    /*  Går IGENOM SAMTLIGA RESULTAT och väljer DISTINKTA ID-NUMMER,
                    *  så att SAMMA ID-NUMMER INTE FÖREKOMMER FLERA GÅNGER; */
                    IEnumerable<CD> filteredList = sendlist.GroupBy(ku => ku.ID).Select(group => group.First());
                    /*  KONVERTERAR IEnumerable till LIST;   */
                    List<CD> nylista = new List<CD>(filteredList);
                    nylista.Sort((id1, id2) => id1.ID.CompareTo(id2.ID));
                    return Json(nylista);
                }
                else
                    return Json("Finns EJ");
            }
            return Json("Finns EJ");
        }

        [HttpGet]
        [Route("CDS/SearchCDByTitle/{title}")]
        public IActionResult SearchCDByTitle(string title)
        {
            List<CD> templist = new List<CD>();
            List<CD> sendlist = new List<CD>();
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrWhiteSpace(title))
            {
                /* Splittrar med tomma mellanrum;  */
                string[] keywords = title.Split(null);
                foreach (string wor in keywords)
                {
                    templist = _context.CD.Include(c => c.Artist).Where(cd => cd.Title.ToLower().Contains(wor.ToLower())).ToList();
                    if (templist.Count > 0)
                    {
                        foreach (CD ny in templist)
                        {
                            sendlist.Add(ny);
                        }
                    }
                }
                if (sendlist.Count > 0)
                {
                    /*  Går IGENOM SAMTLIGA RESULTAT och väljer DISTINKTA ID-NUMMER,
                    *  så att SAMMA ID-NUMMER INTE FÖREKOMMER FLERA GÅNGER; */
                    IEnumerable<CD> filteredList = sendlist.GroupBy(ku => ku.ID).Select(group => group.First());
                    /*  KONVERTERAR IEnumerable till LIST;   */
                    List<CD> nylista = new List<CD>(filteredList);
                    nylista.Sort((id1, id2) => id1.ID.CompareTo(id2.ID));
                    return Json(nylista);
                }
                else
                    return Json("Finns EJ");
            }
            return Json("Finns EJ");
        }

    // GET: CDs
    public async Task<IActionResult> Index()
    {
        var databaseContext = _context.CD.Include(c => c.Artist);
        return View(await databaseContext.ToListAsync());
    }

    // GET: CDs/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cD = await _context.CD
            .Include(c => c.Artist)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (cD == null)
        {
            return NotFound();
        }

        return View(cD);
    }

    // GET: CDs/Create
    public IActionResult Create()
    {
        if (_context.Artist != null && _context.Artist.Count() > 0)
            ViewData["ArtistID"] = new SelectList(_context.Artist, "ID", "Name");
        else
            ViewBag.Message = "Inga artister inlagda";
        return View();
    }

    // POST: CDs/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Title,ArtistID,Category,Year")] CD cD)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cD);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ArtistID"] = new SelectList(_context.Set<Artist>(), "ID", "Name", cD.ArtistID);
        return View(cD);
    }

    // GET: CDs/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cD = await _context.CD.FindAsync(id);
        if (cD == null)
        {
            return NotFound();
        }
        ViewData["ArtistID"] = new SelectList(_context.Set<Artist>(), "ID", "Name", cD.ArtistID);
        return View(cD);
    }

    // POST: CDs/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ArtistID,Category,Year")] CD cD)
    {
        if (id != cD.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cD);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CDExists(cD.ID))
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
        ViewData["ArtistID"] = new SelectList(_context.Set<Artist>(), "ID", "Name", cD.ArtistID);
        return View(cD);
    }

    // GET: CDs/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cD = await _context.CD
            .Include(c => c.Artist)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (cD == null)
        {
            return NotFound();
        }

        return View(cD);
    }

    // POST: CDs/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cD = await _context.CD.FindAsync(id);
        _context.CD.Remove(cD);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CDExists(int id)
    {
        return _context.CD.Any(e => e.ID == id);
    }
}
}
