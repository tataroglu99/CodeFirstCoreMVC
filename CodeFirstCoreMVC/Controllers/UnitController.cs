using CodeFirstCoreMVC.Utility;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstCoreMVC.Controllers
{
    public class UnitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UnitListAsync()
        {
            UnitRepository repo = new UnitRepository(Settings.makeConnection());
            var dataList = await repo.GetAllAsync();
            return View(dataList);
        }

        [HttpGet]
        public IActionResult AddUnit()
        {
            return View();
        }
    }
}
