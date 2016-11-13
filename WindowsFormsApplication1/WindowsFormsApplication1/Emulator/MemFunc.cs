using System;
using static WindowsFormsApplication1.Emulator.Emulator;

namespace WindowsFormsApplication1.Emulator
{
    public static class MemFunc
    {
        internal static void printRegisters()
        {
            registers[6] = PC;
            for (int i = 0; i <= 6; i++)
            {
                //printf("R%d = %o ", i + 1, registers[i]);
                Console.Write("R{0} = {1} ", i, registers[i]);
            }
            Console.WriteLine("");
            //printf("\n");
            //printf("op1 %d op2 %d\n",operand1,operand2);
        }

        internal static bool getBitInWord(ushort inByte, int index)
        {
            return ((inByte >> index) & 1) == 1;
        }

        internal static byte getTriade(ushort inByte, uint index)
        { //gives octal digit. Index 0 starts from left triade.
            byte shift = (byte) (15 - index*3);
            ushort ans = (ushort) (inByte >> shift);
            ans = (ushort) (ans & 7);
            
            return (byte) ans;
        }

        internal static byte twoOctal(byte bOne, byte bTwo)
        {
            return (byte) ((bOne * 8) + (bTwo));
        }

        internal static void setWord(int address, uint word)
        {
            ushort _word = (ushort) word;
            MEMORY[address] = (byte) (_word & 255);
            MEMORY[++address] = (byte) (_word >> 8);
        }

        internal static ushort getWord(int address)
        {
            ushort _word = 0;
            _word += MEMORY[address];
            _word +=(ushort) (MEMORY[address + 1] << 8);
            return _word;
        }

        internal static ushort convToDec(String Octal)
        {
           return (ushort) Convert.ToInt16(Convert.ToString(Convert.ToInt64(Octal, 8), 10));
     
        }

        public static int[] uploadVRAM()
        {
            int [] result = new int[16384];
            for (int i = 16384; i < 32768; i++)
            {
                result[i - 16384] = MEMORY[i];
            }
            return result;
        }
        
    }
}