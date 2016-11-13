using System.ComponentModel;
using System.Drawing;

namespace WindowsFormsApplication1.Emulator
{
    public static class Emulator
    {
        private static int MEMSIZE = 65544;
        internal static byte[] MEMORY = new byte[MEMSIZE];
        internal static ushort[] registers = new ushort[] { 01, 32768, 16384, 04, 1000, 00, 00 };
        internal static ushort IR = 00; // Instruction Register
        internal static ushort PC = 49152; // 0140000

        internal const ushort VRAMAddr = 16384;

        internal const ushort pictureAddr = 32768; 
   

        public static Color col = Color.Chartreuse;

        public static void init()
        {
            Pipeline.testPipeline();
            MemFunc.printRegisters();
            run();
        }

        public static void run()
        {
            initDummyPicture();
            while (MEMORY[PC] > 0)
            {
                step();
            }

        }

        public static  void step()
        {
            Pipeline.instrCycle();
            //MemFunc.printRegisters();
        }



        private static void initDummyPicture()
        {
            int i;
            int j = 0;
            for (i = pictureAddr; i <= (pictureAddr + 1024); i++)
            {
                j = (i - pictureAddr)/8;
                MEMORY[i] =(byte) j;
            }
        }



}
}