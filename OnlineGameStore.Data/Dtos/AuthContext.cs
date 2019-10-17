using System.Collections.Generic;

namespace OnlineGameStore.Data.Dtos
{
    public class AuthContext
    {
        public List<SimpleClaim> Claims { get; set; }
        public PublisherModel UserProfile { get; set; }
    }
}