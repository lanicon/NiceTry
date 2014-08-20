using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (OrElseWithExt), "OrElseWith")]
    internal class When_I_try_to_divide_by_zero_and_try_to_add_one_and_three_instead
    {
        private static Try<int> _result;

        private Because of = () => _result = Try.Success(0)
            .Map(zero => 5 / zero)
            .OrElseWith(Try.Success(1 + 3));

        private It should_contain_four_in_the_success = () => _result.Value.Should().Be(4);

        private It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}