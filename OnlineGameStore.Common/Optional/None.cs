using System;

namespace OnlineGameStore.Common.Optional
{
    public sealed class None<T> : Option<T>, IEquatable<None<T>>, IEquatable<None>
    {
        public bool Equals(None<T> other)
        {
            return true;
        }

        public bool Equals(None other)
        {
            return true;
        }

        public override Option<TResult> Map<TResult>(Func<T, TResult> map)
        {
            return None.Value;
        }

        public override Option<TResult> MapOptional<TResult>(Func<T, Option<TResult>> map)
        {
            return None.Value;
        }

        public override T Reduce(T whenNone)
        {
            return whenNone;
        }

        public override T Reduce(Func<T> whenNone)
        {
            return whenNone();
        }

        public override bool Equals(object obj)
        {
            return !(obj is null) && (obj is None<T> || obj is None);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static bool operator ==(None<T> a, None<T> b)
        {
            return a is null && b is null ||
                   !(a is null) && a.Equals(b);
        }

        public static bool operator !=(None<T> a, None<T> b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return "None";
        }
    }

    public sealed class None : IEquatable<None>
    {
        private None()
        {
        }

        public static None Value { get; } = new None();

        public bool Equals(None other)
        {
            return true;
        }

        public override string ToString()
        {
            return "None";
        }

        public override bool Equals(object obj)
        {
            return !(obj is null) && (obj is None || IsGenericNone(obj.GetType()));
        }

        private bool IsGenericNone(Type type)
        {
            return type.GenericTypeArguments.Length == 1 &&
                   typeof(None<>).MakeGenericType(type.GenericTypeArguments[0]) == type;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}