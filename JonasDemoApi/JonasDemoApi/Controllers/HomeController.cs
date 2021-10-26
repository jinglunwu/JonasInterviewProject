using System.Web.Mvc;

namespace JonasDemoApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}