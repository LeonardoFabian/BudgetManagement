using BudgetManagement.Models;
using BudgetManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagement.Controllers
{
    public class AccountTypeController : Controller
    {
        private readonly IAccountTypeRepository accountTypeRepository;
        private readonly IUsersService usersService;

        public AccountTypeController(IAccountTypeRepository accountTypeRepository, IUsersService usersService)
        {
            this.accountTypeRepository = accountTypeRepository;
            this.usersService = usersService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = usersService.GetUserId();

            var userAccountTypes = await accountTypeRepository.Get(userId);

            return View(userAccountTypes);
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

            accountType.UserId = usersService.GetUserId();

            var alreadyExistsAccountType = await accountTypeRepository.Exists(accountType.Name, accountType.UserId);

            if (alreadyExistsAccountType)
            {
                ModelState.AddModelError(nameof(accountType.Name), $"The name {accountType.Name} already exists.");

                return View(accountType);
            }

            await accountTypeRepository.Create(accountType);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerifyIfCategoryAlreadyExists(string name)
        {
            var userId = usersService.GetUserId();

            var alreadyExistsAccountType = await accountTypeRepository.Exists(name, userId);

            if (alreadyExistsAccountType)
            {
                return Json($"The {name} account type already exists.");
            }

            return Json(true);
        }
    }
}
