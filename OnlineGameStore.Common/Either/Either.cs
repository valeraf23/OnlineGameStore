namespace OnlineGameStore.Common.Either
{
    public abstract class Either<TLeft, TRight>
    {
        public static implicit operator Either<TLeft, TRight>(TLeft obj)
        {
            return new Left<TLeft, TRight>(obj);
        }

        public static implicit operator Either<TLeft, TRight>(TRight obj)
        {
            return new Right<TLeft, TRight>(obj);
        }
    }

    public class Left<TLeft, TRight> : Either<TLeft, TRight>
    {
        public Left(TLeft content)
        {
            Content = content;
        }

        private TLeft Content { get; }

        public static implicit operator TLeft(Left<TLeft, TRight> obj)
        {
            return obj.Content;
        }
    }

    public class Right<TLeft, TRight> : Either<TLeft, TRight>
    {
        public Right(TRight content)
        {
            Content = content;
        }

        private TRight Content { get; }

        public static implicit operator TRight(Right<TLeft, TRight> obj)
        {
            return obj.Content;
        }
    }
}