using Microsoft.AspNetCore.Mvc;

namespace SchedAppAPI.Controllers
{
    public class GoalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
