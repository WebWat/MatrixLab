using System.Diagnostics;
using MatrixLab;

void Test()
{
    var a = new Matrix(new int[,] { { 1, 2 }, { 3, 4 } }, "A");

    Console.WriteLine(a);

    var result = a.Inverse();

    Console.WriteLine(result.Item1);
    Console.WriteLine(result.Item2);
}

Test();

Console.WriteLine("End");

Console.ReadLine();
