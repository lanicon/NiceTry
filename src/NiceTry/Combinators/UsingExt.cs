using JetBrains.Annotations;
using System;
using TheVoid;
using static NiceTry.Predef;

namespace NiceTry.Combinators {

    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}" /> to handle instances of <see cref="IDisposable" />.
    /// </summary>
    public static class UsingExt {

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified
        ///     <paramref name="try" /> represents success as specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
        ///     and returns a <see cref="Success{T}" /> or a <see cref="Failure{T}" />, depending on
        ///     the outcome of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<Unit> Using<Disposable, T>(
            [NotNull] this Try<T> @try,
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Action<Disposable, T> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNull(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return Using(@try, createDisposable, (d, x) => {
                useDisposable(d, x);
                return Ok(Unit.Default);
            });
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified
        ///     <paramref name="try" /> represents success as specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
        ///     and returns a <see cref="Success{T}" /> containing the result or a
        ///     <see cref="Failure{T}" />, depending on the outcome of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="try"></param>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<B> Using<Disposable, A, B>(
            [NotNull] this Try<A> @try,
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, A, B> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNull(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return Using(@try, createDisposable, (d, a) => {
                var b = useDisposable(d, a);
                return Ok(b);
            });
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified
        ///     <paramref name="try" /> represents success as specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
        ///     and returns a <see cref="Success{T}" /> containing the result or a
        ///     <see cref="Failure{T}" />, depending on the outcome of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="try"></param>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<B> Using<Disposable, A, B>(
            [NotNull] this Try<A> @try,
            [NotNull] Func<A, Disposable> createDisposable,
            [NotNull] Func<Disposable, B> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNull(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return Using(@try, createDisposable, d => {
                var res = useDisposable(d);
                return Ok(res);
            });
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified
        ///     <paramref name="try" /> represents success as specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
        ///     and returns a <see cref="Success{T}" /> containing the result or a
        ///     <see cref="Failure{T}" />, depending on the outcome of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="try"></param>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<B> Using<Disposable, A, B>(
            [NotNull] this Try<A> @try,
            [NotNull] Func<A, Disposable> createDisposable,
            [NotNull] Func<Disposable, Try<B>> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNull(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return @try.Match(
                failure: Fail<B>,
                success: a => Try.Using(() => createDisposable(a), useDisposable));
        }

        /// <summary>
        ///     Creates, uses and properly disposes a <see cref="IDisposable" /> if the specified
        ///     <paramref name="try" /> represents success as specified by the
        ///     <paramref name="createDisposable" /> and <paramref name="useDisposable" /> functions
        ///     and returns a <see cref="Success{T}" /> containing the result or a
        ///     <see cref="Failure{T}" />, depending on the outcome of the operation.
        /// </summary>
        /// <typeparam name="Disposable"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="try"></param>
        /// <param name="createDisposable"></param>
        /// <param name="useDisposable"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="createDisposable" /> or <paramref name="useDisposable" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<B> Using<Disposable, A, B>(
            [NotNull] this Try<A> @try,
            [NotNull] Func<Disposable> createDisposable,
            [NotNull] Func<Disposable, A, Try<B>> useDisposable) where Disposable : IDisposable {
            @try.ThrowIfNull(nameof(@try));
            createDisposable.ThrowIfNull(nameof(createDisposable));
            useDisposable.ThrowIfNull(nameof(useDisposable));

            return @try.Match(
                failure: Fail<B>,
                success: a => Try.Using(createDisposable, d => useDisposable(d, a)));
        }
    }
}