using System.Threading.Tasks;
using OnlineGameStore.Common.Either;
using OnlineGameStore.Common.Errors;

namespace OnlineGameStore.Common
{
    public interface ISaveSafe<T, Tinput>
    {
        Task<Either<Error, T>> SaveSafe(Tinput obj);
    }
}