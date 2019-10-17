using System;

namespace OnlineGameStore.Api.Filters.PublisherIdStrategies
{
    public abstract class BaseAssignPublisherIdStrategy<T> : IAssignPublisherIdStrategy
    {
        protected T Model;
        public Type ForType => typeof(T);

        protected abstract void Assign(Guid userIdAsGuid);

        public void Convert(object argument, Guid userIdAsGuid)
        {
            switch (argument)
            {
                case null:
                    throw new ArgumentException(
                        $"{nameof(argument)} is null or empty.",
                        nameof(argument));
                case T model:
                    Model = model;
                    Assign(userIdAsGuid);
                    break;
            }
        }
    }
}