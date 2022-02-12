using System.Diagnostics;
using MatrixLab;

void Test()
{
    using var matrix_1 = new Matrix(5000, 5000, (i, j) => i);

    using var matrix_2 = new Matrix(5000, 5000, (i, j) => i);

    using var matrix_3 = Matrix.CreateEmpty();
}

void M()
{
    Test();
}

M();

Console.WriteLine("End");

Console.ReadLine();
