using System;
using Microsoft.SPOT.Hardware;

namespace I2C
{
    public abstract class I2CPlug
    {
        private const int DefaultClockRate = 400;
        private const int DefaultTransactionsTimeout = 1000;

        private readonly I2CDevice.Configuration _i2CConfig;
        private readonly I2CDevice _i2CDevice;
        private readonly int _transactionsTimeout;

        public byte Address { get; private set; }

        protected I2CPlug(byte address, int clockRateKhz = DefaultClockRate, int transactionsTimeout = DefaultTransactionsTimeout)
        {
            Address = address;
            _i2CConfig = new I2CDevice.Configuration(Address, clockRateKhz);
            _i2CDevice = new I2CDevice(_i2CConfig);
            _transactionsTimeout = transactionsTimeout;
        }

        protected void WriteToRegister(byte register, byte value)
        {
            Write(new[] { register, value });
        }

        private void Write(byte[] data)
        {
            var writeTransactions = GetWriteTransactionsForBytes(data);

            int numberOfWrittenBytes = _i2CDevice.Execute(writeTransactions, _transactionsTimeout);
            while (!WasAllDataWritten(data, numberOfWrittenBytes))
            {
                var newBuffer = new byte[data.Length - numberOfWrittenBytes];
                Array.Copy(data, numberOfWrittenBytes, newBuffer, 0, newBuffer.Length);
                writeTransactions = GetWriteTransactionsForBytes(newBuffer);

                numberOfWrittenBytes += _i2CDevice.Execute(writeTransactions, _transactionsTimeout);
            }

            if (numberOfWrittenBytes != data.Length)
                throw new Exception("Could not write to device.");
        }

        private static I2CDevice.I2CTransaction[] GetWriteTransactionsForBytes(byte[] data)
        {
            var writeTransaction = I2CDevice.CreateWriteTransaction(data);
            var writeTransactions = new I2CDevice.I2CTransaction[] { writeTransaction };
            return writeTransactions;
        }

        private static bool WasAllDataWritten(byte[] data, int numberOfWrittenBytes)
        {
            return numberOfWrittenBytes >= data.Length;
        }

        protected void WriteToRegister(byte register, byte[] values)
        {
            var writeBuffer = new byte[1 + values.Length];
            writeBuffer[0] = register;
            Array.Copy(values, 0, writeBuffer, 1, values.Length);

            Write(writeBuffer);
        }

        protected void ReadFromRegister(byte register, byte[] readBuffer)
        {
            Write(new[] { register });
            Read(readBuffer);
        }

        private void Read(byte[] readBuffer)
        {
            var readTransactions = GetReadTransactionsForBytes(readBuffer);
            int numberOfReadBytes = _i2CDevice.Execute(readTransactions, DefaultTransactionsTimeout);

            if (numberOfReadBytes != readBuffer.Length)
                throw new Exception("Could not read from device.");
        }

        private static I2CDevice.I2CTransaction[] GetReadTransactionsForBytes(byte[] readBuffer)
        {
            var readTransaction = I2CDevice.CreateReadTransaction(readBuffer);
            var readTransactions = new I2CDevice.I2CTransaction[] {readTransaction};
            return readTransactions;
        }
    }
}