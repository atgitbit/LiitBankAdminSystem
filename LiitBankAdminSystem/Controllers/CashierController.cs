using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using LiitBankAdminSystem.Areas.Identity.Data;
using LiitBankAdminSystem.Services;
using LiitBankAdminSystem.ViewModels;
using LiitBankAdminSystem.Models;
using Transaction = LiitBankAdminSystem.Models.Transaction;

namespace LiitBankAdminSystem.Controllers
{
 
    public class CashierController : Controller
    {
        private readonly ILogger<CashierController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IBankService _bankService;

        public CashierController(ILogger<CashierController> logger, UserManager<IdentityUser> userManager, ApplicationDbContext context, IBankService bankService)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _bankService = bankService;
        }


        [Authorize(Roles = "Cashier")]
        public IActionResult SearchCustomer(string q, string sortField, string sortOrder, int page = 1)
        {

            var viewModel = new CustomerSearchViewModel();
            var query = _context.Customers.Where(c => q == null || c.Givenname.Contains(q) || c.NationalId.Contains(q) || c.City.Contains(q));


            if (string.IsNullOrEmpty(sortField))
                sortField = "Namn";
            if (string.IsNullOrEmpty(sortOrder))
                sortField = "asc";

            if (sortField == "Id")
            {
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.CustomerId);
                else
                    query = query.OrderByDescending(c => c.CustomerId);
            }
            if (sortField == "Namn")
            {
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.Givenname);
                else
                    query = query.OrderByDescending(c => c.Givenname);
            }
            if (sortField == "Personnummer")
            {
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.NationalId);
                else
                    query = query.OrderByDescending(c => c.NationalId);
            }
            if (sortField == "Stad")
            {
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.City);
                else
                    query = query.OrderByDescending(c => c.City);
            }



            int totalRowCount = query.Count();


            int pageSize = 50;
            var pageCount = (double)totalRowCount / pageSize;
            viewModel.TotalPages = (int)Math.Ceiling(pageCount);

            int resultToSkip = (page - 1) * pageSize;

            query = query.Skip(resultToSkip).Take(pageSize);


            viewModel.Customers = query.Select(customer => new CustomerViewModel
            {
                Id = customer.CustomerId,
                PersonalNumber = customer.NationalId,
                Name = customer.Givenname,
                City = customer.City,
                PhoneNumber = customer.Telephonenumber,
                EmailAddress = customer.Emailaddress,
            }).ToList();


            viewModel.q = q;
            viewModel.SortField = sortField;
            viewModel.SortOrder = sortOrder;
            viewModel.Page = page;
            viewModel.OppositeSortOrder = sortOrder == "asc" ? "desc" : "asc";


            return View(viewModel);
        }
        [Authorize(Roles = "Cashier")]
        public IActionResult SelectCustomer(int id)
        {
            var viewModel = new SelectCustomerViewModel();
            var c = _context.Customers.Include(c => c.Dispositions).First(a => a.CustomerId == id);
            //var d = _context.Dispositions.Include(d => d.Account).First(d=>d.)

            viewModel.CustomerNumber = c.CustomerId.ToString();
            viewModel.PersonalNumber = c.NationalId;
            viewModel.Name = c.Givenname;
            viewModel.Adress = c.Streetaddress;
            viewModel.City = c.City;

            //viewModel.TotalBalanceOnAll = 

            return View(viewModel);
        }

        public IActionResult GetCustomerAllDetails(int Id)
        {
            var viewModel = new GetCustomerAllDetailsViewModel();

            viewModel.Customer = _context.Customers.SingleOrDefault(c => c.CustomerId == Id);

            decimal sum = 0;
            var accounts = new List<Account>();
            var loans = new List<Loan>();
         
            var transactions = new List<Transaction>();

            foreach (var d in _context.Dispositions)
            {
                foreach (var cd in viewModel.Customer.Dispositions)
                {
                    if (cd.CustomerId == d.CustomerId)
                    {
                        sum += _context.Accounts.FirstOrDefault(x => x.AccountId == cd.AccountId).Balance;
                        accounts = _context.Accounts.Where(x => x.AccountId == cd.AccountId).ToList();
                        loans = _context.Accounts.FirstOrDefault(x => x.AccountId == cd.AccountId).Loans.ToList();
                        transactions = _context.Accounts.FirstOrDefault(x => x.AccountId == cd.AccountId).Transactions.ToList();
                    }
                }
            }



            if (viewModel.Customer != null)
            {
                viewModel.Accounts = accounts;
                viewModel.Loans = loans;
                viewModel.Transactions = transactions;
                viewModel.TotalBalance = sum;
            
            }

            return View(viewModel);
        }
        public IActionResult GetAccountDetail(int id, int page = 1 )
        {
            var viewModel = new AccountDetailViewModel();
            var account = _context.Accounts.SingleOrDefault(a=> a.AccountId == id);
            var transactions = _context.Transactions.Where(t => t.AccountId == id)
                .OrderByDescending(t => t.TransactionId).ToList();

           
            viewModel.Account = account;
            viewModel.Transactions = transactions;
            
            var totalRowCount = viewModel.Transactions.Count();

            viewModel.TotalRowCount = totalRowCount;

           var pageSize = 20;
           var pageCount = (double) totalRowCount / pageSize;

           viewModel.NumberOfPages = (int) Math.Ceiling(pageCount);
           viewModel.CurrentPage = page;

           return View(viewModel);
        }
     
        public IActionResult CreateCustomer()
        {
            return View();
        }
        public IActionResult DeleteCustomer()
        {
            return View();
        }
        public IActionResult Withdraw()
        {
            var viewModel = new WithdrawViewModel();
            
            return View(viewModel);

        }
        [HttpPost]
        public IActionResult Withdraw(WithdrawViewModel viewModel)
        {
            var account = _bankService.FindAccountById(viewModel.AccountId);
            var success = _bankService.CheckWithdrawPossible(account.AccountId, viewModel.AmountToWithdraw);

            if (!success)
            {
                ModelState.AddModelError("AmountToWithdraw", "Du har inte tillräckligt på konto för att ta ut denna summa, testa med en lägre summa");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _bankService.Withdraw(viewModel.AccountId,viewModel.AmountToWithdraw);
                    return RedirectToAction("GetAccountDetail");
                }
            }

            return View(viewModel);
            
        }

       
        public IActionResult Deposit()
        {
            var viewModel = new DepositViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Deposit(DepositViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var transaction = new Transaction();
                var account = _context.Accounts.SingleOrDefault(a => a.AccountId == viewModel.AccountId);

                transaction.Balance = account.Balance + viewModel.Amount;
                transaction.AccountId = account.AccountId;
                transaction.Date = DateTime.Now.Date;
                transaction.Amount = viewModel.Amount;
                transaction.AccountNavigation = account;

                account.Balance += viewModel.Amount;
                account.Transactions.Add(transaction);
                viewModel.DepositSuceed = false;

                _context.SaveChanges();

                return RedirectToAction("GetAccountDetail");
            }

            viewModel.DepositSuceed = false;

            return View(viewModel);


        }
        public IActionResult Transaction()
        {
            var viewModel = new TransactionViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Transaction(TransactionViewModel viewModel)
        {
            var account = _bankService.FindAccountById(viewModel.FromAccountId);
            var success = _bankService.CheckWithdrawPossible(account.AccountId, viewModel.AmountToSend);

            if (!success)
            {
                ModelState.AddModelError("AmountToSend", "Du har inte tillräckligt på konto för att skicka denna summa, testa med en lägre summa");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _bankService.Transaction(viewModel.FromAccountId,viewModel.ToAccountId,viewModel.AmountToSend);
                    return RedirectToAction("GetAccountDetail");
                }
            }

            return View(viewModel);
        }
        public bool CheckWithdrawPossible(Account account, decimal amount)
        {
            
            var balance = account.Balance;

            if (balance <= amount && amount > 0M)
            {

                return false;
            }

            return true;
        }

    }
}
