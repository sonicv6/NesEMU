using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.NESHardware;
using Emulator = WindowsFormsApp2.NESHardware.Emulator;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private Emulator emu = new Emulator();
        private Color[] colors = new Color[]
        {
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
        };
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                if (emu.cpu.pc != 0x8ede)
                {
                    emu.Clock();
                }
            }
            label10.Text = emu.cpu.acc.ToString("X2");
            label11.Text = emu.cpu.x.ToString("X2");
            label12.Text = emu.cpu.y.ToString("X2");
            label13.Text = emu.cpu.status.C.ToString();
            label14.Text = emu.cpu.status.Z.ToString();
            label15.Text = emu.cpu.status.I.ToString();
            label16.Text = emu.cpu.status.D.ToString();
            label17.Text = emu.cpu.status.B.ToString();
            label18.Text = emu.cpu.status.U.ToString();
            label19.Text = emu.cpu.status.V.ToString();
            label20.Text = emu.cpu.status.N.ToString();
            label22.Text = emu.cpu.pc.ToString("X4");
            label25.Text = emu.cpu.pointer.ToString("X2");
            label26.Text = emu.cpu.status.Register.ToString("X2");
            pictureBox1.Image = emu.ppu.currentFrame;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 16; c++)
                {
                    Button btn = new Button();
                    try
                    {
                        btn.BackColor = colors[r * 16 + c];
                    }
                    catch
                    {
                        btn.BackColor = Color.Black;
                    }
                    btn.Width = 30;
                    btn.Height = 30;
                    btn.Location = new Point(c * 30, r * 30);
                    panel1.Controls.Add(btn);
                }
            }
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }
    }
}
