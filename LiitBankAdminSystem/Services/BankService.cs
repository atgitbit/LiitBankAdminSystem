using LiitBankAdminSystem.Areas.Identity.Data;
using LiitBankAdminSystem.Models;

namespace LiitBankAdminSystem.Services
{
    public class BankService : IBankService
    {
        private readonly ApplicationDbContext _context;
        public BankService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Account FindAccountById(int id)
        {
            return _context.Accounts.FirstOrDefault(a => a.AccountId == id);
        }
        public void Withdraw(int id, decimal amount)
        {
            var fromAccount = _context.Accounts.FirstOrDefault(a => a.AccountId == id);
            var balance = fromAccount.Balance;
        }
        public void Transaction(int id, int toAccId, decimal amount)
        {
            throw new NotImplementedException();
        }
        public bool CheckWithdrawPossible(int id, decimal amount)
        {
            throw new NotImplementedException();
        }

        public void CreateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

    
    

     
    }
}
