using Microsoft.AspNetCore.Mvc;

namespace DockerMvc
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
