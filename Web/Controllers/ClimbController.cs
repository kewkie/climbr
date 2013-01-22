using System;
using System.Linq;
using System.Web.Mvc;
using Web.Common;
using Web.Models;
using Domain;

namespace Web.Controllers
{
    public class ClimbController : ClimbrController
    {
        //
        // GET: /Climb/
        public ClimbController(ClimbrContext context) : base(context)
        {
        }

        public ActionResult Index()
        {
            return View(db.Climbs.ToList());
        }

        //
        // GET: /Climb/Details/5
        public ActionResult Details(int id = 0)
        {
            Climb climb = db.Climbs.Find(id);
            if (climb == null)
            {
                return HttpNotFound();
            }
            return View(climb);
        }

        //
        // GET: /Climb/Create
        public ActionResult Create()
        {
            var viewModel = new CreateClimbViewModel();

            var userId = db.Users.Single(u => u.UserName == User.Identity.Name).Id;
            var lastLocation = db.Climbs
                .Where(c => c.ClimberId == userId)
                .OrderByDescending(c => c.Date)
                .Select(c => c.Route.LocationId)
                .FirstOrDefault();

            viewModel.LocationId = lastLocation;

            ViewBag.Locations = db.Locations.ToList();
            ViewBag.LastLocation = lastLocation;

            ViewBag.Routes = db.Routes.ToList();
            ViewBag.ClimbTypes = db.ClimbTypes.ToList();

            return View(viewModel);
        }

        //
        // POST: /Climb/Create
        [HttpPost]
        public ActionResult Create(CreateClimbViewModel createClimb)
        {
            Climb climb = new Climb();
            if (TryUpdateModel(climb))
            {
                climb.Climber = db.Users.Single(u => u.UserName == User.Identity.Name);
                climb.Date = DateTime.Now;

                db.Climbs.Add(climb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(climb);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}