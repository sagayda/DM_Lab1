using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_Labs
{
    public static class SLAE
    {
        public static void Solve(float[,] mainMatrix, float[] vector)
        {
            if (mainMatrix.GetLength(0) == mainMatrix.GetLength(1))
            {
                QuadraticEquationsSolve(mainMatrix, vector);
                return;
            }
            else
            {
                return;
            }
        }

        private static void QuadraticEquationsSolve (float[,] mainMatrix, float[] vector)
        {

            StraightCourse(mainMatrix);

        }

        private static void StraightCourse(float[,] mainMatrix)
        {

        }
    }
}
