using System.Reactive.Subjects;

namespace System.Reactive.Linq
{
    partial class ObservableExtensions
    {
        /// <summary>
        /// Projects each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the result sequence, obtained by running the selector function for each element in the source sequence.</typeparam>
        /// <param name="source">A sequence of elements to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each source element.</param>
        /// <returns>An sequence whose elements are the result of invoking the transform function on each element of source.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        public static IObservable<TResult> Select<TSource, TResult>(this IObservable<TSource> source, Func<TSource, TResult> selector)
            => new SelectorSubject<TSource, TResult>(source ?? throw new ArgumentNullException(nameof(source)), selector ?? throw new ArgumentNullException(nameof(selector)));

        class SelectorSubject<TSource, TResult> : Subject<TResult>
        {
            IDisposable? subscription;
            Func<TSource, TResult>? selector;

            public SelectorSubject(IObservable<TSource> source, Func<TSource, TResult> selector)
            {
                this.selector = selector;
                subscription = source.Subscribe(
                    next => OnNext(this.selector(next)),
                    OnError,
                    OnCompleted);
            }

            public override void Dispose()
            {
                base.Dispose();
                subscription?.Dispose();
                subscription = null;
                selector = null;
            }
        }
    }
}
