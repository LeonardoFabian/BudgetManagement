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

            var ifExistsAccountType = await accountTypeRepository.Exists(accountType.Name, accountType.UserId);

            if (ifExistsAccountType)
            {
                ModelState.AddModelError(nameof(accountType.Name), $"The name {accountType.Name} already exists.");

                return View(accountType);
            }

            await accountTypeRepository.Create(accountType);

            return View();
        }
    }
}
