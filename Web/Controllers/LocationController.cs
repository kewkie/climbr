using System.Linq;
using System.Web.Mvc;
using Domain;
using Web.Common;

namespace Web.Controllers
{
    public class LocationController : ClimbrController
    {
        //
        // GET: /Location/
        public LocationController(ClimbrContext context) : base(context)
        {
        }

        public ActionResult Index()
        {
            return View(db.Locations.ToList());
        }

        //
        // GET: /Location/Details/5
        public ActionResult Details(int id = 0)
        {
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        //
        // GET: /Location/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Location/Create
        [HttpPost]
        public ActionResult Create(Location createlocation)
        {
            Location location = new Location();
            if (TryUpdateModel(location))
            {
                location.AddedBy = db.Users.Single(u => u.UserName == User.Identity.Name);
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(createlocation);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}