using System;

namespace OnlineGameStore.Common.Either
{
    public static class EitherAdapters
    {
        public static Either<TLeft, TNewRight> Map<TLeft, TRight, TNewRight>(
            this Either<TLeft, TRight> either, Func<TRight, TNewRight> map) =>
            either is Right<TLeft, TRight> right
                ? (Either<TLeft, TNewRight>)map(right)
                : (TLeft)(Left<TLeft, TRight>)either;

        public static Either<TLeft, TNewRight> Map<TLeft, TRight, TNewRight>(
            this Either<TLeft, TRight> either, Func<TRight, Either<TLeft, TNewRight>> map) =>
            either is Right<TLeft, TRight> right
                ? map(right)
                : (TLeft)(Left<TLeft, TRight>)either;

        public static TRight Reduce<TLeft, TRight>(
            this Either<TLeft, TRight> either, Func<TLeft, TRight> map) =>
            either is Left<TLeft, TRight> left
                ? map(left)
                : (Right<TLeft, TRight>)either;

        public static Either<TLeft, TRight> Reduce<TLeft, TRight>(this Either<TLeft, TRight> either,
            Func<TLeft, TRight> map, Func<TLeft, bool> when) =>
            either is Left<TLeft, TRight> bound && when(bound)
                ? map(bound) as Either<TLeft, TRight>
                : either;

        public static Either<TLeft, TRight> DoWhenRight<TLeft, TRight>(this Either<TLeft, TRight> either,
            Action<TRight> map)
        {
            if (!(either is Right<TLeft, TRight> right))
            {
                return (TLeft)(Left<TLeft, TRight>)either;
            }

            map(right);
            return right;

        }

        public static Either<TLeft, TRight> DoWhenLeft<TLeft, TRight>(this Either<TLeft, TRight> either, Action<TLeft> map)
        {
            if (!(either is Left<TLeft, TRight> left))
            {
                return either;
            }

            map(left);
            return left;
        }

        public static Either<TLeft, TRight> DoWhenRight<TLeft, TRight>(this Either<TLeft, TRight> either, Action @do)
        {
            if (!(either is Right<TLeft, TRight> right))
            {
                return (TLeft)(Left<TLeft, TRight>)either;
            }

            @do();
            return right;

        }

        public static Either<TLeft, TRight> DoWhenLeft<TLeft, TRight>(this Either<TLeft, TRight> either, Action @do)
        {
            if (!(either is Left<TLeft, TRight> left))
            {
                return either;
            }

            @do();
            return left;

        }
    }
}
