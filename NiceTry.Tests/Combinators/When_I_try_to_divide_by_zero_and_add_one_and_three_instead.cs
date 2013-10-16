using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "OrElse")]
    class When_I_try_to_divide_by_zero_and_add_one_and_three_instead
    {
        static Func<int> _divideByZero;
        static ITry<int> _result;
        static ITry<int> _four;
        static Func<ITry<int>> _addOneAndThree;

        Establish context = () =>
        {
            _divideByZero = () =>
            {
                var zero = 0;

                return 5 / zero;
            };

            _addOneAndThree = () => Try.To(() => 1 + 3);

            _four = _addOneAndThree();
        };

        Because of = () => _result = Try.To(_divideByZero)
                                        .OrElse(_addOneAndThree);

        It should_contain_four_in_the_success = () => _result.Value.ShouldEqual(_four.Value);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}