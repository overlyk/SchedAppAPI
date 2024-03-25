using Microsoft.AspNetCore.Mvc;

namespace SchedAppAPI.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
