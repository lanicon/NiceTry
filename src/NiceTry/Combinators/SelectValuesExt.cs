﻿using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NiceTry.Combinators {

    /// <summary>
    ///     Provides extension methods for <see cref="IEnumerable{T}" /> to extract values from
    ///     enumerables of <see cref="Try{T}" />.
    /// </summary>
    public static class SelectValuesExt {

        /// <summary>
        ///     Returns an <see cref="IEnumerable{T}" /> that contains only the values contained in
        ///     the elements of the specified <paramref name="enumerable" /> that represent success.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="enumerable" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static IEnumerable<T> SelectValues<T>([NotNull] this IEnumerable<Try<T>> enumerable) {
            enumerable.ThrowIfNull(nameof(enumerable));

            return enumerable
                    .Select(t => t.Match(
                        failure: _ => new { HasVal = false, Val = default(T) },
                        success: x => new { HasVal = true, Val = x }))
                    .Where(o => o.HasVal)
                    .Select(o => o.Val);
        }
    }
}