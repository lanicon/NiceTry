﻿using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (GetExt), "Get")]
    internal class When_I_try_to_divide_by_zero_and_get_the_result
    {
        private static Func<int> _divideByZero;
        private static bool _successCallbackExecuted;
        private static Exception _error;

        private Establish context = () =>
        {
            _divideByZero = () =>
            {
                var zero = 0;

                return 5 / zero;
            };
        };

        private Because of = () => _error = Catch.Exception(() => Try.To(_divideByZero)
            .Get());

        private It should_throw_the_DivideByZeroException_contained_in_the_try =
            () => _error.Should().BeOfType<DivideByZeroException>();
    }
}