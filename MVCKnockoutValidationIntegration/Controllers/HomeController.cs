using System.Web.Mvc;

namespace MVCKnockoutValidationIntegration.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult SimpleViewModel()
        {
            return View();
        }

        public ActionResult WrappedSimpleViewModel() {
            return View();
        }

        public ActionResult NestedViewModel() {
            return View();
        }
    }
}