using LiitBankAdminSystem.Models;


namespace LiitBankAdminSystem.ViewModels
{
    public class AccountDetailViewModel
    {
        public Account Account { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public int TotalRowCount { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
