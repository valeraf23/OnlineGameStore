using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Helpers
{
    public static class PlatformTypeManager
    {
        public static PlatformType Mobile =>
            new PlatformType
            {
                Id = GuidsManager.Get[1],
                Type = "Mobile"
            };

        public static PlatformType Browser =>
            new PlatformType
            {
                Id = GuidsManager.Get[2],
                Type = "Browser"
            };

        public static PlatformType Desktop =>
            new PlatformType
            {
                Id = GuidsManager.Get[3],
                Type = "Desktop"
            };

        public static PlatformType Console =>
            new PlatformType
            {
                Id = GuidsManager.Get[4],
                Type = "Console"
            };
    }
}