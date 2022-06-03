﻿using System.Text;

namespace MatrixLab
{
    public sealed class Matrix
    {
        private const int SpacesForToString = 20;

        public int Rows
        {
            get { return _array.GetLength(0); }
        }

        public int Columns
        {
            get { return _array.GetLength(1); }
        }

        public string Name { get; set; } = "NONE";

        private double[,] _array;
        public double[,] Array
        {
            get { return _array; }
        }

        public int Length
        {
            get { return _array.Length; }
        }

        public Matrix(double[,] array, string name = default)
        {
            _array = array;
            Name = name == default ? Name : name;
        }

        public Matrix(int row, int column, string name = default)
        {
            _array = new double[row, column];
            Name = name == default ? Name : name;
        }

        public Matrix(int row, int column, Func<int, int, double> func, string name = default)
        {
            _array = new double[row, column];
            Name = name == default ? Name : name;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    this[i, j] = func(i, j);
                }
            }
        }

        public double this[int row, int column]
        {
            get { return _array[row, column]; }
            set { _array[row, column] = value; }
        }

        public static implicit operator double[,](Matrix matrix)
        {
            return matrix.Array;
        }

        public static implicit operator Matrix(double[,] array)
        {
            return new Matrix(array);
        }

        public static implicit operator Matrix(int[,] array)
        {
            double[,] newArray = new double[array.GetLength(0), array.GetLength(1)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    newArray[i, j] = array[i, j];
                }
            }

            return new Matrix(newArray);
        }

        public static Matrix operator -(Matrix matrix)
        {
            var result = new Matrix(matrix.Rows, matrix.Rows, matrix.Name);

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = -matrix[i, j];
                }
            }

            return result;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            return m1 + (-m2);
        }

        public static Matrix operator +(Matrix matrix_1, Matrix matrix_2)
        {
            if (matrix_1.Columns != matrix_2.Columns || matrix_1.Rows != matrix_2.Rows)
                throw new ArgumentException("Matrices sizes must be equals");

            var result = new Matrix(matrix_1.Rows, matrix_1.Rows);

            for (int i = 0; i < matrix_1.Rows; i++)
            {
                for (int j = 0; j < matrix_1.Columns; j++)
                {
                    result[i, j] = matrix_1[i, j] + matrix_2[i, j];
                }
            }

            return result;
        }

        public static Matrix operator * (Matrix matrix_1, Matrix matrix_2)
        {
            if (matrix_1.Columns != matrix_2.Rows)
                throw new ArgumentException("The columns of the first matrix " +
                    "must be equal to the rows of the second matrix");

            var result = new Matrix(matrix_1.Rows, matrix_2.Columns);

            for (int i = 0; i < matrix_1.Rows; i++)
            {
                int row = 0;

                while (row < matrix_2.Columns)
                {
                    for (int j = 0; j < matrix_1.Columns; j++)
                    {
                        result[i, row] += matrix_1[i, j] * matrix_2[j, row];
                    }

                    row++;
                }

            }

            return result;
        }

        public static Matrix operator * (double x, Matrix matrix)
        {
            var result = new Matrix(matrix.Rows, matrix.Rows, matrix.Name);


            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = x * matrix[i, j];
                }
            }

            return result;
        }

        public static Matrix CreateEmpty()
        {
            return new Matrix(0, 0);
        }

        public override string ToString()
        {
            StringBuilder result = new();

            string operation = $"{Name} = ";
            result.Append(operation);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    string main = $"{this[i, j]:f4}";
                    if (j == 0)
                        result.Append($"{new string(' ', i == 0 && j == 0 ? 0 : operation.Length)}");
                    result.Append($"{main}" +
                        $"{new string(' ', SpacesForToString - main.Length)}");
                }
                result.Append('\n');
            }

            return result.ToString();
        }

        public Matrix Transpose()
        {
            double[,] newArray = new double[Columns, Rows];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    newArray[j, i] = _array[i, j];
                }
            }

            return new Matrix(newArray, Name);
        }

        public (double, Matrix) Inverse()
        {
            if (Rows != Columns)
                throw new ArgumentException("The matrix must be square");

            var result = new Matrix(this, Name);

            result = result.Minor();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if ((i + j) % 2 != 0)
                        result[i, j] = -result[i, j];
                }
            }

            result = result.Transpose();

            return (1d / Determinant(), result);
        }

        public Matrix Minor()
        {
            if (Rows != Columns)
                throw new ArgumentException("The matrix must be square");

            switch (Rows)
            {
                case 1:
                    return new Matrix(this, Name);
                case 2:
                    return new Matrix(new double[,]
                    {
                        { _array[1, 1], _array[1, 0] },
                        { _array[0, 1], _array[0, 0] }
                    }, Name);
                default:
                    var result = new Matrix(Columns, Columns, Name);

                    for (int i = 0; i < Rows; i++)
                    {
                        for (int j = 0; j < Columns; j++)
                        {
                            var matrix = new Matrix(Columns - 1, Columns - 1);
                            int column = 0, row = 0;

                            for (int a = 0; a < Rows; a++)
                            {
                                for (int b = 0; b < Columns; b++)
                                {
                                    if (a != i && b != j)
                                    {
                                        matrix[row, column] = _array[a, b];
                                        column++;
                                    }
                                }

                                if (column != 0)
                                    row++;

                                column = 0;
                            }

                            result[i, j] = matrix.Determinant();
                        }
                    }

                    return result;
            }
        }

        public Matrix PowN(uint n)
        {
            if (Rows != Columns)
                throw new ArgumentException("The matrix must be square");

            if (n == 0)
                return new Matrix(Rows, Columns, (_, _) => 1, Name);

            var result = new Matrix(this, Name);

            for (int i = 1; i < n; i++)
            {
                result *= this;
            }

            return result;
        }

        public double Determinant()
        {
            if (Rows != Columns)
                throw new ArgumentException("The matrix must be square");

            switch (Rows)
            {
                case 1:
                    return _array[0, 0];
                case 2:
                    return _array[0, 0] * _array[1, 1] - _array[0, 1] * _array[1, 0];
                case 3:
                    return _array[0, 0] * _array[1, 1] * _array[2, 2] +
                           _array[0, 1] * _array[1, 2] * _array[2, 0] +
                           _array[0, 2] * _array[1, 0] * _array[2, 1] -
                           _array[0, 2] * _array[1, 1] * _array[2, 0] -
                           _array[0, 0] * _array[1, 2] * _array[2, 1] -
                           _array[0, 1] * _array[1, 0] * _array[2, 2];
                default:
                    double result = 0;

                    for (int i = 0; i < Columns; i++)
                    {
                        double value = _array[0, i];
                        var matrix = new Matrix(Columns - 1, Columns - 1);
                        int column = 0;

                        for (int a = 1; a < Rows; a++)
                        {
                            for (int b = 0; b < Columns; b++)
                            {
                                if (b != i)
                                {
                                    matrix[a - 1, column] = _array[a, b];
                                    column++;
                                }
                            }
                            column = 0;
                        }

                        result += (i % 2 == 0 ? 1 : (-1)) * value * matrix.Determinant();
                    }

                    return result;             
            }
        }

        public static Matrix MMR(Matrix a, Matrix b)
        {
            if (a.Rows != a.Columns || b.Rows > 1 || a.Columns != b.Columns)
                throw new ArgumentException("Error");

            var (n, result) = a.Inverse();

            return b * (n * result);
        }

        public static Matrix Gauss(Matrix a)
        {
            double temp = 0;
            int inc = 0;

            int n = -1;

            while (++n < a.Rows)
            {
                temp = a[n, n];

                Console.WriteLine($"({n + 1}) / {temp}");

                for (int i = inc; i < a.Columns; i++)
                {
                    a[n, i] /= temp;
                }

                Console.WriteLine(a);

                for (int i = 0; i < a.Rows; i++)
                {
                    temp = a[i, n];

                    Console.WriteLine($"({i + 1}) - ({n + 1}) * {temp}");

                    for (int j = inc; j < a.Columns; j++)
                    {
                        if (n == i)
                            continue;

                        a[i, j] -= a[n, j] * temp;
                    }

                    Console.WriteLine(a);
                }

                inc++;
            }

            var result = new double[a.Rows, 1];

            for (int i = 0; i < a.Rows; i++)
            {
                result[i, 0] = a[i, a.Columns - 1];
            }

            return new Matrix(result);
            #region 2
            //Console.WriteLine("1/x");
            //temp = a[0, 0];

            //// 1/x
            //for (int i = 0; i < a.Columns; i++)
            //{
            //    a[0, i] /= temp;
            //}

            //Console.WriteLine(a);
            //Console.WriteLine("2-1*x");


            //temp = a[1, 0];

            //// 2-1*x
            //for (int i = 0; i < a.Columns; i++)
            //{
            //    a[1, i] -= a[0, i] * temp;
            //}

            //Console.WriteLine(a);
            //Console.WriteLine("3-1*x");


            //temp = a[2, 0];

            //// 3-1*x
            //for (int i = 0; i < a.Columns; i++)
            //{
            //    a[2, i] -= a[0, i] * temp;
            //}

            //Console.WriteLine(a);
            //Console.WriteLine("2/x");


            //temp = a[1, 1];

            //// 2/x
            //for (int i = 1; i < a.Columns; i++)
            //{
            //    a[1, i] /= temp;
            //}

            //Console.WriteLine(a);
            //Console.WriteLine("1-2*x");


            //temp = a[0, 1];

            //// 1-2*x
            //for (int i = 1; i < a.Columns; i++)
            //{
            //    a[0, i] -= a[1, i] * temp;
            //}

            //Console.WriteLine(a);
            //Console.WriteLine("3-2*x");


            //temp = a[2, 1];

            //// 3-2*x
            //for (int i = 1; i < a.Columns; i++)
            //{
            //    a[2, i] -= a[1, i] * temp;
            //}

            //Console.WriteLine(a);
            //Console.WriteLine("3/x");


            //temp = a[2, 2];

            //// 3/x
            //for (int i = 2; i < a.Columns; i++)
            //{
            //    a[2, i] /= temp;
            //}

            //Console.WriteLine(a);
            //Console.WriteLine("1-3*x");


            //temp = a[0, 2];

            //// 1-3*x

            //for (int i = 2; i < a.Columns; i++)
            //{
            //    a[0, i] -= a[2, i] * temp;
            //}

            //Console.WriteLine(a);
            //Console.WriteLine("2-3*x");


            //temp = a[1, 2];

            //// 2-3*x

            //for (int i = 2; i < a.Columns; i++)
            //{
            //    a[1, i] -= a[2, i] * temp;
            //}

            //Console.WriteLine(a);

            //return new Matrix(new double[,] { { a[0, 3] }, { a[1, 3] }, { a[2, 3] } });
            #endregion
        }
    }
}
