using System;
using System.Collections.Generic;
using System.Text;

namespace NesHardware
{
    public class Emulator
    {
        private CPU cpu = new CPU();

        public Emulator()
        {
            cpu.Connect(this);
        }
        public void BusWrite(ushort addr, byte data)
        {

        }

        public byte BusRead(ushort addr)
        {
            return 0x00;
        }
    }
}
