using System;

namespace EmbeddedMelodyPlayer.Infrastructure
{
    public class BusyScope : IDisposable
    {
        private readonly IFailureDetector _failureDetector;

        public BusyScope(IFailureDetector failureDetector)
        {
            _failureDetector = failureDetector;
            IsBusy = true;
        }

        public bool IsBusy { get; private set; }

        #region IDisposable Members

        public void Dispose()
        {
            IsBusy = false;
        }

        #endregion
    }
}