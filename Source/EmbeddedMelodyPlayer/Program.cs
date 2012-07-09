using System;
using System.Threading;
using EmbeddedMelodyPlayer.Core;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using GHIElectronics.NETMF.Hardware.LowLevel;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace EmbeddedMelodyPlayer
{
    public class I2CPlug
    {
        private const int DefaultClockRate = 400;
        private const int TransactionTimeout = 1000;

        private I2CDevice.Configuration i2cConfig;
        private I2CDevice i2cDevice;

        public byte Address { get; private set; }

        public I2CPlug(byte address, int clockRateKhz)
        {
            this.Address = address;
            this.i2cConfig = new I2CDevice.Configuration(this.Address, clockRateKhz);
            this.i2cDevice = new I2CDevice(this.i2cConfig);
        }
        public I2CPlug(byte address)
            : this(address, DefaultClockRate)
        {
        }

        private void Write(byte[] writeBuffer)
        {
            // create a write transaction containing the bytes to be written to the device
            I2CDevice.I2CTransaction[] writeTransaction = new I2CDevice.I2CTransaction[]
        {
            I2CDevice.CreateWriteTransaction(writeBuffer)
        };

            // write the data to the device
            int written = this.i2cDevice.Execute(writeTransaction, TransactionTimeout);

            while (written < writeBuffer.Length)
            {
                byte[] newBuffer = new byte[writeBuffer.Length - written];
                Array.Copy(writeBuffer, written, newBuffer, 0, newBuffer.Length);

                writeTransaction = new I2CDevice.I2CTransaction[]
            {
                I2CDevice.CreateWriteTransaction(newBuffer)
            };

                written += this.i2cDevice.Execute(writeTransaction, TransactionTimeout);
            }

            // make sure the data was sent
            if (written != writeBuffer.Length)
            {
                throw new Exception("Could not write to device.");
            }
        }
        private void Read(byte[] readBuffer)
        {
            // create a read transaction
            I2CDevice.I2CTransaction[] readTransaction = new I2CDevice.I2CTransaction[]
        {
            I2CDevice.CreateReadTransaction(readBuffer)
        };

            // read data from the device
            int read = this.i2cDevice.Execute(readTransaction, TransactionTimeout);

            // make sure the data was read
            if (read != readBuffer.Length)
            {
                throw new Exception("Could not read from device.");
            }
        }

        protected void WriteToRegister(byte register, byte value)
        {
            this.Write(new byte[] { register, value });
        }
        protected void WriteToRegister(byte register, byte[] values)
        {
            // create a single buffer, so register and values can be send in a single transaction
            byte[] writeBuffer = new byte[values.Length + 1];
            writeBuffer[0] = register;
            Array.Copy(values, 0, writeBuffer, 1, values.Length);

            this.Write(writeBuffer);
        }
        protected void ReadFromRegister(byte register, byte[] readBuffer)
        {
            this.Write(new byte[] { register });
            this.Read(readBuffer);
        }
    }

    public class ExpanderPlug : I2CPlug
    {
        private const int ExpanderPlugAddress = 0x20;

        public enum Registers
        {
            IODIR,
            IPOL,
            GPINTEN,
            DEFVAL,
            INTCON,
            IOCON,
            GPPU,
            INTF,
            INTCAP,
            GPIO,
            OLAT
        };

        public ExpanderPlug()
            : base(ExpanderPlugAddress)
        {
        }
        public ExpanderPlug(byte directions)
            : base(ExpanderPlugAddress)
        {
            SetDirections(directions);
        }

        public void SetDirections(byte directions)
        {
            this.WriteToRegister((byte)Registers.IODIR, directions);
        }

        public void Write(byte values)
        {
            this.WriteToRegister((byte)Registers.GPIO, values);
        }

        public byte Read()
        {
            byte[] values = new byte[1] { 0 };

            this.ReadFromRegister((byte)Registers.GPIO, values);

            return values[0];
        }
    }

    public class Program
    {
        //private static readonly PWM Red = new PWM((PWM.Pin)FEZ_Pin.PWM.Di10);
        //private static readonly PWM Green = new PWM((PWM.Pin)FEZ_Pin.PWM.Di9);
        //private static readonly PWM Blue = new PWM((PWM.Pin)FEZ_Pin.PWM.Di8);

        //private static I2CDevice _i2CDevice = new I2CDevice(new I2CDevice.Configuration(0x20, 400));

        //private static OutputPort _reset = new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di0, false);
        //private static OutputPort _data = new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di7, false);
        //private static OutputPort _clock = new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di6, false);
        //private static OutputPort _latch = new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di5, false);

        private static readonly OutputPort[] NotesLeds = new[]
                                                             {
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di0, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di1, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di2, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di3, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di4, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di5, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di6, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di7, false),
                                                             };

        public static void Main()
        {
            foreach (var led in NotesLeds)
            {
                led.Write(true);
            }

            //// initialize the expander plug, setting all pins as output
            //var expanderPlug = new ExpanderPlug(0x00);

            //// do forever...
            //while (true)
            //{
            //    expanderPlug.Write(0x03);       // turn all the pins on
            //    Thread.Sleep(250);              // make the pins stay on for 250 ms
            //    expanderPlug.Write(0x00);       // turn all the pins off
            //    Thread.Sleep(250);              // make the pins stay off for 250 ms
            //}

            //var buffer = new byte[100];
            //var readTransaction = I2CDevice.CreateReadTransaction(buffer);
            //_i2CDevice.Execute(new I2CDevice.I2CTransaction[] {readTransaction}, 1000);

            //_i2CDevice.Execute()
            //Blue.Set(10000, 100);

            //var programController = new ProgramController();
            //programController.Start();
            //_reset.Write(true);
            
            //_data.Write(true);

            //_clock.Write(true);
            //Thread.Sleep(1);
            //_clock.Write(false);
            //Thread.Sleep(1);
            //_clock.Write(true);            

            //_latch.Write(true);



            Debug.Print("Program started and ready.");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}