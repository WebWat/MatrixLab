using System.Diagnostics;
using MatrixLab;

Random ran = new Random();
//var a = new Matrix(new double[,] { { 2, 2, 8, 3, 9 }, { 4, 8, 9, 39, 39 }, { 3, 8, 2, 6, 1 }, { 2, 8, 47, -3, 32 } });
var a = new Matrix(new double[,] { { 1, 2 }, { 0, 0 } });

var result = Matrix.Gauss(a);

Console.WriteLine(result);

Console.ReadLine();
