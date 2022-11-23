using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.NESHardware.Mappers;

namespace WindowsFormsApp2.NESHardware
{
    public class Cartridge
    {
        private Header header;
        private byte[] prgMem;
        private byte[] chrMem;
        private byte mapperID;
        private Mapper mapper;
        public MirrorModes mirroring;
        public Cartridge(string path)
        {
            FileStream rom = File.OpenRead(path);
            byte[] h = new byte[16];
            rom.Read(h, 0, 16);
            header = new Header(h);
            if ((header.Flags6 & 0x04) != 0)
            {
                rom.Seek(512, SeekOrigin.Current);
            }
            ReadInMapper(header);
            mirroring = (MirrorModes) (header.Flags6 & 0x01);
            prgMem = new byte[header.PRGSize * 16384];
            rom.Read(prgMem, 0, header.PRGSize * 16384);
            if (header.CHRSize == 0) chrMem = new byte[8192];
            else chrMem = new byte[header.CHRSize * 8192];
            rom.Read(chrMem, 0, header.CHRSize * 8192);
            SelectMapper(mapperID);
        }

        public void ReadInMapper(Header header)
        {
            byte lowerNyb = (byte) (header.Flags6 >> 4);
            byte upperNyb = (byte) (header.Flags7 >> 4);
            mapperID = (byte) (lowerNyb | upperNyb << 4);
        }

        public void SelectMapper(int mapperID)
        {
            switch (mapperID)
            {
                case 000:
                    mapper = new Mapper000(header.PRGSize, header.CHRSize);
                    break;
            }
        }
        public bool CPURead(ushort addr, ref byte data)
        {
            ushort mapped = 0;
            if (mapper.CPURead(addr, ref mapped))
            {
                data = prgMem[mapped];
                return true;
            }

            return false;
        }

        public bool CPUWrite(ushort addr, byte data)
        {
            ushort mapped = 0;
            if (mapper.CPUWrite(addr, ref mapped, data))
            {
                prgMem[mapped] = data;
                return true;
            }

            return false;
        }

        public bool PPURead(ushort addr, ref byte data)
        {
            ushort mapped = 0;
            if (mapper.PPURead(addr, ref mapped))
            {
                data = chrMem[mapped];
                return true;
            }

            return false;
        }

        public bool PPUWrite(ushort addr, byte data)
        {
            ushort mapped = 0;
            if (mapper.PPUWrite(addr, ref mapped))
            {
                chrMem[mapped] = data;
                return true;
            }

            return false;
        }
    }
    public enum MirrorModes
    {
        Horizontal,
        Vertical
    }

    public class Header
    {
        public int Constant;
        public byte PRGSize;
        public byte CHRSize;
        public byte Flags6;
        public byte Flags7;
        public byte Flags8;
        public byte Flags9;
        public byte Flags10;
        public Int64 Padding;

        public Header(byte[] header)
        {
            Constant = (header[0] << 24) | (header[1] << 16) | (header[2] << 8) | header[3];
            PRGSize = header[4];
            CHRSize = header[5];
            Flags6 = header[6];
            Flags7 = header[7];
            Flags8 = header[8];
            Flags9 = header[9];
            Flags10 = header[10];
            //Padding = BitConverter.ToInt64(header, 11);
        }
    }
}
