using GigHub.Models;
using GigHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class GigController : Controller
    {
        private ApplicationDbContext _context;
        public GigController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Gig
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View(viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                Venue = viewModel.Venue,
                GenreId = viewModel.Genre,
                DateTime = viewModel.GetDateTime()
            };

            _context.Gigs.Add(gig);
            _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}