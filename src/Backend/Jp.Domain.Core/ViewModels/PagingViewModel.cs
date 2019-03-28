namespace Jp.Domain.Core.ViewModels
{
    public class PagingViewModel
    {
        public PagingViewModel(int quantity = 10, int page = 1, string search = null)
        {
            Quantity = quantity;
            Page = page;
            Search = search;
        }

        public int Quantity { get; set; }
        public int Page { get; set; }
        public string Search { get; }
    }
}
