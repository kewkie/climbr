using System.Web.Mvc;
using Domain;

namespace Web.Common
{
    public class ClimbrController : Controller
    {
        protected readonly ClimbrContext db;

        public ClimbrController(ClimbrContext context)
        {
            db = context;
        }
    }
}
