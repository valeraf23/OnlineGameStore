namespace OnlineGameStore.Data.Dtos
{
    public interface IValidatorStrategy<T>
    {
        bool IsValid(T validateThis);
    }
}