using System.Diagnostics;
using MatrixLab;

void Test()
{
    //using var a = new Matrix(new int[,] { { 3, 4 }, { 1, 2 } });
    //using var b = new Matrix(new int[,] { { -1, 0 }, { 2, 2 } });


    //var result = (2 * a + b.Transpose()).Transpose();
    using var a = new Matrix(new int[,] { { 3, 4 }, { 1, 2 } });
    using var b = new Matrix(new int[,] { { -1, 0 }, { 2, 2 } });


    var result = (2 * a + b.Transpose()).Transpose() + (- 3) * b * a;

    Console.WriteLine(result);
}

Test();

Console.WriteLine("End");

Console.ReadLine();
