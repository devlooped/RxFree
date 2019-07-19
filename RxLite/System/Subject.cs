//Copyright(c) .NET Foundation and Contributors
//All Rights Reserved

//Licensed under the Apache License, Version 2.0 (the "License"); you
//may not use this file except in compliance with the License. You may
//obtain a copy of the License at

//http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
//implied. See the License for the specific language governing permissions
//and limitations under the License.

//https://github.com/dotnet/reactive/blob/master/Rx.NET/Source/src/System.Reactive/Subjects/Subject.cs

using System.Threading;

namespace System
{
    /// <summary>
    /// Represents an object that is both an observable sequence as well as an observer.
    /// Each notification is broadcasted to all subscribed observers.
    /// </summary>
    /// <typeparam name="T">The type of the elements processed by the subject.</typeparam>
    internal sealed class Subject<T> : IObserver<T>, IObservable<T>, IDisposable
    {
        static readonly SubjectDisposable[] Terminated = new SubjectDisposable[0];
        static readonly SubjectDisposable[] Disposed = new SubjectDisposable[0];

        SubjectDisposable[] observers;
        Exception exception;

        /// <summary>
        /// Creates a subject.
        /// </summary>
        public Subject() => Volatile.Write(ref observers, Array.Empty<SubjectDisposable>());

        /// <summary>
        /// Notifies all subscribed observers about the end of the sequence.
        /// </summary>
        public void OnCompleted()
        {
            for (; ; )
            {
                var observers = Volatile.Read(ref this.observers);
                if (observers == Disposed)
                {
                    exception = null;
                    ThrowDisposed();
                }
                if (observers == Terminated)
                {
                    break;
                }
                if (Interlocked.CompareExchange(ref this.observers, Terminated, observers) == observers)
                {
                    foreach (var observer in observers)
                    {
                        observer.Observer?.OnCompleted();
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Notifies all subscribed observers about the specified exception.
        /// </summary>
        /// <param name="error">The exception to send to all currently subscribed observers.</param>
        /// <exception cref="ArgumentNullException"><paramref name="error"/> is null.</exception>
        public void OnError(Exception error)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            for (; ; )
            {
                var observers = Volatile.Read(ref this.observers);
                if (observers == Disposed)
                {
                    exception = null;
                    ThrowDisposed();
                }
                if (observers == Terminated)
                {
                    break;
                }
                exception = error;
                if (Interlocked.CompareExchange(ref this.observers, Terminated, observers) == observers)
                {
                    foreach (var observer in observers)
                    {
                        observer.Observer?.OnError(error);
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Notifies all subscribed observers about the arrival of the specified element in the sequence.
        /// </summary>
        /// <param name="value">The value to send to all currently subscribed observers.</param>
        public void OnNext(T value)
        {
            var observers = Volatile.Read(ref this.observers);
            if (observers == Disposed)
            {
                exception = null;
                ThrowDisposed();
            }
            foreach (var observer in observers)
            {
                observer.Observer?.OnNext(value);
            }
        }

        /// <summary>
        /// Subscribes an observer to the subject.
        /// </summary>
        /// <param name="observer">Observer to subscribe to the subject.</param>
        /// <returns>Disposable object that can be used to unsubscribe the observer from the subject.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="observer"/> is null.</exception>
        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }

            var disposable = default(SubjectDisposable);
            for (; ; )
            {
                var observers = Volatile.Read(ref this.observers);
                if (observers == Disposed)
                {
                    exception = null;
                    ThrowDisposed();
                }
                if (observers == Terminated)
                {
                    var ex = exception;
                    if (ex != null)
                    {
                        observer.OnError(ex);
                    }
                    else
                    {
                        observer.OnCompleted();
                    }
                    break;
                }

                if (disposable == null)
                {
                    disposable = new SubjectDisposable(this, observer);
                }

                var n = observers.Length;
                var b = new SubjectDisposable[n + 1];
                Array.Copy(observers, 0, b, 0, n);
                b[n] = disposable;
                if (Interlocked.CompareExchange(ref this.observers, b, observers) == observers)
                {
                    return disposable;
                }
            }

            return Disposable.Empty;
        }

        /// <summary>
        /// Releases all resources used by the current instance of the <see cref="Subject{T}"/> class and unsubscribes all observers.
        /// </summary>
        public void Dispose()
        {
            Interlocked.Exchange(ref observers, Disposed);
            exception = null;
        }

        void Unsubscribe(SubjectDisposable observer)
        {
            for (; ; )
            {
                var a = Volatile.Read(ref observers);
                var n = a.Length;
                if (n == 0)
                {
                    break;
                }

                var j = Array.IndexOf(a, observer);

                if (j < 0)
                {
                    break;
                }

                var b = default(SubjectDisposable[]);
                if (n == 1)
                {
                    b = Array.Empty<SubjectDisposable>();
                }
                else
                {
                    b = new SubjectDisposable[n - 1];
                    Array.Copy(a, 0, b, 0, j);
                    Array.Copy(a, j + 1, b, j, n - j - 1);
                }
                if (Interlocked.CompareExchange(ref observers, b, a) == a)
                {
                    break;
                }
            }
        }

        void ThrowDisposed() => throw new ObjectDisposedException(string.Empty);

        class Disposable : IDisposable
        {
            public static IDisposable Empty { get; } = new Disposable();

            Disposable() { }

            public void Dispose() { }
        }

        class SubjectDisposable : IDisposable
        {
            Subject<T> subject;
            IObserver<T> observer;

            public SubjectDisposable(Subject<T> subject, IObserver<T> observer)
            {
                this.subject = subject;
                Volatile.Write(ref this.observer, observer);
            }

            public void Dispose()
            {
                var observer = Interlocked.Exchange(ref this.observer, null);
                if (observer == null)
                {
                    return;
                }

                subject.Unsubscribe(this);
                subject = null;
            }

            public IObserver<T> Observer => Volatile.Read(ref observer);
        }
    }
}