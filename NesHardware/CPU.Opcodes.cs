using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace NesHardware
{
    public partial class CPU
    {
        private void Execute(byte code)
        {
            switch (code)
            {
                //ADC codes
                case 0x69:
                    cycles += 2;
                    IMM();
                    ADC();
                    break;
                case 0x65:
                    cycles += 3;
                    ZP0();
                    ADC();
                    break;
                case 0x75:
                    cycles += 4;
                    ZPX();
                    ADC();
                    break;
                case 0x6D:
                    cycles += 4;
                    ABS();
                    ADC();
                    break;
                case 0x7D:
                    cycles += 4;
                    ABX();
                    ADC();
                    break;
                case 0x79:
                    cycles += 4;
                    ABY();
                    ADC();
                    break;
                case 0x61:
                    cycles += 6;
                    INX();
                    ADC();
                    break;
                case 0x71:
                    cycles += 5;
                    INY();
                    ADC();
                    break;
                //AND codes
                case 0x29:
                    cycles += 2;
                    IMM();
                    AND();
                    break;
                case 0x25:
                    cycles += 3;
                    ZP0();
                    AND();
                    break;
                case 0x35:
                    cycles += 4;
                    ZPX();
                    AND();
                    break;
                case 0x2D:
                    cycles += 4;
                    ABS();
                    AND();
                    break;
                case 0x3D:
                    cycles += 4;
                    ABX();
                    AND();
                    break;
                case 0x39:
                    cycles += 4;
                    ABY();
                    AND();
                    break;
                case 0x21:
                    cycles += 6;
                    INX();
                    AND();
                    break;
                case 0x31:
                    cycles += 5;
                    INY();
                    AND();
                    break;
                // ASL codes
                case 0x0A:
                    cycles += 2;
                    IMP();
                    ASL();
                    break;
                case 0x06:
                    cycles += 5;
                    ZP0();
                    ASL();
                    break;
                case 0x16:
                    cycles += 6;
                    ZPX();
                    ASL();
                    break;
                case 0x0E:
                    cycles += 6;
                    ABS();
                    ASL();
                    break;
                case 0x1E:
                    cycles += 7;
                    ABX();
                    ASL();
                    break;
                //BCC codes
                case 0x90:
                    cycles += 2;
                    REL();
                    BCC();
                    break;
                //BCS codes
                case 0xB0:
                    cycles += 2;
                    REL();
                    BCS();
                    break;
                //BEQ codes
                case 0xF0:
                    cycles += 2;
                    REL();
                    BEQ();
                    break;
                //BIT codes
                case 0x24:
                    cycles += 3;
                    ZP0();
                    BIT();
                    break;
                case 0x2C:
                    cycles += 4;
                    ABS();
                    BIT();
                    break;
                //BMI codes
                case 0x30:
                    cycles += 2;
                    REL();
                    BMI();
                    break;
                //BNE codes
                case 0xD0:
                    cycles += 2;
                    REL();
                    BNE();
                    break;
                //BPL codes
                case 0x10:
                    cycles += 2;
                    REL();
                    BPL();
                    break;
                //BRK codes
                case 0x00:
                    cycles += 7;
                    IMP();
                    BRK();
                    break;
                //BVC codes
                case 0x50:
                    cycles += 2;
                    REL();
                    BVC();
                    break;
                //BVS codes
                case 0x70:
                    cycles += 2;
                    REL();
                    BVS();
                    break;
                //CLC codes
                case 0x18:
                    cycles += 2;
                    IMP();
                    CLC();
                    break;
                //CLD codes
                case 0xD8:
                    cycles += 2;
                    IMP();
                    CLD();
                    break;
                //CLI codes
                case 0x58:
                    cycles += 2;
                    IMP();
                    CLI();
                    break;
                //CLV codes
                case 0xB8:
                    cycles += 2;
                    IMP();
                    CLV();
                    break;
                //CMP codes
                case 0xC9:
                    cycles += 2;
                    IMM();
                    CMP();
                    break;
                case 0xC5:
                    cycles += 3;
                    ZP0();
                    CMP();
                    break;
                case 0xD5:
                    cycles += 4;
                    ZPX();
                    CMP();
                    break;
                case 0xCD:
                    cycles += 4;
                    ABS();
                    CMP();
                    break;
                case 0xDD:
                    cycles += 4;
                    ABX();
                    CMP();
                    break;
                case 0xD9:
                    cycles += 4;
                    ABS();
                    CMP();
                    break;
                case 0xC1:
                    cycles += 6;
                    INX();
                    CMP();
                    break;
                case 0xD1:
                    cycles += 5;
                    INY();
                    CMP();
                    break;
                //CPX codes
                case 0xE0:
                    cycles += 2;
                    IMM();
                    CPX();
                    break;
                case 0xE4:
                    cycles += 3;
                    ZP0();
                    CPX();
                    break;
                case 0xEC:
                    cycles += 4;
                    ABS();
                    CPX();
                    break;
                //CPY codes
                case 0xC0:
                    cycles += 2;
                    IMM();
                    CPY();
                    break;
                case 0xC4:
                    cycles += 3;
                    ZP0();
                    CPY();
                    break;
                case 0xCC:
                    cycles += 4;
                    ABS();
                    CPY();
                    break;
                //DEC codes
                case 0xC6:
                    cycles += 5;
                    ZP0();
                    DEC();
                    break;
                case 0xD6:
                    cycles += 6;
                    ZPX();
                    DEC();
                    break;
                case 0xCE:
                    cycles += 6;
                    ABS();
                    DEC();
                    break;
                case 0xDE:
                    cycles += 7;
                    ABX();
                    DEC();
                    break;
                //DEX codes
                case 0xCA:
                    cycles += 2;
                    IMP();
                    DEX();
                    break;
                //DEY codes
                case 0x88:
                    cycles += 2;
                    IMP();
                    DEY();
                    break;
                //EOR codes
                case 0x49:
                    cycles += 2;
                    IMM();
                    EOR();
                    break;
                case 0x45:
                    cycles += 3;
                    ZP0();
                    EOR();
                    break;
                case 0x55:
                    cycles += 4;
                    ZPX();
                    EOR();
                    break;
                case 0x4D:
                    cycles += 4;
                    ABS();
                    EOR();
                    break;
                case 0x5D:
                    cycles += 4;
                    ABX();
                    EOR();
                    break;
                case 0x59:
                    cycles += 4;
                    ABY();
                    EOR();
                    break;
                case 0x41:
                    cycles += 6;
                    INX();
                    EOR();
                    break;
                case 0x51:
                    cycles += 5;
                    INY();
                    EOR();
                    break;
                //INC codes
                case 0xE6:
                    cycles += 5;
                    ZP0();
                    INC();
                    break;
                case 0xF6:
                    cycles += 6;
                    ZPX();
                    INC();
                    break;
                case 0xEE:
                    cycles += 6;
                    ABS();
                    INC();
                    break;
                case 0xFE:
                    cycles += 7;
                    ABX();
                    INC();
                    break;
                //INX codes
                case 0xE8:
                    cycles += 2;
                    IMP();
                    INCX();
                    break;
                //INY codes
                case 0xC8:
                    cycles += 2;
                    IMP();
                    INCY();
                    break;
                //JMP codes
                case 0x4C:
                    cycles += 3;
                    ABS();
                    JMP();
                    break;
                case 0x6C:
                    cycles += 5;
                    IND();
                    JMP();
                    break;
                //JSR codes
                case 0x20:
                    cycles += 6;
                    ABS();
                    JSR();
                    break;
                //LDA codes
                case 0xA9:
                    cycles += 2;
                    IMM();
                    LDA();
                    break;
                case 0xA5:
                    cycles += 3;
                    ZP0();
                    LDA();
                    break;
                case 0xB5:
                    cycles += 4;
                    ZPX();
                    LDA();
                    break;
                case 0xAD:
                    cycles += 4;
                    ABS();
                    LDA();
                    break;
                case 0xBD:
                    cycles += 4;
                    ABX();
                    LDA();
                    break;
                case 0xB9:
                    cycles += 4;
                    ABY();
                    LDA();
                    break;
                case 0xA1:
                    cycles += 6;
                    INX();
                    LDA();
                    break;
                case 0xB1:
                    cycles += 5;
                    INY();
                    LDA();
                    break;
                //LDX codes
                case 0xA2:
                    cycles += 2;
                    IMM();
                    LDX();
                    break;
                case 0xA6:
                    cycles += 3;
                    ZP0();
                    LDX();
                    break;
                case 0xB6:
                    cycles += 4;
                    ZPY();
                    LDX();
                    break;
                case 0xAE:
                    cycles += 4;
                    ABS();
                    LDX();
                    break;
                case 0xBE:
                    cycles += 4;
                    ABY();
                    LDX();
                    break;
                //LDY codes
                case 0xA0:
                    cycles += 2;
                    IMM();
                    LDY();
                    break;
                case 0xA4:
                    cycles += 3;
                    ZP0();
                    LDY();
                    break;
                case 0xB4:
                    cycles += 4;
                    ZPX();
                    LDY();
                    break;
                case 0xAC:
                    cycles += 4;
                    ABS();
                    LDY();
                    break;
                case 0xBC:
                    cycles += 4;
                    ABX();
                    LDY();
                    break;
                //LSR codes
                case 0x4A:
                    cycles += 2;
                    IMP();
                    LSR();
                    break;
                case 0x46:
                    cycles += 5;
                    ZP0();
                    LSR();
                    break;
                case 0x56:
                    cycles += 6;
                    ZPX();
                    LSR();
                    break;
                case 0x4E:
                    cycles += 6;
                    ABS();
                    LSR();
                    break;
                case 0x5E:
                    cycles += 7;
                    ABX();
                    LSR();
                    break;
                //NOP codes
                case 0xEA:
                    cycles += 2;
                    IMP();
                    NOP();
                    break;
                //ORA codes
                case 0x09:
                    cycles += 2;
                    IMM();
                    ORA();
                    break;
                case 0x05:
                    cycles += 3;
                    ZP0();
                    ORA();
                    break;
                case 0x15:
                    cycles += 4;
                    ZPX();
                    ORA();
                    break;
                case 0x0D:
                    cycles += 4;
                    ABS();
                    ORA();
                    break;
                case 0x1D:
                    cycles += 4;
                    ABX();
                    ORA();
                    break;
                case 0x19:
                    cycles += 4;
                    ABY();
                    ORA();
                    break;
                case 0x01:
                    cycles += 6;
                    INX();
                    ORA();
                    break;
                case 0x11:
                    cycles += 5;
                    INY();
                    ORA();
                    break;
                //PHA codes
                case 0x48:
                    cycles += 3;
                    IMP();
                    PHA();
                    break;
                //PHP codes
                case 0x08:
                    cycles += 3;
                    IMP();
                    PHP();
                    break;
                //PLA codes
                case 0x68:
                    cycles += 4;
                    IMP();
                    PLA();
                    break;
                //PLP codes
                case 0x28:
                    cycles += 4;
                    IMP();
                    PLP();
                    break;
                //ROL codes
                case 0x2A:
                    cycles += 2;
                    IMP();
                    ROL();
                    break;
                case 0x26:
                    cycles += 5;
                    ZP0();
                    ROL();
                    break;
                case 0x36:
                    cycles += 6;
                    ZPX();
                    ROL();
                    break;
                case 0x2E:
                    cycles += 6;
                    ABS();
                    ROL();
                    break;
                case 0x3E:
                    cycles += 7;
                    ABX();
                    ROL();
                    break;
                //ROR codes
                case 0x6A:
                    cycles += 2;
                    IMP();
                    ROR();
                    break;
                case 0x66:
                    cycles += 5;
                    ZP0();
                    ROR();
                    break;
                case 0x76:
                    cycles += 6;
                    ZPX();
                    ROR();
                    break;
                case 0x6E:
                    cycles += 6;
                    ABS();
                    ROR();
                    break;
                case 0x7E:
                    cycles += 7;
                    ABX();
                    ROR();
                    break;
                //RTI codes
                case 0x40:
                    cycles += 6;
                    IMP();
                    RTI();
                    break;
                //RTS codes
                case 0x60:
                    cycles += 6;
                    IMP();
                    RTS();
                    break;
                //SBC codes
                case 0xE9:
                    cycles += 2;
                    IMM();
                    SBC();
                    break;
                case 0xE5:
                    cycles += 3;
                    ZP0();
                    SBC();
                    break;
                case 0xF5:
                    cycles += 4;
                    ZPX();
                    SBC();
                    break;
                case 0xED:
                    cycles += 4;
                    ABS();
                    SBC();
                    break;
                case 0xFD:
                    cycles += 4;
                    ABX();
                    SBC();
                    break;
                case 0xF9:
                    cycles += 4;
                    ABY();
                    SBC();
                    break;
                case 0xE1:
                    cycles += 6;
                    INX();
                    SBC();
                    break;
                case 0xF1:
                    cycles += 5;
                    INY();
                    SBC();
                    break;
                //SEC codes
                case 0x38:
                    cycles += 2;
                    IMP();
                    SEC();
                    break;
                //SED codes
                case 0xF8:
                    cycles += 2;
                    IMP();
                    SED();
                    break;
                //SEI codes
                case 0x78:
                    cycles += 2;
                    IMP();
                    SEI();
                    break;
                //STA codes
                case 0x85:
                    cycles += 3;
                    ZP0();
                    STA();
                    break;
                case 0x95:
                    cycles += 4;
                    ZPX();
                    STA();
                    break;
                case 0x8D:
                    cycles += 4;
                    ABS();
                    STA();
                    break;
                case 0x9D:
                    cycles += 5;
                    ABX();
                    STA();
                    break;
                case 0x99:
                    cycles += 5;
                    ABY();
                    STA();
                    break;
                case 0x81:
                    cycles += 6;
                    INX();
                    STA();
                    break;
                case 0x91:
                    cycles += 6;
                    INY();
                    STA();
                    break;
                //STX codes
                case 0x86:
                    cycles += 3;
                    ZP0();
                    STX();
                    break;
                case 0x96:
                    cycles += 4;
                    ZPY();
                    STX();
                    break;
                case 0x8E:
                    cycles += 4;
                    ABS();
                    STX();
                    break;
                //STY codes
                case 0x84:
                    cycles += 3;
                    ZP0();
                    STY();
                    break;
                case 0x94:
                    cycles += 4;
                    ZPX();
                    STY();
                    break;
                case 0x8C:
                    cycles += 4;
                    ABS();
                    STY();
                    break;
                //TAX codes
                case 0xAA:
                    cycles += 2;
                    IMP();
                    TAX();
                    break;
                //TAY codes
                case 0xA8:
                    cycles += 2;
                    IMP();
                    TAY();
                    break;
                //TSX codes
                case 0xBA:
                    cycles += 2;
                    IMP();
                    TSX();
                    break;
                //TXA codes
                case 0x8A:
                    cycles += 2;
                    IMP();
                    TXA();
                    break;
                //TXS codes
                case 0x9A:
                    cycles += 2;
                    IMP();
                    TXS();
                    break;
                //TYA codes
                case 0x98:
                    cycles += 2;
                    IMP();
                    TYA();
                    break;
            }
        }
    }
}
