using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace LiitBankAdminSystem.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }

        [Remote("Transaction", "Cashier")]
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal CurrentAmount { get; set; }
        public decimal NewBalance { get; set; }

        [Required(ErrorMessage = "Du måste skriva in en summa för att skicka")]
        [Display(Name = "Amount to send")]
        public decimal AmountToSend { get; set; }
        public DateTime TimeOfAcction { get; set; } = new DateTime();
        public Transaction NewTransaction { get; set; }

        public bool SuceedTransaction { get; set; }
    }
}
