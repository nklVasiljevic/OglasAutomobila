using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OglasAutomobila.Data;
using OglasAutomobila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OglasAutomobila.Controllers
{

    public class OglasiController : Controller
    {
        private readonly OglasiContext _context;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly SignInManager<AspNetUser> _signInManager;


        public OglasiController(OglasiContext context, UserManager<AspNetUser> userManager, SignInManager<AspNetUser>
        signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var oglas =  _context.Oglasis.FirstOrDefault(o => o.OglasiId == id);
            return View(oglas);

        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit (Oglasi oglasi)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(oglasi).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oglasi);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglasis
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.OglasiId == id);
            if (oglas == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(oglas);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oglasi = await _context.Oglasis.FindAsync(id);
            if (oglasi == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Oglasis.Remove(oglasi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Korisnici()
        {
            var korisnici = _context.AspNetUsers;
            return View(await korisnici.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> UkloniRolu (int id)
        {
            try
            {
                var userRola = _context.AspNetUserRoles.Single(e => e.UserId == id && e.RoleId == 2);
                _context.AspNetUserRoles.Remove(userRola);
                await _context.SaveChangesAsync();
                return View("Korisnici");
            }
            catch(DbUpdateException ex)
            {
                return View("Index");
            }           
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var oglasi = _context.Oglasis
                .Include(c => c.Mesto)
                .AsNoTracking();
            return View(await oglasi.ToListAsync());
        }


        [HttpGet]
        public  ActionResult Detalji(int id)
        
       {         
            
            var detaljiOglasa =  _context.Oglasis.Single(e=> e.OglasiId==id);            
            return View(detaljiOglasa);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Kreiraj()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Kreiraj(Oglasi oglas)
        {
           string user = User.Identity.Name;
            AspNetUser u = _context.AspNetUsers.Single(m => m.UserName==user);
            Oglasi o = new();
            OglasiUser ou = new();
            if (ModelState.IsValid)
            {
                o.Godište = oglas.Godište;
                o.Marka = oglas.Marka;
                o.Mesto = oglas.Mesto;
                o.Model = oglas.Model;
                o.Naziv = oglas.Naziv;
                o.Registrovan = oglas.Registrovan;
                o.RegistrovanDo = oglas.RegistrovanDo;
                o.Opis = oglas.Opis;
                ou.Oglasi = o;
                ou.User = u;
                _context.OglasiUsers.Add(ou);
                _context.Oglasis.Add(o);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
                return View();
        }


    }
}
