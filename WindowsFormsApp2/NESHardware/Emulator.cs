namespace WindowsFormsApp2.NESHardware
{
    public class Emulator
    {
        public CPU cpu = new CPU();
        public PPU ppu = new PPU();
        private byte[] RAM = new byte[2048];
        private int clocks = 0;
        public Emulator()
        {
            cpu.Connect(this);
            cpu.Reset();
        }

        public void LoadCart(Cartridge cart)
        {

        }

        public void Reset()
        {
            cpu.Reset();
            clocks = 0;
        }

        public void Clock()
        {

        }

        public void CPUWrite(ushort addr, byte data)
        {
            if (addr >= 0 && addr <= 0x1FFFF)
            {
                RAM[addr & 0x07FF] = data;
            }

            if (addr >= 0x2000 && addr <= 0x3FFF)
            {
                ppu.CPUWrite((ushort) (addr & 0x0007), data);
            }
        }

        public byte CPURead(ushort addr)
        {
            if (addr >= 0 && addr <= 0x1FFF)
            {
                return RAM[addr & 0x07FF];
            }

            if (addr >= 0x2000 && addr <= 0x3FFF)
            {
                return ppu.CPURead((ushort) (addr & 0x0007));
            }
            if (addr >= 0x4020 && addr <= 0xFFFF)
            {

            }

            return 0;
        }
    }
}
