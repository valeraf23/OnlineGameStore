using System;
using FluentAssertions.Execution;

namespace OnlineGameStore.Tests.TestProject
{
    public static class MultiplyAssertion
    {
        public static void For(Action act)
        {
            using (new AssertionScope())
            {
                act();
            }
        }
    }
}