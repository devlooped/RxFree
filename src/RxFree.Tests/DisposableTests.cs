using System.Reactive.Disposables;
using Xunit;

namespace RxFree.Tests
{
    public class DisposableTests
    {
        [Fact]
        public void DisposeOnce()
        {
            var disposed = 0;
            var disposable = Disposable.Create(() => disposed++);

            disposable.Dispose();

            Assert.Equal(1, disposed);

            disposable.Dispose();

            Assert.Equal(1, disposed);
        }

        [Fact]
        public void EmptyDisposable()
        {
            var disposable = Disposable.Empty;
            disposable.Dispose();
            disposable.Dispose();
            disposable.Dispose();
        }
    }
}
