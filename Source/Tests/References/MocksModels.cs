using System.Collections.Generic;
using System.Linq;

namespace GHIElectronics.NETMF.Hardware
{
    public abstract class PWM
    {
        public abstract void Set(int frequency, int i);
        public abstract void Set(bool value);
    }
}
