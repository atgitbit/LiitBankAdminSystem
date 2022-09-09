using LiitBankAdminSystem.Models;

namespace LiitBankAdminSystem.ViewModels
{
    public class GetCustomerAllDetailsViewModel
    {
        public Customer Customer { get; set; }
        public decimal TotalBalance { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
        public List<Card> Cards { get; set; } = new List<Card>();
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public List<Loan> Loans { get; set; } = new List<Loan>();
    }
}
