using System.ComponentModel.DataAnnotations;

namespace LiitBankAdminSystem.ViewModels
{
    public class DepositViewModel
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal NewBalance { get; set; }

        public readonly DateTime Placeholerdate = DateTime.Now;

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }
        public bool DepositSuceed { get; set; }
    }
}
