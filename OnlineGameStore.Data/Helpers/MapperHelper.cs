using System.Reflection;
using AutoMapper;

namespace OnlineGameStore.Data.Helpers
{
    public static class MapperHelper
    {
        public static void InitMapperConf()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(Assembly.GetExecutingAssembly()));
        }

    }
}