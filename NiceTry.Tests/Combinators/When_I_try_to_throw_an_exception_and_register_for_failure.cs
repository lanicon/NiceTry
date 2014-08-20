﻿using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (OnFailureExt), "OnFailure")]
    public class When_I_try_to_throw_an_exception_and_register_for_failure
    {
        private static Exception _error;

        private Because of = () => Try.To(() => { throw new Exception("Test exception"); })
            .OnFailure(error => _error = error);

        private It should_return_the_expected_exception = () => _error.Message.Should().Be("Test exception");
    }
}