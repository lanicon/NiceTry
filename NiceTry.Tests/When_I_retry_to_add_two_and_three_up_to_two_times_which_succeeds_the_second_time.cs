using System;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Retry))]
    internal class When_I_retry_to_add_two_and_three_up_to_two_times_which_succeeds_the_second_time {
        private static ITry<int> _result;
        private static int _five;
        private static Func<int> _addTwoAndThreeButFailTheFirstTime;
        private static int _try;

        private Establish context = () => {
            _five = 2 + 3;

            _addTwoAndThreeButFailTheFirstTime = () => {
                _try += 1;

                if (_try < 2) {
                    throw new ArgumentException("Expected test exception.");
                }

                return _five;
            };
        };

        private Because of = () => _result = Retry.To(_addTwoAndThreeButFailTheFirstTime);

        private It should_contain_five_in_the_success =
            () => _result.Value.ShouldEqual(_five);

        private It should_return_a_success =
            () => _result.IsSuccess.ShouldBeTrue();
    }
}