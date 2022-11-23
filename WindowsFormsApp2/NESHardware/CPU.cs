using System.Diagnostics;
using System.IO;

namespace WindowsFormsApp2.NESHardware
{
    public partial class CPU
    {
        //Registers
        public byte acc;
        public byte x;
        public byte y;
        public StatusRegister status;
        public byte pointer;
        public ushort pc;

        //Internal variables
        private Emulator emu;
        private AddressMode addressMode;
        private byte cycles;
        private ushort absAddr;
        private ushort relAddr;
        private byte fetched;
        private long executions = 1;

        private StreamWriter output;

        public CPU()
        {
            output = new StreamWriter("outputlog.txt");
        }
        public void Connect(Emulator e)
        {
            emu = e;
        }
        public void Cycle()
        {
            if (cycles == 0)
            {
                var opcode = Read(pc++);
                Execute(opcode);
                status.U = true;
            }

            cycles--;
        }

        public void DebugCycle()
        {
            output.WriteLine($"{pc:X4} A:{acc:X2} X:{x:X2} Y:{y:X2} P:{status.Register:X2} SP:{pointer:X2}");
            while (executions < 8991)
            {
                if (cycles == 0)
                {
                    Execute(Read(pc++));
                    status.U = true;
                    output.WriteLine(
                        $"{pc:X4} A:{acc:X2} X:{x:X2} Y:{y:X2} P:{status.Register:X2} SP:{pointer:X2}");
                    executions++;
                }

                cycles--;
            }
            output.Close();
        }

        public void IRQ()
        {
            if (!status.I)
            {
                Write((ushort) (0x100 + pointer--), (byte)(pc >> 8));
                Write((ushort) (0x100 + pointer--), (byte)(pc));

                status.B = false;
                status.U = true;
                status.I = true;
                Write((ushort) (0x100 + pointer--), status.Register);
                pc = (ushort) (Read(0xFFFE) | (Read(0xFFFF) << 8));
                cycles = 7;
            }
        }

        public void NMI()
        {
            Write((ushort) (0x100 + pointer--), (byte)(pc >> 8));
            Write((ushort) (0x100 + pointer--), (byte)(pc));

            status.B = false;
            status.U = true;
            status.I = true;
            Write((ushort) (0x100 + pointer--), status.Register);
            pc = (ushort) (Read(0xFFFE) | (Read(0xFFFF) << 8));
            cycles = 7;
        }
        public void Reset()
        {
            acc = 0;
            x = 0;
            y = 0;
            pointer = 0xFD;
            status.Register = 0x34;
            pc = (ushort)((Read(0xFFFD) << 8) | Read(0xFFFC));
            //pc = 0xC000; Used for nestest rom before PPU is completed
            cycles = 8;
        }
        private byte Read(ushort addr)
        {
            return emu.CPURead(addr);
        }

        private void Write(ushort addr, byte data)
        {
            emu.CPUWrite(addr, data);
        }
    }
}
