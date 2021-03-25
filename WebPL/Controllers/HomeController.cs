using Microsoft.AspNetCore.Mvc;
using Persistence;
using BL;
 
namespace PL_Web.Controllers
{
    public class HomeController:Controller
    {
        private ItemBL ibl = new ItemBL();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int? id)
        {
            ViewData["Item"] = ibl.GetItemById(id ?? 0);
            if(ViewData["Item"] == null){
                TempData["msg"] = "Can't find Item with id = " + id;
            }
            return View();
        }
    }
}