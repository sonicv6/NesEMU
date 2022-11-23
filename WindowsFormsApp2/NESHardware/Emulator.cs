using System.Windows.Forms;

namespace WindowsFormsApp2.NESHardware
{
    public class Emulator
    {
        public CPU cpu = new CPU();
        public PPU ppu = new PPU();
        private Cartridge cartridge;
        private byte[] ram = new byte[2048];
        private int clocks = 0;
        public Emulator()
        {
            cpu.Connect(this);
            LoadCart(new Cartridge("Mario.nes"));
            cpu.Reset();
        }

        public void LoadTest()
        {
            ram = new byte[0xFFFF];
            ushort offset = 0x8000;
            byte[] prg = new byte[]
            {
                0xA2, 0x0A, 0x8E, 0x00, 0x00, 0xA2, 0x03, 0x8E, 0x01, 0x00, 0xAC, 0x00, 0x00, 0xA9, 0x00, 0x18, 0x6D,
                0x01, 0x00, 0x88, 0xD0, 0xFA, 0x8D, 0x02, 0x00, 0xEA, 0xEA, 0xEA
            };
            foreach (byte b in prg)
            {
                CPUWrite(offset++, b);
            }

            ram[0xFFFC] = 0x00;
            ram[0xFFFD] = 0x80;
        }

        public void LoadCart(Cartridge cart)
        {
            cartridge = cart;
            ppu.ConnectCartridge(cart);
        }

        public void Reset()
        {
            cpu.Reset();
            clocks = 0;
        }

        public void Clock()
        {
            ppu.Cycle();
            if (clocks % 3 == 0) cpu.Cycle();
            if (ppu.NMI)
            {
                cpu.NMI();
                ppu.NMI = false;
            }

            clocks++;
        }

        public void CPUWrite(ushort addr, byte data)
        {
            if (addr >= 0 && addr <= 0x1FFF)
            {
                ram[addr & 0x7FF] = data;
            }

            else if (addr >= 0x2000 && addr <= 0x3FFF)
            {
                ppu.CPUWrite((ushort) (addr & 0x0007), data);
            }

            else if (addr >= 0x4020 && addr <= 0xFFFF)
            {
                cartridge.CPUWrite(addr, data);
            }
        }

        public byte CPURead(ushort addr)
        {
            byte data = 0;
            if (cartridge.CPURead(addr, ref data))
            {
                return data;
            }
            if (addr >= 0 && addr <= 0x1FFF)
            {
                return ram[addr & 0x7FF];
            }
            else if (addr >= 0x2000 && addr <= 0x3FFF)
            {
                return ppu.CPURead((ushort) (addr & 0x0007));
            }

            return 0;
        }
    }
}
