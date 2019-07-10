using System;

namespace OnlineGameStore.Common.Errors
{
    public class NotFoundError : Error
    {
        public NotFoundError()
        {
        }

        public NotFoundError(Guid id) => Id = id;
        private Guid Id { get;}
        public string Msg => $"Not Found Object with id \"{Id}\"";
    }
}