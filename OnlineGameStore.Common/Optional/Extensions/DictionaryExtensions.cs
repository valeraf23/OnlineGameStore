using System.Collections.Generic;

namespace OnlineGameStore.Common.Optional.Extensions
{
    public static class DictionaryExtensions
    {
        public static Option<TValue> TryGetValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value)
                ? (Option<TValue>) new Some<TValue>(value)
                : None.Value;
        }
    }
}