using System;

namespace NiceTry {
    public static class Combinators {
        public static ITry<B> Then<A, B>(this ITry<A> ta, Func<ITry<A>, ITry<B>> f) {
            return ta.IsFailure ? new Failure<B>(ta.Error) : f(ta);
        }

        public static ITry<A> Then<A>(this ITry t, Func<ITry, ITry<A>> f) {
            return t.IsFailure ? new Failure<A>(t.Error) : f(t);
        }

        public static ITry Then<A>(this ITry<A> t, Func<ITry<A>, ITry> f) {
            return t.IsFailure ? new Failure(t.Error) : f(t);
        }

        public static ITry Then(this ITry t, Func<ITry, ITry> f) {
            return t.IsFailure ? new Failure(t.Error) : f(t);
        }

        public static ITry<B> OrElse<A, B>(this ITry<A> t, Func<ITry<B>> f) where A : B {
            return t.IsFailure ? f() : (ITry<B>) t;
        }

        public static ITry<B> OrElse<A, B>(this ITry<A> t, ITry<B> te) where A : B {
            return t.IsFailure ? te : (ITry<B>) t;
        }

        public static ITry Apply<A>(this ITry<A> t, Action<A> a) {
            return t.FlatMap(x => Try.To(() => a(x)));
        }

        public static ITry<B> Map<A, B>(this ITry<A> ta, Func<A, B> f) {
            return ta.FlatMap(a => Try.To(() => f(a)));
        }

        public static ITry<B> FlatMap<A, B>(this ITry<A> ta, Func<A, ITry<B>> f) {
            return ta.IsFailure ? new Failure<B>(ta.Error) : f(ta.Value);
        }

        public static ITry FlatMap<T>(this ITry<T> t, Func<T, ITry> f) {
            return t.IsFailure ? new Failure(t.Error) : f(t.Value);
        }

        public static ITry<C> LiftMap<A, B, C>(this ITry<A> ta, ITry<B> tb, Func<A, B, C> f) {
            return ta.FlatMap(a => tb.Map(b => f(a, b)));
        }

        public static ITry<A> Flatten<A>(this ITry<ITry<A>> tt) {
            return tt.FlatMap(t => t);
        }

        public static ITry Flatten(this ITry<ITry> tt) {
            return tt.FlatMap(t => t);
        }

        public static ITry Recover(this ITry t, Action<Exception> recover) {
            return t.IsFailure ? Try.To(() => recover(t.Error)) : t;
        }

        public static ITry<B> Recover<A, B>(this ITry<A> t, Func<Exception, B> f) where A : B {
            return t.IsFailure ? Try.To(() => f(t.Error)) : (ITry<B>) t;
        }

        public static ITry<B> RecoverWith<A, B>(this ITry<A> t, Func<Exception, ITry<B>> f) where A : B {
            return t.IsFailure ? f(t.Error) : (ITry<B>) t;
        }

        public static ITry RecoverWith(this ITry t, Func<Exception, ITry> f) {
            return t.IsFailure ? f(t.Error) : t;
        }

        public static ITry Transform(this ITry t, Func<ITry> whenSuccess, Func<Exception, ITry> whenFailure) {
            return t.IsSuccess ? whenSuccess() : whenFailure(t.Error);
        }

        public static ITry<B> Transform<A, B>(this ITry<A> ta, Func<A, ITry<B>> whenSuccess,
                                              Func<Exception, ITry<B>> whenFailure) {
            return ta.IsSuccess ? whenSuccess(ta.Value) : whenFailure(ta.Error);
        }

        public static ITry<A> Filter<A>(this ITry<A> t, Func<A, bool> p) {
            return t.FlatMap(x =>
                             p(x)
                                 ? t
                                 : new Failure<A>(
                                       new ArgumentException("The given predicate does not hold for this Try.")));
        }
    }
}