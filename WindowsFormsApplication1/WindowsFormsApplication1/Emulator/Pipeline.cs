using System;
using static WindowsFormsApplication1.Emulator.Emulator;

namespace WindowsFormsApplication1.Emulator
{
    public static class Pipeline
    {
        static byte SINGLE_OPERAND = 0;

        private const byte MODE0 = 00;
        private const byte MODE2 = 02;
        private const byte MODE4 = 04;
        const byte MODE6 = 06;

        // page 32 manual
        // Mode0 0    REg contains operand
        // Mode2 2    Reg contains pointer, then incremented
        // Mode4 4    Reg decremented then used as pointer
        // Mode6 6    X+Reg = pointer

        private const byte INC = 42; //052 in octal
        private const byte SOB = 63; //077


        internal static bool isRegisterOperator1 = false;
        internal static bool isRegisterOperator2 = false;
        internal static ushort operand1 = 00;
        internal static ushort operand2 = 00;

        static bool tempIsRegisterOperator = false;


        internal static void testPipeline()
        {
            Console.WriteLine("Hello pipeline!");// Registers: 01,02,03,04,1000,00,00

            MemFunc.setWord(PC, MemFunc.convToDec("012122")); // lab: MOV(R1) +, (R2) +
            MemFunc.setWord(PC+2, MemFunc.convToDec("077402")); //  SOB R4, lab

            //MemFunc.setWord(PC, MemFunc.convToDec("010102"));            // MOVB R1,R2 
            //MemFunc.setWord(PC + 2, MemFunc.convToDec("005205"));          // INC R5;
            //MemFunc.setWord(PC + 4, MemFunc.convToDec("077402"));          // SOB R3,1





        }

        internal static void instrCycle()
        {
            Pipeline.fetch();
            decode();

        }

        static void fetch()
        {
            IR = MemFunc.getWord(PC);
            PC = (ushort) (PC + 2);
        }

        static void decode()
        {
            bool MSB = (MemFunc.getBitInWord(IR, 15));
            byte oct1 = MemFunc.getTriade(IR, 1);
            byte oct2 = MemFunc.getTriade(IR, 2);
            byte oct3 = MemFunc.getTriade(IR, 3);//считаем триады слева

            if (!MSB)
            {                                                 // Most-significant bit
                if (oct1 == 0)
                {                                            // If single-operand operation
                    switch (MemFunc.twoOctal(oct2, oct3))
                    {
                        case INC:
                            Operation.inc();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (MemFunc.getTriade(IR, 1))
                    {                         // Octal digit 1
                        case 1: Operation.mov(); break;

                    }
                    switch (MemFunc.twoOctal(oct1, oct2))
                    {
                        case SOB:
                            Operation.sob();
                            break;
                        default:
                            break;
                    }
                }
            }



        }

        static ushort readOperator(byte mode, byte regNum)
        {
            ushort operand = 0;
            ushort reg = registers[regNum];

            switch (mode)
            {
                case MODE0:
                    operand = regNum;
                    tempIsRegisterOperator = true;
                    break;
                case MODE2:
                    operand = reg;
                    registers[regNum]++;
                    tempIsRegisterOperator = false;
                    break;
                case MODE4:
                    reg = --registers[regNum];
                    operand = MEMORY[reg];
                    tempIsRegisterOperator = false;
                    break;
                case MODE6: break;
                default: Console.WriteLine("Error1");
                    break;
            }
            return operand;

        }

        internal static void singleReadOperand()
        {
            byte mode = MemFunc.getTriade(IR, 4);
            byte regNum = MemFunc.getTriade(IR, 5);
            operand1 = readOperator(mode, regNum);
            isRegisterOperator1 = tempIsRegisterOperator;
        }
        internal static void doubleReadOperand()
        {
            byte mode = MemFunc.getTriade(IR, 2);
            byte regNum = MemFunc.getTriade(IR, 3);
            operand1 = readOperator(mode, regNum);
            isRegisterOperator1 = tempIsRegisterOperator;

            mode = MemFunc.getTriade(IR, 4);
            regNum = MemFunc.getTriade(IR, 5);
            operand2 = readOperator(mode, regNum);
            isRegisterOperator2 = tempIsRegisterOperator;

        }
    }
}