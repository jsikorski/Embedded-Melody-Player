using System;
using System.Threading;

namespace EmbeddedMelodyPlayer.Utils
{
    public class SeparateThreadTimer
    {
        private readonly Action _onTick;
        private readonly int _interval;

        private bool _isStarted;
        private bool _shouldWork;

        public SeparateThreadTimer(Action onTick, int interval)
        {
            _onTick = onTick;
            _interval = interval;
        }

        public void Start()
        {
            if (_isStarted)
                throw new Exception("Cannot start timer that is already started.");

            _shouldWork = true;
            var workingThread = new Thread(Work);
            workingThread.Start();

            _isStarted = true;
        }

        public void Abort()
        {
            if (!_isStarted)
                throw new Exception("Cannot stop not started timer.");

            _shouldWork = false;
        }

        private void Work()
        {
            while (_shouldWork)
            {
                _onTick();
                Thread.Sleep(_interval);
            }
        }
    }
}