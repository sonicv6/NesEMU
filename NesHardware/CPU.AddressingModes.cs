using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace NesHardware
{
    public partial class CPU
    {
        public void IMP()
        {
            fetched = acc;
            addressMode = AddressMode.IMP;
        }

        public void IMM()
        {
            absAddr = pc++;
            addressMode = AddressMode.IMM;
        }

        public void ZP0()
        {
            absAddr = Read(pc++);
            absAddr &= 0x00FF;
            addressMode = AddressMode.ZP0;
        }

        public void ZPX()
        {
            absAddr = Read((ushort) (pc++ + x));
            absAddr &= 0x00FF;
            addressMode = AddressMode.ZPX;
        }
        public void ZPY()
        {
            absAddr = Read((ushort) (pc++ + y));
            absAddr &= 0x00FF;
            addressMode = AddressMode.ZPY;
        }

        public void ABS()
        {
            absAddr = Read((ushort) (Read(pc++) << 8 | Read(pc++)));
            addressMode = AddressMode.ABS;
        }
        public void ABX()
        {
            ushort highByte = (ushort) (Read(pc++) << 8);
            absAddr = (ushort) (highByte | Read(pc++) + x);
            if ((absAddr & 0xFF00) != highByte) cycles++;
            addressMode = AddressMode.ABX;
        }

        public void ABY()
        {
            ushort highByte = (ushort) (Read(pc++) << 8);
            absAddr = (ushort) (highByte | Read(pc++) + y);
            if ((absAddr & 0xFF00) != highByte) cycles++;
            addressMode = AddressMode.ABY;
        }

        public void IND()
        {
            ushort temp = (ushort) ((Read(pc++) << 8) | Read(pc++));
            absAddr = (ushort) (Read((ushort) ((temp + 1) << 8)) | Read(temp));
            addressMode = AddressMode.IND;
        }

        public void INX()
        {
            ushort temp = Read(pc++);
            absAddr = (ushort) (Read((ushort) ((ushort) ((temp + x + 1) & 0x00FF) << 8)) | Read((ushort) ((temp + x) & 0x00FF)));
            addressMode = AddressMode.INX;
        }

        public void INY()
        {
            ushort temp = Read(pc++);
            ushort highByte = (ushort) ((ushort) (Read((ushort) (temp + 1)) & 0x00FF) << 8);
            absAddr = (ushort) ((highByte | Read((ushort) (temp & 0x00FF))) + y);
            if ((absAddr & 0x0FF) != highByte) cycles++;
            addressMode = AddressMode.INY;
        }

        public void REL()
        {
            relAddr = Read(pc++);
            if ((relAddr & 0x80) == 1)
            {
                relAddr |= 0xFF00;
            }

            addressMode = AddressMode.REL;
        }
    }
}
