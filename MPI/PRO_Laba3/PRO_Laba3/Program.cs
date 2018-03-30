using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPI;
using MathNet.Numerics.LinearAlgebra;

namespace PRO_lab3
{
    [Serializable]
    public class MATRIX_INITIALIZATION_HEADER
    {
        public Matrix<double> subMatrixA;
        public Matrix<double> subMatrixB;
    }
    [Serializable]
    public class MATRIX_RESULT
    {
        public Matrix<double> value;
    }

    class Program
    {
        public static int numberOfProcesses = 8;
        public static int numberOfColumns = 165;
        public static int numberOfRows = 840;

        public static int block420x41_rows = 420;
        public static int block420x41_columns = 41;

        public static int block420x42_rows = 420;
        public static int block420x42_columns = 42;

        static void Main(string[] args)
        {
            using (new MPI.Environment(ref args))
            {
                Intracommunicator comm = Communicator.world;
                if (comm.Size != numberOfProcesses && comm.Rank == 0)
                {
                    Console.WriteLine("Please enter correct number of processes");
                    return;
                }

                if (comm.Rank == 0)
                {
                    Matrix<double> originMatrixA = Matrix<double>.Build.Dense(numberOfRows, numberOfColumns, (i, j) => 1);
                    Matrix<double> originMatrixB = Matrix<double>.Build.Dense(numberOfColumns, 1, (i, j) => 1);
                    Matrix<double>[] subMatrixA = new Matrix<double>[numberOfProcesses];
                    Matrix<double>[] subMatrixB = new Matrix<double>[numberOfProcesses];
                    MATRIX_RESULT temp = new MATRIX_RESULT();
                    Matrix<double> tempSubC;
                    Matrix<double> resultC;


                    createSubMatrix(ref originMatrixA, ref originMatrixB, ref subMatrixA, ref subMatrixB);
                    Console.WriteLine(originMatrixA.ToString(numberOfRows, numberOfColumns));
                    Console.WriteLine(originMatrixB.ToString(numberOfColumns, 1));
                    printSubMatrices(ref subMatrixA, ref subMatrixB);

                    for (int i = 1; i < numberOfProcesses; i++)
                    {
                        MATRIX_INITIALIZATION_HEADER data = new MATRIX_INITIALIZATION_HEADER();
                        data.subMatrixA = subMatrixA[i];
                        data.subMatrixB = subMatrixB[i];
                        comm.Send<MATRIX_INITIALIZATION_HEADER>(data, i, 0);
                    }

                    tempSubC = subMatrixA[0].Multiply(subMatrixB[0]);

                    for (int cnt = 2; cnt < numberOfProcesses; cnt += 2) //0, 2, 4, 6
                    {
                        temp = comm.Receive<MATRIX_RESULT>(cnt, 0);
                        tempSubC = tempSubC.Add(temp.value);
                    }

                    temp = comm.Receive<MATRIX_RESULT>(1, 0);       //1 
                    resultC = tempSubC.Stack(temp.value);

                    Console.WriteLine(resultC.ToString(numberOfRows, 1));

                }
                else
                {
                    MATRIX_RESULT result = new MATRIX_RESULT();
                    MATRIX_INITIALIZATION_HEADER data = comm.Receive<MATRIX_INITIALIZATION_HEADER>(0, 0);
                    result.value = data.subMatrixA.Multiply(data.subMatrixB);

                    if (comm.Rank % 2 == 0) //even
                    {
                        comm.Send<MATRIX_RESULT>(result, 0, 0);
                    }
                    else                     //odd
                    {
                        if (comm.Rank == 1)
                        {
                            MATRIX_RESULT temp = new MATRIX_RESULT();

                            for (int cnt = 3; cnt < numberOfProcesses; cnt += 2) //3, 5, 7
                            {
                                temp = comm.Receive<MATRIX_RESULT>(cnt, 0);
                                result.value = result.value.Add(temp.value);
                            }
                            comm.Send<MATRIX_RESULT>(result, 0, 0);
                        }
                        else        //3, 5, 7
                        {
                            comm.Send<MATRIX_RESULT>(result, 1, 0);
                        }
                    }
                }


            }
        }

        public static void printSubMatrices(ref Matrix<double>[] subMatrixA, ref Matrix<double>[] subMatrixB)
        {
            Console.WriteLine("PRINTING MATRIX A");
            for (int i = 0; i < numberOfProcesses; i++)
            {
                Console.WriteLine("I = " + i.ToString());
                if (i < 6)
                    Console.WriteLine(subMatrixA[i].ToString(block420x41_rows, block420x41_columns));
                else
                    Console.WriteLine(subMatrixA[i].ToString(block420x42_rows, block420x42_columns));
            }

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("PRINTING MATRIX B");

            for (int i = 0; i < numberOfProcesses; i++)
            {
                Console.WriteLine("I = " + i.ToString());
                if (i < 6)
                    Console.WriteLine(subMatrixB[i].ToString(block420x41_columns, 1));
                else
                    Console.WriteLine(subMatrixB[i].ToString(block420x42_columns, 1));
            }

        }

        public static void createSubMatrix(ref Matrix<double> originMatrixA, ref Matrix<double> originMatrixB, ref Matrix<double>[] subMatrixA, ref Matrix<double>[] subMatrixB)
        {
            for (int cnt = 0; cnt < numberOfProcesses; cnt++)
            {
                if (cnt < 6)  //0-5
                {
                    subMatrixA[cnt] = originMatrixA.SubMatrix(block420x41_rows * (cnt % 2), block420x41_rows, block420x41_columns * (cnt / 2), block420x41_columns);
                    subMatrixB[cnt] = originMatrixB.SubMatrix(block420x41_columns * (cnt / 2), block420x41_columns, 0, 1);
                }
                else        //6 and 7
                {
                    subMatrixA[cnt] = originMatrixA.SubMatrix(block420x42_rows * (cnt % 2), block420x42_rows, block420x41_columns * 3, block420x42_columns);
                    subMatrixB[cnt] = originMatrixB.SubMatrix(block420x41_columns * 3, block420x42_columns, 0, 1);
                }

            }

        }

    }
}
