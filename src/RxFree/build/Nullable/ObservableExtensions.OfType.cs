using System.Reactive.Subjects;

namespace System.Reactive.Linq
{
    static partial class ObservableExtensions
    {
        /// <summary>
        /// Filters the elements of an observable sequence based on the specified type.
        /// </summary>
        /// <typeparam name="T">The type to filter the elements in the source sequence on.</typeparam>
        /// <param name="source">The sequence that contains the elements to be filtered.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static IObservable<T> OfType<T>(this IObservable<object> source)
            => new OfTypeSubject<T>(source ?? throw new ArgumentNullException(nameof(source)));

        class OfTypeSubject<T> : Subject<T>
        {
            IDisposable? subscription;

            public OfTypeSubject(IObservable<object> source)
            {
                subscription = source.Subscribe(
                    next =>
                    {
                        if (next is T result)
                            OnNext(result);
                    },
                    OnError,
                    OnCompleted);
            }

            public override void Dispose()
            {
                base.Dispose();
                subscription?.Dispose();
                subscription = null;
            }
        }
    }
}
