using System.Web.Mvc;
using Domain;
using Web.Common;

namespace Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : ClimbrController
    {
        public HomeController(ClimbrContext context) : base(context)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
