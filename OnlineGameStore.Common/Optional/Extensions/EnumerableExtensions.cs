﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineGameStore.Common.Optional.Extensions
{
    public static class EnumerableExtensions
    {
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> sequence)
        {
            return sequence.Select(x => (Option<T>) new Some<T>(x))
                .DefaultIfEmpty(None.Value)
                .First();
        }

        public static Option<T> FirstOrNone<T>(
            this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            return sequence.Where(predicate).FirstOrNone();
        }

        public static IEnumerable<TResult> SelectOptional<T, TResult>(
            this IEnumerable<T> sequence, Func<T, Option<TResult>> map)
        {
            return sequence.Select(map)
                .OfType<Some<TResult>>()
                .Select(some => some.Content);
        }
    }
}