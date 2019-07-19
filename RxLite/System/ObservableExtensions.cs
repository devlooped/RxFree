using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace System
{
    /// <summary>
    /// Provides a set of static methods for subscribing delegates to observables.
    /// </summary>
    [GeneratedCode("RxLite", "*")]
    [CompilerGenerated]
    internal static class ObservableExtensions
    {
        static readonly Action<Exception> rethrow = e => ExceptionDispatchInfo.Capture(e).Throw();
        static readonly Action nop = () => { };

        /// <summary>
        /// Subscribes to the observable providing just the <paramref name="onNext"/> delegate.
        /// </summary>
        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext)
            => Subscribe(source, onNext, rethrow, nop);

        /// <summary>
        /// Subscribes to the observable providing both the <paramref name="onNext"/> and 
        /// <paramref name="onError"/> delegates.
        /// </summary>
        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError)
            => Subscribe(source, onNext, onError, nop);

        /// <summary>
        /// Subscribes to the observable providing both the <paramref name="onNext"/> and 
        /// <paramref name="onCompleted"/> delegates.
        /// </summary>
        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action onCompleted)
            => Subscribe(source, onNext, rethrow, onCompleted);

        /// <summary>
        /// Subscribes to the observable providing all three <paramref name="onNext"/>, 
        /// <paramref name="onError"/> and <paramref name="onCompleted"/> delegates.
        /// </summary>
        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError, Action onCompleted)
            => source.Subscribe(new AnonymousObserver<T>(onNext, onError, onCompleted));

        class AnonymousObserver<T> : IObserver<T>
        {
            readonly Action<T> onNext;
            readonly Action<Exception> onError;
            readonly Action onCompleted;

            public AnonymousObserver(Action<T> onNext, Action<Exception> onError, Action onCompleted)
            {
                this.onNext = onNext;
                this.onError = onError;
                this.onCompleted = onCompleted;
            }

            public void OnCompleted() => onCompleted();

            public void OnError(Exception error) => onError(error);

            public void OnNext(T value) => onNext(value);
        }
    }}
