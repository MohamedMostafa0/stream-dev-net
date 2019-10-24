using System;
using System.Text;

namespace Socket_Lib.Core.Buffering
{
    public class ReadPacket
    {
        private byte[] buffer;
        private int pointer;
        public byte OpCode { get; private set; }
        public ReadPacket(byte[] buffer)
        {
            this.buffer = new byte[buffer.Length];
            Buffer.BlockCopy(buffer, 0, this.buffer, 0, buffer.Length);
            OpCode = ReadByte();
        }
        public byte ReadByte()
        {
            byte value = buffer[pointer];
            pointer++;
            return value;
        }
        public bool ReadBool()
        {
            bool value = BitConverter.ToBoolean(buffer, pointer);
            pointer++;
            return value;
        }
        public char ReadChar()
        {
            char value = BitConverter.ToChar(buffer, pointer);
            pointer += 2;
            return value;
        }
        public short ReadShort()
        {
            short value = BitConverter.ToInt16(buffer, pointer);
            pointer += 2;
            return value;
        }
        public int ReadInteger()
        {
            int value = BitConverter.ToInt32(buffer, pointer);
            pointer += 4;
            return value;
        }
        public long ReadLong()
        {
            long value = BitConverter.ToInt64(buffer, pointer);
            pointer += 8;
            return value;
        }
        public float ReadFloat()
        {
            float value = BitConverter.ToSingle(buffer, pointer);
            pointer += 4;
            return value;
        }
        public double ReadDouble()
        {
            double value = BitConverter.ToDouble(buffer, pointer);
            pointer += 8;
            return value;
        }
        public string ReadString()
        {
            short value = ReadShort();
            string stringValue = Encoding.ASCII.GetString(buffer, pointer, value);
            pointer += stringValue.Length;
            return stringValue;
        }
    }
}
