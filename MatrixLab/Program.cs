using MatrixLab;

var a = new Matrix(new double[,] {
    { 2, 1, 3, 0 },
    { 1, 4, 2, 1 },
    { 3, 2, 2, 6 },
    { 8, 5, 7, 12 }
});

var b = a;


Console.WriteLine(a.Rank());

Console.ReadLine();
 