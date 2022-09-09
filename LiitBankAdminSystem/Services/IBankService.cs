using LiitBankAdminSystem.Models;


namespace LiitBankAdminSystem.Services
{
    public interface IBankService
    {
        public Account FindAccountById(int id);
        public void Withdraw(int id, decimal amount);
        public void Transaction(int id, int toAccId, decimal amount);
        public bool CheckWithdrawPossible(int id, decimal amount);
        public void CreateTransaction(Transaction transaction);
    }
}
