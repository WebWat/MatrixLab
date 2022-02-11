using System.Diagnostics;
using MatrixLab;

var matrix_1 = new Matrix(new int[,]
{
    { 1, -1, 3 },
    { 2, 0, 2 },
});

var matrix_2 = new Matrix(new int[,]
{
    { 0, 1, 2, 0 },
    { -1, 2, 0, 0 },
    { 1, 2, 1, 0 },
});

var result = matrix_1 * matrix_2;

Console.WriteLine(result);

Console.WriteLine("End");
