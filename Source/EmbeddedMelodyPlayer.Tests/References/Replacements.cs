using System;
using System.Collections.Generic;
using System.Linq;

internal delegate void NativeEventHandler(uint data1, uint data2, DateTime time);

namespace VikingErik.NetMF.MicroLinq
{
    internal static class LinqExtensions
    {
        public static bool Contains<T>(this IEnumerable<T> enumarable, T element)
        {
            return Enumerable.Contains(enumarable, element);
        }

        public static int Count<T>(this IEnumerable<T> enumrable)
        {
            return Enumerable.Count(enumrable);
        }
    }
}

namespace Microsoft.SPOT
{
    internal class Debug
    {
        public static void Print(string message)
        {
        }
    }
}

namespace Microsoft.SPOT.IO
{
    public class VolumeInfo
    {
        public string RootDirectory { get; set; }
    }

    internal class RemovableMedia
    {
        public static event InsertEventHandler Insert;
        public static event InsertEventHandler Eject;
    }

    internal class MediaEventArgs
    {
        public VolumeInfo Volume { get; set; }
    }

    internal delegate void InsertEventHandler(object sender, MediaEventArgs mediaEventArgs);
    internal delegate void EjectEventHandler(object sender, MediaEventArgs mediaEventArgs);
}

namespace Microsoft.SPOT.Hardware
{
    internal class Port
    {
        public enum ResistorMode
        {
            PullUp
        }

        public enum InterruptMode
        {
            InterruptEdgeLow
        }
    }

    internal class Cpu
    {
        internal enum Pin
        {
        }
    }

    internal class OutputPort
    {
        internal OutputPort(Cpu.Pin pin, bool initialState)
        {
        }

        internal void Write(bool state)
        {
        }
    }

    internal class InterruptPort
    {
        public NativeEventHandler OnInterrupt;
        
        public InterruptPort(Cpu.Pin portId, bool glitchFilter, Port.ResistorMode resistorMode, Port.InterruptMode interruptMode)
        {
        }

        public void ClearInterrupt()
        {
        }
    }
}

namespace GHIElectronics.NETMF.IO
{

    public class PersistentStorage : IDisposable
    {
        public PersistentStorage(string deviceId)
        {
        }

        public static bool DetectSDCard()
        {
            return true;
        }

        public void MountFileSystem()
        {
        }

        public void UnmountFileSystem()
        {
        }

        public void Dispose()
        {
        }
    }
}

namespace GHIElectronics.NETMF.Hardware
{
    internal class PWM
    {
        internal enum Pin
        {
        }

        internal PWM(Pin pin)
        {
        }

        internal virtual void Set(int frequency, int i)
        {
        }

        internal virtual void Set(bool value)
        {
        }
    }

    internal class AnalogIn
    {
        public enum Pin
        {
            Ain0
        }

        public AnalogIn(Pin pin)
        {
        }

        public void SetLinearScale(int min, int max)
        {
        }

        public int Read()
        {
            return 0;
        }
    }
}

namespace GHIElectronics.NETMF.FEZ
{
    internal class FEZ_Pin
    {
        internal enum PWM
        {
            Di5,
            Di8,
            Di9,
            Di10,
        }

        internal enum Digital
        {
            Di0,
            Di1,
            Di2,
            Di3,
            Di4,
            Di5,
            Di6,
            Di7,
            Di11,
            Di12
        }
    }
}

namespace I2C.Expander
{
    public class I2CExpander
    {
        public I2CExpander(byte pinsMode)
        {
        }

        public void Write(byte value)
        {
        }
    }
}