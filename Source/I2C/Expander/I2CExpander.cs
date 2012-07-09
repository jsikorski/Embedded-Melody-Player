namespace I2C.Expander
{
    public class I2CExpander : I2CPlug
    {
        private const byte DefaultExpanderAddress = 0x20;

        /// <param name="expanderAddress">Address of the device. Default is 0x20.</param>
        public I2CExpander(byte expanderAddress = DefaultExpanderAddress) : base(expanderAddress)
        {
        }

        /// <param name="pinsMode">Modes of the pins. 0 at position n means output mode for n-th pin (and 1 means input mode).</param>
        /// <param name="expanderAddress">Address of the device. Default is 0x20.</param>
        public I2CExpander(byte pinsMode, byte expanderAddress = DefaultExpanderAddress) : base(expanderAddress)
        {
            SetPinsMode(pinsMode);
        }

        /// <param name="pinsMode">Modes of the pins. 0 at position n means output mode for n-th pin (and 1 means input mode).</param>
        public void SetPinsMode(byte pinsMode)
        {
            WriteToRegister((byte)ExpanderRegisters.IODIR, pinsMode);
        }

        public void Write(byte values)
        {
            WriteToRegister((byte)ExpanderRegisters.GPIO, values);
        }

        public byte ReadByte()
        {
            var buffer = new byte[1] { 0 };
            ReadFromRegister((byte)ExpanderRegisters.GPIO, buffer);
            return buffer[0];
        }
    }
}