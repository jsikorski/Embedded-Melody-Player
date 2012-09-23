using System.Threading;
using EmbeddedMelodyPlayer.Core;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Utils;
using GHIElectronics.NETMF.IO;
using Microsoft.SPOT;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer.Commands
{
    public class StartSdDetection : ICommand
    {
        private const int SdDetectionTimerInterval = 500;

        private readonly SdCardDetect _onSdCardDetected;
        private readonly ProgramContext _programContext;
        private readonly SeparateThreadTimer _sdDetectionTimer;

        private Thread _onDetectActionThread;
        private PersistentStorage _sdStorage;

        public StartSdDetection(ProgramContext programContext, SdCardDetect onSdCardDetected)
        {
            _onSdCardDetected = onSdCardDetected;
            _programContext = programContext;
            _sdDetectionTimer = new SeparateThreadTimer(DetectSdCard, SdDetectionTimerInterval);
        }

        private void DetectSdCard()
        {
            try
            {
                bool isSdCardDetected = IsSdCardDetected();

                if (isSdCardDetected && _sdStorage == null && !_programContext.IsPlaying)
                {
                    Debug.Print("Sd card detected.");
                    InitializeSdStorage();
                }
                else if (!isSdCardDetected && _sdStorage != null)
                {
                    Debug.Print("Sd card ejected.");
                    FinalizeSdStorage();
                }
            }
            catch
            {
                Debug.Print("Error while mounting SD card file system.");
                TryDisposeSdStorageObject();
            }
        }

        private static bool IsSdCardDetected()
        {
            bool isSdCardDetected = PersistentStorage.DetectSDCard();

            // Ensure card is stable
            if (isSdCardDetected)
            {
                Thread.Sleep(50);
                isSdCardDetected = PersistentStorage.DetectSDCard();
            }

            return isSdCardDetected;
        }

        private void InitializeSdStorage()
        {
            _sdStorage = new PersistentStorage("SD");
            _sdStorage.MountFileSystem();
        }

        private void FinalizeSdStorage()
        {
            _sdStorage.UnmountFileSystem();
            TryDisposeSdStorageObject();
        }

        private void TryDisposeSdStorageObject()
        {
            if (_sdStorage == null)
                return;

            _sdStorage.Dispose();
            _sdStorage = null;
        }

        public void Execute()
        {
            Debug.Print("Starting sd detection...");

            BindSdCardEventsHandlers();
            _sdDetectionTimer.Start();
        }

        private void BindSdCardEventsHandlers()
        {
            RemovableMedia.Insert += OnRemovableMediaInsert;
            RemovableMedia.Eject += OnRemovableMediaEject;
        }

        private void OnRemovableMediaInsert(object sender, MediaEventArgs mediaEventArgs)
        {
            Debug.Print("Sd card file system mounted.");

            _onDetectActionThread = new Thread(() => _onSdCardDetected(mediaEventArgs.Volume));
            _onDetectActionThread.Start();
        }

        private void OnRemovableMediaEject(object sender, MediaEventArgs mediaEventArgs)
        {
            Debug.Print("Sd card file system unmounted.");
        }
    }
}