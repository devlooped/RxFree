using System;
using System.Linq;
using Xunit;

namespace System
{
    public class LinqTests
    {
        [Fact]
        public void OfType()
        {
            var subject = new Subject<A>();

            var b = 0;
            var c = 0;

            subject.OfType<B>().Subscribe(_ => b++);
            var subs = subject.OfType<C>().Subscribe(_ => c++);

            subject.OnNext(new A());
            subject.OnNext(new B());
            subject.OnNext(new C());
            subject.OnNext(new C());

            Assert.Equal(1, b);
            Assert.Equal(2, c);

            subs.Dispose();

            subject.OnNext(new C());

            Assert.Equal(1, b);
            Assert.Equal(2, c);
        }

        [Fact]
        public void Select()
        {
            var subject = new Subject<A>();

            var count = 0;

            subject.Select(x => new B()).Subscribe(_ => count++);

            subject.OnNext(new A());
            subject.OnNext(new C());

            Assert.Equal(2, count);
        }

        [Fact]
        public void Where()
        {
            var subject = new Subject<A>();

            var count = 0;

            subject.Where(a => a.Id?.StartsWith("a") == true).Subscribe(_ => count++);

            subject.OnNext(new A());
            subject.OnNext(new A { Id = "asdf" });
            subject.OnNext(new A { Id = "sdf" });
            subject.OnNext(new A { Id = "a" });

            Assert.Equal(2, count);
        }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        class A { public string Id { get; set; } }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        class B : A { }
        class C : A { }
    }
}
