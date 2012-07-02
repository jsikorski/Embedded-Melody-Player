using System;

namespace EmbeddedMelodyPlayer.Infrastructure
{
    public class BusyScope : IDisposable
    {
        public BusyScope()
        {
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