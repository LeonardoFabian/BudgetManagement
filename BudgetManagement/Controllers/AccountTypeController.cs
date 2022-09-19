using BudgetManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagement.Controllers
{
    public class AccountTypeController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create( AccountType accountType )
        {
            if ( !ModelState.IsValid )
            {
                return View(accountType);
            }
            return View();
        }
    }
}
