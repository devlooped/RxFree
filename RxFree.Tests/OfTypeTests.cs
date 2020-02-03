using System;
using Xunit;

namespace System
{
    public class OfTypeTests
    {
        [Fact]
        public void FilterSucceeeds()
        {
            var subject = new Subject<A>();

            var b = 0;
            var c = 0;

            subject.OfType<B>().Subscribe(x => b++);
            var subs = subject.OfType<C>().Subscribe(x => c++);

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


        class A { }
        class B : A { }
        class C : A { }
    }
}
