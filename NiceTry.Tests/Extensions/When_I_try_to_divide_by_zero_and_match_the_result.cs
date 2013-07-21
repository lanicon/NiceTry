﻿using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    internal class When_I_try_to_divide_by_zero_and_match_the_result {
        static Func<int> _divideByZero;
        static bool _successCallbackExecuted;
        static Exception _error;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };

            _whenSuccess = i => _successCallbackExecuted = true;
            _whenFailure = error => _error = error;
        };

        Because of = () => Try.To(_divideByZero)
                              .Match(_whenSuccess, _whenFailure);

        It should_execute_the_failure_callback = () => _error.ShouldNotBeNull();

        It should_not_execute_the_success_callback = () => _successCallbackExecuted.ShouldBeFalse();

        static Action<int> _whenSuccess;
        static Action<Exception> _whenFailure;
    }
}