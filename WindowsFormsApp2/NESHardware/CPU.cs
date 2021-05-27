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

        public void Connect(Emulator e)
        {
            emu = e;
        }
        public void Cycle()
        {
            if (cycles == 0)
            {
                Execute(Read(pc++));
            }

            cycles--;
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
                Write((ushort) (0x100 + pointer--), status.GetRegister());
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
            Write((ushort) (0x100 + pointer--), status.GetRegister());
            pc = (ushort) (Read(0xFFFE) | (Read(0xFFFF) << 8));
            cycles = 7;
        }
        public void Reset()
        {
            acc = 0;
            x = 0;
            y = 0;
            pointer = 0xFD;
            status.SetRegister(0x00);
            status.I = true;
            pc = (ushort) (Read(0xFFFC) | (Read(0xFFFD) << 8));
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
