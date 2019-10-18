namespace Jp.Domain.Core.ViewModels
{
    public class PagingViewModel
    {
        public PagingViewModel(int limit = 10, int offset = 1, string search = null)
        {
            Limit = limit;
            Offset = offset;
            Search = search;
        }

        public int Limit { get; set; }
        public int Offset { get; set; }
        public string Search { get; }
    }
}
