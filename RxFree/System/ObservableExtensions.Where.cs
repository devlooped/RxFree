namespace System
{
    partial class ObservableExtensions
    {
        /// <summary>
        /// Filters the elements of an observable sequence based on the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to filter the elements in the source sequence on.</typeparam>
        /// <param name="source">The sequence that contains the elements to be filtered.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static IObservable<T> Where<T>(this IObservable<T> source, Func<T, bool> predicate)
            => new WhereSubject<T>(source ?? throw new ArgumentNullException(nameof(source)), predicate ?? throw new ArgumentNullException(nameof(predicate)));

        class WhereSubject<T> : Subject<T>
        {
            IDisposable subscription;
            Func<T, bool> predicate;

            public WhereSubject(IObservable<T> source, Func<T, bool> predicate)
            {
                this.predicate = predicate;
                subscription = source.Subscribe(
                    next =>
                    {
                        if (this.predicate(next))
                            OnNext(next);
                    },
                    OnError,
                    OnCompleted);
            }

            public override void Dispose()
            {
                base.Dispose();
                subscription?.Dispose();
                subscription = null;
                predicate = null;
            }
        }
    }
}
