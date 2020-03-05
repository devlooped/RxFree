using System.Threading;

namespace System
{
    /// <summary>
    /// Provides a set of static methods for creating <see cref="IDisposable"/> objects.
    /// </summary>
    public static class Disposable
    {
        /// <summary>
        /// Gets the disposable that does nothing when disposed.
        /// </summary>
        public static IDisposable Empty { get; } = new EmptyDisposable();

        /// <summary>
        /// Creates a disposable object that invokes the specified action when disposed.
        /// </summary>
        /// <param name="dispose">Action to run during the first call to <see cref="IDisposable.Dispose"/>. The action is guaranteed to be run at most once.</param>
        /// <returns>The disposable object that runs the given action upon disposal.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dispose"/> is <c>null</c>.</exception>
        public static IDisposable Create(Action dispose) =>
            new AnonymousDisposable(dispose ?? throw new ArgumentNullException(nameof(dispose)));

        sealed class EmptyDisposable : IDisposable
        {
            public void Dispose() { }
        }

        /// <summary>
        /// Represents an Action-based disposable.
        /// </summary>
        internal sealed class AnonymousDisposable : IDisposable
        {
            volatile Action dispose;

            /// <summary>
            /// Constructs a new disposable with the given action used for disposal.
            /// </summary>
            /// <param name="dispose">Disposal action which will be run upon calling Dispose.</param>
            public AnonymousDisposable(Action dispose) =>
                this.dispose = dispose;

            /// <summary>
            /// Calls the disposal action if and only if the current instance hasn't been disposed yet.
            /// </summary>
            public void Dispose() =>
                Interlocked.Exchange(ref dispose, null)?.Invoke();
        }
    }
}
