using System.Runtime.ExceptionServices;

namespace System
{
    /// <summary>
    /// Create an <see cref="IObserver{T}"/> instance from delegate-based implementations 
    /// of the On* methods.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    partial class AnonymousObserver<T> : IObserver<T>
    {
        static readonly Action<Exception> rethrow = e => ExceptionDispatchInfo.Capture(e).Throw();
        static readonly Action nop = () => { };

        readonly Action<T> onNext;
        readonly Action<Exception> onError;
        readonly Action onCompleted;

        /// <summary>
        /// Creates the observable providing just the <paramref name="onNext"/> action.
        /// </summary>
        public AnonymousObserver(Action<T> onNext)
            : this(onNext, rethrow, nop) { }

        /// <summary>
        /// Creates the observable providing both the <paramref name="onNext"/> and 
        /// <paramref name="onError"/> actions.
        /// </summary>
        public AnonymousObserver(Action<T> onNext, Action<Exception> onError)
            : this(onNext, onError, nop) { }

        /// <summary>
        /// Creates the observable providing both the <paramref name="onNext"/> and 
        /// <paramref name="onCompleted"/> actions.
        /// </summary>
        public AnonymousObserver(Action<T> onNext, Action onCompleted)
            : this(onNext, rethrow, onCompleted) { }

        /// <summary>
        /// Creates the observable providing all three <paramref name="onNext"/>, 
        /// <paramref name="onError"/> and <paramref name="onCompleted"/> actions.
        /// </summary>
        public AnonymousObserver(Action<T> onNext, Action<Exception> onError, Action onCompleted)
        {
            this.onNext = onNext;
            this.onError = onError;
            this.onCompleted = onCompleted;
        }

        /// <summary>
        /// Calls the action implementing <see cref="IObserver{T}.OnCompleted()"/>.
        /// </summary>
        public void OnCompleted() => onCompleted();

        /// <summary>
        /// Calls the action implementing <see cref="IObserver{T}.OnError(Exception)"/>.
        /// </summary>
        public void OnError(Exception error) => onError(error);

        /// <summary>
        /// Calls the action implementing <see cref="IObserver{T}.OnNext(T)"/>.
        /// </summary>
        public void OnNext(T value) => onNext(value);
    }
}
