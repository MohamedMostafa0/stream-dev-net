using System;
using System.Text;

namespace Socket_Lib.Core.Buffering
{
    public class WritePacket
    {
        public byte[] Buffer { get; private set; }
        private int pointer;

        public WritePacket(byte opCode)
        {
            Buffer = new byte[1];
            AddByte(opCode);
        }
        private void ScaleBuffer(int lenght)
        {
            byte[] bytes = new byte[Buffer.Length + lenght];
            for (int i = 0; i < Buffer.Length; i++)
            {
                bytes[i] = Buffer[i];
            }
            Buffer = bytes;
        }
        public void AddByte(byte value)
        {
            ScaleBuffer(1);
            Buffer[pointer] = value;
            pointer++;
        }
        public void AddBool(bool value)
        {
            ScaleBuffer(1);
            byte[] bValue = BitConverter.GetBytes(value);
            Buffer[pointer] = bValue[0];
            pointer++;
        }
        public void AddChar(char value)
        {
            ScaleBuffer(2);
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0; i < 2; i++)
            {
                Buffer[pointer] = bytes[i];
                pointer++;
            }
        }
        public void AddShort(short value)
        {
            ScaleBuffer(2);
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0; i < 2; i++)
            {
                Buffer[pointer] = bytes[i];
                pointer++;
            }
        }
        public void AddInteger(int value)
        {
            ScaleBuffer(4);
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0; i < 4; i++)
            {
                Buffer[pointer] = bytes[i];
                pointer++;
            }
        }
        public void AddLong(long value)
        {
            ScaleBuffer(8);
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0; i < 8; i++)
            {
                Buffer[pointer] = bytes[i];
                pointer++;
            }
        }
        public void AddFloat(float value)
        {
            ScaleBuffer(4);
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0; i < 4; i++)
            {
                Buffer[pointer] = bytes[i];
                pointer++;
            }
        }
        public void AddDouble(double value)
        {
            ScaleBuffer(8);
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0; i < 8; i++)
            {
                Buffer[pointer] = bytes[i];
                pointer++;
            }
        }
        public void AddString(string value)
        {
            if (value.Length <= short.MaxValue)
            {
                short size = (short)value.Length;
                AddShort(size);
                byte[] bytes = Encoding.ASCII.GetBytes(value);
                ScaleBuffer(size);
                for (int i = 0; i < size; i++)
                {
                    Buffer[pointer] = bytes[i];
                    pointer++;
                }
            }
        }
    }
}
