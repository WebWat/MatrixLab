using MatrixLab;

var a = new Matrix(new double[,] {
    { 1, -1, 7, -5, 3 },
    { 2, 5, -3, 9, 4  },
    { 3, -2, 8, 1, 5  },
    { 4, 6, -4, 2, 6  }
});


a.Minor(2);

Console.ReadLine();
