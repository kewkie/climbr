using System.Linq;
using System.Web.Mvc;
using Web.Common;
using Domain;

namespace Web.Controllers
{
    public class RouteController : ClimbrController
    {
        //
        // GET: /Route/
        public RouteController(ClimbrContext context) : base(context)
        {
        }

        public ActionResult Index()
        {
            return View(db.Routes.ToList());
        }

        //
        // GET: /Route/Details/5
        public ActionResult Details(int id = 0)
        {
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        //
        // GET: /Route/Create
        public ActionResult Create()
        {
            ViewBag.ClimbTypes = db.ClimbTypes.ToList();
            ViewBag.Colors = db.Colors.ToList();
            ViewBag.Grades = db.Grades.ToList();
            ViewBag.Locations = db.Locations.ToList();

            return View();
        }

        //
        // POST: /Route/Create
        [HttpPost]
        public ActionResult Create(Route createRoute)
        {
            Route route = new Route();
            if (TryUpdateModel(route))
            {
                route.AddedBy = db.Users.Single(u => u.UserName == User.Identity.Name);
                db.Routes.Add(route);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Colors = db.Colors.ToList();
            ViewBag.ClimbTypes = db.ClimbTypes.ToList();
            ViewBag.Grades = db.Grades.ToList();
            ViewBag.Locations = db.Locations.ToList();

            return View(route);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}