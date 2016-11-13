using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WindowsFormsApplication1
{
    
    public partial class Form1 : Form
    {
        internal const int WIDTH = 128;
        public static Bitmap bmp = new Bitmap(WIDTH, WIDTH);
        public Form1()
        {
            InitializeComponent();
           
            pictureBox1.Image = bmp;
            int x, y;
            for (y = 0; y < WIDTH; y++)
            {
                for (x = 0; x < WIDTH; x++)
                {
                    bmp.SetPixel(x, y, Color.White);
                }
            }

            
            //Program.setBmpCol(bmp, Emulator.Emulator.col);

            Emulator.Emulator.init();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Program.refreshScreen();

            pictureBox1.Image = bmp;
        }
    }


}
