using OnlineGameStore.Data.Dtos;

namespace OnlineGameStore.Data.Helpers
{
    public class GameResourceParameters
    {
        private const int MaxPageSize = 20;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string OrderBy { get; set; } = "Name";

        public string Genre { get; set; }
        public string SearchQuery { get; set; }
        public string Publisher { get; set; }
        public string PlatformType { get; set; }
        public string Fields { get; set; }
    }
}
