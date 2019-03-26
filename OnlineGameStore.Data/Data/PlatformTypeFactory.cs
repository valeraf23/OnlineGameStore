using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Data
{
    public static class PlatformTypeFactory
    {
        public static PlatformType Mobile =>
            new PlatformType
            {
                Id = 1,
                Type = "Mobile"
            };

        public static PlatformType Browser =>
            new PlatformType
            {
                Id = 2,
                Type = "Browser"
            };

        public static PlatformType Desktop =>
            new PlatformType
            {
                Id = 3,
                Type = "Desktop"
            };

        public static PlatformType Console =>
            new PlatformType
            {
                Id = 4,
                Type = "Console"
            };
    }
}