using System.ComponentModel;

namespace System
{
    /// <summary>
    /// For compatibility with Rx
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal class StableCompositeDisposable
    {
        /// <summary>
        /// For compatibility with Rx
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IDisposable Create(params IDisposable[] disposables) => CompositeDisposable.Create(disposables);
    }
}
