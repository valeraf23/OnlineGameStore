namespace OnlineGameStore.Api.Filters.PublisherIdStrategies
{
    public static class PublisherIdStrategiesHelper
    {
        public static IAssignPublisherIdStrategy[] Get() => new IAssignPublisherIdStrategy[]
            {new GameCreationModelAssignIdStrategy(), new PublisherModelAssignIdStrategy()};
    }
}
