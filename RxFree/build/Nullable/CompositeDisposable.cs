using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
    /// <summary>
    /// Represents a group of disposable resources that are disposed together.
    /// </summary>
    [GeneratedCode("RxFree", "*")]
    [CompilerGenerated]
    internal class CompositeDisposable : IDisposable
    {
        bool disposed;
        readonly ConcurrentBag<IDisposable> disposables;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeDisposable"/> class from a group of disposables.
        /// </summary>
        /// <param name="disposables">Disposables that will be disposed together.</param>
        /// <exception cref="ArgumentNullException"><paramref name="disposables"/> is <see langword="null"/>.</exception>
        public CompositeDisposable(params IDisposable[] disposables)
        {
            this.disposables = new ConcurrentBag<IDisposable>(disposables) ?? throw new ArgumentNullException(nameof(disposables));
        }

        /// <summary>
        /// Gets a value that indicates whether the object is disposed.
        /// </summary>
        public bool IsDisposed => disposed;

        /// <summary>
        /// Adds a disposable to the <see cref="CompositeDisposable"/> or disposes the disposable if the <see cref="CompositeDisposable"/> is disposed.
        /// </summary>
        /// <param name="item">Disposable to add.</param>
        public void Add(IDisposable disposable)
        {
            if (disposed)
                disposable?.Dispose();
            else
                disposables.Add(disposable);
        }

        /// <summary>
        /// Disposes all disposables in the group and removes them from the group.
        /// </summary>
        public void Dispose()
        {
            if (disposed)
                return;

            disposed = true;
            while (disposables.TryTake(out var disposable))
            {
                disposable?.Dispose();
            }
        }

        /// <summary>
        /// Creates a new group of disposable resources that are disposed together.
        /// </summary>
        /// <param name="disposables">Disposable resources to add to the group.</param>
        /// <returns>Group of disposable resources that are disposed together.</returns>
        public static IDisposable Create(params IDisposable[] disposables) => new DisposableArray(disposables);

        sealed class DisposableArray : IDisposable
        {
            IDisposable[]? disposables;

            public DisposableArray(IDisposable[] disposables)
            {
                Volatile.Write(ref this.disposables, disposables ?? throw new ArgumentNullException(nameof(disposables)));
            }

            public void Dispose()
            {
                var old = Interlocked.Exchange(ref disposables, null);
                if (old != null)
                {
                    foreach (var disposable in old)
                    {
                        disposable?.Dispose();
                    }
                }
            }
        }
    }
}
