using System.Diagnostics;
using MatrixLab;
using Spectre.Console;

void Test()
{
    var b = new Matrix(new int[,] { { 2, 3, 7, 10, 13 }, { 1, 2, 3, 4, 5 }, { 3, 5, 11, 16, 21 }, { 2, -7, 7, 7, 2 }, { 1, 4, 5, 3, 10 } }, "B");

    Console.WriteLine(b);
    Console.WriteLine(b.Determinant());
}

Test();

//Console.WriteLine("End");

Console.ReadLine();
