namespace I2C.Expander
{
    public class I2CExpander : I2CPlug
    {
        private const byte DefaultExpanderAddress = 0x20;

        /// <param name="initialValue">InitialValueForPins</param>
        /// <param name="expanderAddress">Address of the device. Default is 0x20.</param>
        public I2CExpander(byte initialValue = 0x00, byte expanderAddress = DefaultExpanderAddress) : base(expanderAddress)
        {
            Write(initialValue);
        }

        /// <param name="pinsMode">Modes of the pins. 0 at position n means output mode for n-th pin (and 1 means input mode).</param>
        /// <param name="initialValue">Initial value for pins.</param>
        /// <param name="expanderAddress">Address of the device. Default is 0x20.</param>
        public I2CExpander(byte pinsMode, byte initialValue = 0x00, byte expanderAddress = DefaultExpanderAddress) : base(expanderAddress)
        {
            SetPinsMode(pinsMode);
            Write(initialValue);
        }

        /// <param name="pinsMode">Modes of the pins. 0 at position n means output mode for n-th pin (and 1 means input mode).</param>
        public void SetPinsMode(byte pinsMode)
        {
            WriteToRegister((byte)ExpanderRegisters.IODIR, pinsMode);
        }

        /// <param name="value">Value for pins.</param>
        public void Write(byte value)
        {
            WriteToRegister((byte)ExpanderRegisters.GPIO, value);
        }

        public byte ReadByte()
        {
            var buffer = new byte[1] { 0 };
            ReadFromRegister((byte)ExpanderRegisters.GPIO, buffer);
            return buffer[0];
        }
    }
}