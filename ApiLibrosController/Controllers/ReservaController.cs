using Microsoft.AspNetCore.Mvc;

namespace ApiLibrosController.Controllers
{
    public class ReservaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
