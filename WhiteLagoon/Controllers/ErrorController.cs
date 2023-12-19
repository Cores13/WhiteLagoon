using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WhiteLagoon.Models;

namespace WhiteLagoon.Controllers
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            //var error = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            var error = "";

            if (statusCode == 404)
            {
                error = "Page not found";
                return View(error);
            }else
            {
                error = "An unexpected error ocurred. Please try again later.";
                return View(error);
            }
        }
    }
}
