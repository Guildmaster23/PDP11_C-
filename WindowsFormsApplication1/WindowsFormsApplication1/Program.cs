using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApplication1.Emulator.Emulator;

namespace WindowsFormsApplication1
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]


        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


        }


        public static void setBmpCol(Bitmap bmp, Color color)
        {
            int x, y;
            for (y = 0; y < 256; y++)
            {
                for (x = 0; x < 256; x++)
                {
                    bmp.SetPixel(x, y, color);
                }
            }
        }

        public static void refreshScreen()
        {
            int[] VRAM = Emulator.MemFunc.uploadVRAM();

            int x, y;
            for (y = 0; y < 128; y++)
            {
                for (x = 0; x < 128; x++)
                {
                    int rgb = VRAM[y * 128 + x];
                    Form1.bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb)); //
                }
            }
        }
}
}
