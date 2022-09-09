using LiitBankAdminSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace LiitBankAdminSystem.ViewModels
{
    public class WithdrawViewModel
    {
        public int AccountId { get; set; }
        public decimal CurrentAmount { get; set; }

        [Required(ErrorMessage = "Du måste skriva in en summa för att ta ut")]
        [Display(Name = "Amount to Withdraw")]
        public decimal AmountToWithdraw { get; set; }
        public DateTime TimeOfAcction { get; set; } = new DateTime();

        public Transaction NewTransaction { get; set; }
    }
}
