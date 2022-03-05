using System.Diagnostics;
using MatrixLab;

void Test()
{
    var b = new Matrix(new int[,] { { 1, 0, 5 }, { 2, 1, 1 }, { 7, 0, 3 } }, "B");
    var a = new Matrix(new int[,] { { 1, 0, 2 }, { 3, 1, 1 }, { 2, 1, 1 } }, "A");

    Console.WriteLine(b);
    Console.WriteLine(a);

    var result = (2 * a + b.Transpose()).Transpose() - 3 * b * a;

    Console.WriteLine(result);
}

Test();

Console.WriteLine("End");

Console.ReadLine();
