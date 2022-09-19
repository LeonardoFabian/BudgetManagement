using BudgetManagement.Models;
using BudgetManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagement.Controllers
{
    public class AccountTypeController : Controller
    {
        private readonly IAccountTypeRepository accountTypeRepository;

        public AccountTypeController(IAccountTypeRepository accountTypeRepository)
        {
            this.accountTypeRepository = accountTypeRepository;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create( AccountType accountType )
        {
            if ( !ModelState.IsValid )
            {
                return View(accountType);
            }

            await accountTypeRepository.Create(accountType);

            return View();
        }
    }
}
