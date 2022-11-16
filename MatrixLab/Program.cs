using MatrixLab;

var a = new Matrix(new double[,] {
    { 1, 2, 0, 5,},
    { 2, 4, -1, 0 },
    { -2, -4, 1, 0 },
    { 1, 0, 2, 1 }
});


a.Minor(4);

Console.ReadLine();
