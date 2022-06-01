using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    public class MathCalculator
    {
        public int Add(int a, int b)
        {
            checked
            {
                return a + b;
            }
        }

        public int Divide(int a, int b)
        {
            return a / b;
        }

        public float Divide(float a, float b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException();
            }

            return a / b;
        }

        public decimal Divide(decimal a, decimal b)
        {
            return a / b;
        }

        public int Max(int a, int b)
        {
            return (a > b) ? a : b;
        }     
    }
}
