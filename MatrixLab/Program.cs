using MatrixLab;

var a = new Matrix(new double[,] {
  { 1, 1, 1, 0 },
  { 0, 1, 0, 4 },
  { 1, 0, 6, 0 },
  { 0, 2, 0, 3 }
});

var result = a.Transpose().PowN(2);

Console.WriteLine(result);

Console.WriteLine(result.Determinant());

Console.ReadLine();
