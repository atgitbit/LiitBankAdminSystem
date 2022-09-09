namespace LiitBankAdminSystem.ViewModels
{
    public class CustomerSearchViewModel
    {
        public string q { get; set; }
        public string SortOrder { get; set; }
        public string SortField { get; set; }
        public string OppositeSortOrder { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public List<CustomerViewModel> Customers { get; set; } = new List<CustomerViewModel>();

    }
}
