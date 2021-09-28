using System.Reactive.Subjects;
using Xunit;

namespace System
{
    public class SubjectTests
    {
        [Fact]
        public void OnNext()
        {
            var subject = new Subject<int>();
            var value = 0;

            var subscription = subject.Subscribe(i => value += i);

            subject.OnNext(1);
            Assert.Equal(1, value);

            subject.OnNext(1);
            Assert.Equal(2, value);

            subscription.Dispose();

            subject.OnNext(1);
            Assert.Equal(2, value);
        }

        [Fact]
        public void OnNextDisposed()
        {
            var subject = new Subject<int>();

            subject.Dispose();

            Assert.Throws<ObjectDisposedException>(() => subject.OnNext(1));
        }

        [Fact]
        public void OnNextDisposedSubscriber()
        {
            var subject = new Subject<int>();
            var value = 0;

            subject.Subscribe(i => value += i).Dispose();

            subject.OnNext(1);

            Assert.Equal(0, value);
        }

        [Fact]
        public void OnCompleted()
        {
            var subject = new Subject<int>();
            var completed = false;

            var subscription = subject.Subscribe(_ => { }, () => completed = true);

            subject.OnCompleted();

            Assert.True(completed);
        }

        [Fact]
        public void OnCompletedNoOp()
        {
            var subject = new Subject<int>();

            var subscription = subject.Subscribe(_ => { });

            subject.OnCompleted();
        }

        [Fact]
        public void OnCompletedOnce()
        {
            var subject = new Subject<int>();
            var completed = 0;

            var subscription = subject.Subscribe(_ => { }, () => completed++);

            subject.OnCompleted();

            Assert.Equal(1, completed);

            subject.OnCompleted();

            Assert.Equal(1, completed);
        }

        [Fact]
        public void OnCompletedDisposed()
        {
            var subject = new Subject<int>();

            subject.Dispose();

            Assert.Throws<ObjectDisposedException>(() => subject.OnCompleted());
        }

        [Fact]
        public void OnCompletedDisposedSubscriber()
        {
            var subject = new Subject<int>();
            var completed = false;

            subject.Subscribe(_ => { }, () => completed = true).Dispose();

            subject.OnCompleted();

            Assert.False(completed);
        }

        [Fact]
        public void OnError()
        {
            var subject = new Subject<int>();
            var error = false;

            var subscription = subject.Subscribe(_ => { }, e => error = true);

            subject.OnError(new Exception());

            Assert.True(error);
        }

        [Fact]
        public void OnErrorOnce()
        {
            var subject = new Subject<int>();
            var errors = 0;

            var subscription = subject.Subscribe(_ => { }, e => errors++);

            subject.OnError(new Exception());

            Assert.Equal(1, errors);

            subject.OnError(new Exception());

            Assert.Equal(1, errors);
        }

        [Fact]
        public void OnErrorDisposed()
        {
            var subject = new Subject<int>();

            subject.Dispose();

            Assert.Throws<ObjectDisposedException>(() => subject.OnError(new Exception()));
        }

        [Fact]
        public void OnErrorDisposedSubscriber()
        {
            var subject = new Subject<int>();
            var error = false;

            subject.Subscribe(_ => { }, e => error = true).Dispose();

            subject.OnError(new Exception());

            Assert.False(error);
        }

        [Fact]
        public void OnErrorRethrowsByDefault()
        {
            var subject = new Subject<int>();

            var subs = subject.Subscribe(_ => { });

            Assert.Throws<ArgumentException>(() => subject.OnError(new ArgumentException()));
        }

        [Fact]
        public void OnErrorNullThrows()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(() => new Subject<int>().OnError(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }


        [Fact]
        public void SubscribeNullThrows()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(() => new Subject<int>().Subscribe(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void SubscribeDisposedThrows()
        {
            var subject = new Subject<int>();

            subject.Dispose();

            Assert.Throws<ObjectDisposedException>(() => subject.Subscribe(_ => { }));
        }

        [Fact]
        public void SubscribeOnCompleted()
        {
            var subject = new Subject<int>();
            subject.OnCompleted();
            var completed = false;

            subject.Subscribe(_ => { }, () => completed = true).Dispose();

            Assert.True(completed);
        }

        [Fact]
        public void SubscribeOnError()
        {
            var subject = new Subject<int>();
            subject.OnError(new Exception());
            var error = false;

            subject.Subscribe(_ => { }, e => error = true);

            Assert.True(error);
        }
    }
}
