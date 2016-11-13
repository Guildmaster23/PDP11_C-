using static WindowsFormsApplication1.Emulator.Emulator;
using static WindowsFormsApplication1.Emulator.Pipeline;

namespace WindowsFormsApplication1.Emulator
{
    public static class Operation
    {
        internal static void mov()
        {
            Pipeline.doubleReadOperand();
            if (Pipeline.isRegisterOperator2)
            {
                if (Pipeline.isRegisterOperator1)
                {
                    registers[operand2] = registers[operand1];
                }
                else registers[operand2] = MEMORY[operand2];
            }
            else
            {
                if (Pipeline.isRegisterOperator1)
                {
                    MEMORY[operand2] =(byte) operand1; //////////////WRONG
                }
                else MEMORY[operand2] = MEMORY[operand1];
            }
        }

        internal static void inc()
        {
            Pipeline.singleReadOperand();
            if (Pipeline.isRegisterOperator1)
            {
                registers[operand1]++;
            }
            else MEMORY[operand1]++;
        }

        internal static void sob()
        {
            byte regNum = MemFunc.getTriade(IR, 3);
            byte offset = MemFunc.twoOctal(MemFunc.getTriade(IR, 4), MemFunc.getTriade(IR, 5));
            if ((--registers[regNum]) > 0)
            {
                PC -= (ushort) (2 * offset);
            }
        }
    }
}