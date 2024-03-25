using Microsoft.AspNetCore.Mvc;

namespace SchedAppAPI.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
