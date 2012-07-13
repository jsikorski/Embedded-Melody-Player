using System;
using System.Collections.Generic;
using System.Linq;

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
            Di10
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