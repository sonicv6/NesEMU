using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.NESHardware
{
    public class PPU
    {
        private Cartridge cartridge;
        private byte[,] nameTable = new byte[2, 1024];
        private byte[] paletteTable = new byte[32];
        private byte[,] patternTable = new byte[2, 4096];
        private byte latch;
        private LoopyReg t;
        private LoopyReg v;
        private byte fineX;
        private byte buffer;
        private ushort currentAddress;
        private PPUControl control;
        private PPUMask mask;
        private PPUStatus status;
        public bool NMI;
        private int scanline;
        private int cycle;
        private byte bgTileID;
        private byte bgTileAttrib;
        private byte bgTileLSB;
        private byte bgTileMSB;
        private ushort bgShifterPatL;
        private ushort bgShifterPatH;
        private ushort bgShifterAttrL;
        private ushort bgShifterAttrH;
        private Bitmap[] patterns = new Bitmap[2];
        public Bitmap currentFrame = new Bitmap(256, 240);
        private Color[] colors = {
            Color.FromArgb(84, 84, 84),
            Color.FromArgb(0, 30, 116),
            Color.FromArgb(8, 16, 44),
            Color.FromArgb(48, 0, 136),
            Color.FromArgb(68, 0, 100),
            Color.FromArgb(92, 0, 48),
            Color.FromArgb(84, 4, 0),
            Color.FromArgb(60, 24, 0),
            Color.FromArgb(32, 42, 0),
            Color.FromArgb(8, 58, 0),
            Color.FromArgb(0, 64, 0),
            Color.FromArgb(0, 60, 0),
            Color.FromArgb(0, 50, 60),
            Color.FromArgb(0, 0, 0),
            Color.FromArgb(0, 0, 0),
            Color.FromArgb(0, 0, 0),

            Color.FromArgb(152, 150, 152),
            Color.FromArgb(8, 76, 196),
            Color.FromArgb(48, 50, 236),
            Color.FromArgb(92, 30, 228),
            Color.FromArgb(136, 20, 176),
            Color.FromArgb(160, 20, 100),
            Color.FromArgb(152, 34, 32),
            Color.FromArgb(120, 60, 0),
            Color.FromArgb(84, 90, 0),
            Color.FromArgb(40, 114, 0),
            Color.FromArgb(8, 124, 0),
            Color.FromArgb(0, 118, 40),
            Color.FromArgb(0, 102, 120),
            Color.FromArgb(0, 0 ,0),
            Color.FromArgb(0, 0, 0),
            Color.FromArgb(0, 0, 0),

            Color.FromArgb(236, 238, 236),
            Color.FromArgb(76, 154, 236),
            Color.FromArgb(120, 124, 236),
            Color.FromArgb(176, 98, 236),
            Color.FromArgb(228, 84, 236),
            Color.FromArgb(236, 88, 180),
            Color.FromArgb(236, 106, 100),
            Color.FromArgb(212, 136, 32),
            Color.FromArgb(160, 170, 0),
            Color.FromArgb(116, 196, 0),
            Color.FromArgb(76, 208, 32),
            Color.FromArgb(56, 204, 108),
            Color.FromArgb(56, 180, 204),
            Color.FromArgb(60, 60, 60),
            Color.FromArgb(0, 0, 0),
            Color.FromArgb(0, 0, 0),

            Color.FromArgb(236, 238, 236),
            Color.FromArgb(168, 204, 236),
            Color.FromArgb(188, 188, 236),
            Color.FromArgb(212, 178, 236),
            Color.FromArgb(236, 174, 236),
            Color.FromArgb(236, 174, 212),
            Color.FromArgb(236, 180, 176),
            Color.FromArgb(228, 196, 144),
            Color.FromArgb(204, 210, 120),
            Color.FromArgb(180, 222, 120),
            Color.FromArgb(168, 226, 144),
            Color.FromArgb(152, 226, 180),
            Color.FromArgb(160, 214, 228),
            Color.FromArgb(160, 162, 160),
            Color.FromArgb(0, 0, 0),
            Color.FromArgb(0, 0 ,0)
        }; //Hardcoded NES palette, intend to add .pal support

        public PPU()
        {
            patterns[0] = new Bitmap(256, 240);
            patterns[1] = new Bitmap(256, 240);
        }
        public void Cycle()
        {
            if (cycle >= 1 && cycle < 257 && scanline >= 0 && scanline < 240)
            {
                currentFrame.SetPixel(cycle - 1, scanline, colors[new Random().Next(colors.Length)]);
            }
            cycle++;
            if (cycle >= 341)
            {
                cycle = 0;
                scanline++;
                if (scanline >= 261)
                {
                    scanline = -1;
                }
            }
        }

        private Color GetColor(byte pal, byte pix)
        {
            byte data = PPURead((ushort) (0x3F00 + (pal << 2) + pix));
            return colors[data & 0x3F];
        }
        public Bitmap GetThingy(byte i, byte palette)
        {
            for (ushort nTileY = 0; nTileY < 16; nTileY++)
            {
                for (ushort nTileX = 0; nTileX < 16; nTileX++)
                {
                    ushort nOffset = (ushort) (nTileY * 256 + nTileX * 16);

                    for (ushort row = 0; row < 8; row++)
                    {
                        byte tile_lsb = PPURead((ushort) (i * 0x1000 + nOffset + row + 0x0000));
                        byte tile_msb = PPURead((ushort) (i * 0x1000 + nOffset + row + 0x0008));


                        for (ushort col = 0; col < 8; col++)
                        {

                             byte pixel = (byte) ((tile_lsb & 0x01) | ((tile_msb & 0x01) << 1));
                             patterns[i].SetPixel
                            (nTileX * 8 + (7 - col), nTileY * 8 + row, GetColor(palette, pixel)
                            );
                        }
                    }
                }
            }


            return patterns[i];
        }
        private void LoadShifters()
        {
            bgShifterPatL = (ushort) ((bgShifterPatL & 0xFF00) | bgTileLSB);
            bgShifterPatH = (ushort) ((bgShifterPatH & 0xFF00) | bgTileMSB);
            bgShifterAttrL = (ushort) ((bgShifterAttrL & 0xFF00) | ((bgTileAttrib & 0x0b01) != 0 ? 0xFF : 0x00));
            bgShifterAttrH = (ushort) ((bgShifterAttrH & 0xFF00) | ((bgTileAttrib & 0x0b10) != 0 ? 0xFF : 0x00));
        }
        private void IncrementX()
        {
            if (mask.Background || mask.Sprites) //Only do anything if rendering is enabled.
            {
                if (v.CoarseX == 31) //Check if nametable boundary crossed
                {
                    v.CoarseX = 0;
                    v.NametableX = !v.NametableX; //Nametable flip
                }
                else v.CoarseX++; //If boundary not crossed, simply increment the scrolling value.
            }
        }

        private void IncrementY()
        {
            if (mask.Background || mask.Sprites)
            {
                if (v.FineY < 7) v.FineY++; //As we render in scanlines, fineY effectively represents a single pixel of scrolling, this simply checks if we've crossed into the next tile.
                else //If we have crossed a tile, fineY must be reset and coarseY (the tile) must be incremented instead.
                {
                    v.FineY = 0;
                    if (v.CoarseY == 29) //Check if we have crossed a vertical nametable boundary
                    {
                        v.CoarseY = 0;
                        v.NametableY = !v.NametableY; //Nametable flip
                    }
                    else if (v.CoarseY == 31) v.CoarseY = 0; //If we have somehow crossed into attribute memory (palette information stored in the nametable) we just return to the start of the nametable
                    else v.CoarseY++; //If no boundary conditions apply, increment as normal.
                }
            }

        }
        private void ReadScrollAndIncrememntX()
        {
            switch ((cycle - 1) % 8)
            {
                case 0:
                    LoadShifters(); //Load shifters with next 8 pixels
                    bgTileID = PPURead((ushort) (0x2000 | (v.Register & 0xFFF)));
                    break;
                case 2:
                    bgTileAttrib = PPURead((ushort) (0x23C0 | ((v.NametableY ? 1 : 0) << 11) |
                                                     ((v.NametableX ? 1 : 0) << 10) | ((v.CoarseY >> 2) << 3) |
                                                     (v.CoarseX >> 2))); //Messy code, handles scrolling.
                    if ((v.CoarseY & 0x02) != 0) bgTileAttrib >>= 4;
                    if ((v.CoarseX & 0x02) != 0) bgTileAttrib >>= 2;
                    bgTileAttrib &= 0x03;
                    break;
                case 4:
                    bgTileLSB = PPURead((ushort) (((control.BackgroundAddress ? 1:0) << 12) + (bgTileID << 4) + v.FineY));
                    break;
                case 6:
                    bgTileMSB = PPURead((ushort) (((control.BackgroundAddress ? 1:0) << 12) + (bgTileID << 4) + v.FineY + 8));
                    break;
                case 7:
                    IncrementX();
                    break;
            }
        }
        public void ConnectCartridge(Cartridge cart)
        {
            cartridge = cart;
        }
        public void PPUWrite(ushort addr, byte data)
        {
            addr &= 0x3FFF;
            if (cartridge.PPUWrite(addr, data)) return;

            else if (addr >= 0 && addr <= 0x1FFF)
            {
                patternTable[(addr & 0x1000) >> 12, addr & 0xFFF] = data;
            }

            else if (addr >= 0x2000 && addr <= 0x3EFF)
            {

            }

            else if (addr >= 0x3F00 && addr <= 0x3FFF)
            {
                addr &= 0x1F;
                if (addr == 0x0010) addr = 0x0000;
                if (addr == 0x0014) addr = 0x0004;
                if (addr == 0x0018) addr = 0x0008;
                if (addr == 0x001C) addr = 0x000C;
                paletteTable[addr] = data;
            }
        }

        public byte PPURead(ushort addr)
        {
            addr &= 0x3FFF; //Mask to maximum addressable range
            byte data = 0;
            if (cartridge.PPURead(addr, ref data)) return data;

            if (addr >= 0 && addr <= 0x1FFF)
            {
                data = patternTable[(addr & 0x1000) >> 12, addr & 0xFFF];
            }

            if (addr >= 0x2000 && addr <= 0x3EFF)
            {

            }

            if (addr >= 0x3F00 && addr <= 0x3FFF)
            {
                if (addr == 0x3F10 || addr == 0x3F14 || addr == 0x3F18 || addr == 0x3F0C)
                    addr -= 0x10;
                paletteTable[(addr - 0x3F00) & 0x1F] = data;
            }

            return data;
        }
        public void CPUWrite(ushort addr, byte data)
        {
            switch (addr)
            {
                case 0:
                    control.Register = data;
                    t.NametableX = control.NameTableX;
                    t.NametableY = control.NameTableY;
                    break;
                case 1:
                    mask.Register = data;
                    break;
                case 5:
                    if (latch == 0)
                    {
                        fineX = (byte) (data & 0x07);
                        t.CoarseX = (byte) (data >> 3);
                        latch = 1;
                    }
                    else
                    {
                        t.FineY = (byte) (data & 0x07);
                        t.CoarseY = (byte) (data >> 3);
                        latch = 0;
                    }
                    break;
                case 6:
                    if (latch == 0)
                    {
                        currentAddress = (ushort) ((currentAddress & 0xFF00) | data << 8);
                        latch = 1;
                    }
                    else
                    {
                        currentAddress = (ushort) ((currentAddress & 0xFF00) | data);
                        v.Register = t.Register;
                        latch = 0;
                    }
                    break;
                case 7:
                    PPUWrite(currentAddress, data);
                    currentAddress++;
                    v.Register += (ushort) (control.Increment ? 32 : 1); //NES auto increments address on read/write
                    break;

            }
        }

        public void Reset()
        {
            fineX = 0;
            latch = 0;
            buffer = 0;
            scanline = 0;
            cycle = 0;
            bgTileID = 0;
            bgTileAttrib = 0;
            bgTileLSB = 0;
            bgTileMSB = 0;
            bgShifterPatH = 0;
            bgShifterPatL = 0;
            bgShifterAttrH = 0;
            bgShifterAttrL = 0;
            status.Clear();
            mask.Clear();
            control.Clear();
            currentAddress = 0;
        }
        public byte CPURead(ushort addr)
        {
            byte data = 0;
            switch (addr)
            {
                case 2:
                    status.VerticalBlank = true;
                    data =  (byte) ((status.Register & 0xE0) | (buffer & 0x1F)); //Last 5 bits of status register are filled with data from the last read/write
                    status.VerticalBlank = false;
                    latch = 0;
                    break;
                case 3:
                    break;
                case 7:
                    data = buffer;
                    buffer = PPURead(currentAddress);
                    if (currentAddress > 0x3F00) data = buffer;
                    break;
            }

            return data;
        }
    }
}
