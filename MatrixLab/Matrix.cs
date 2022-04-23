using System.Text;

namespace MatrixLab
{
    public sealed class Matrix
    {
        private const int SpacesForToString = 7;

        public int Rows
        {
            get { return _array.GetLength(0); }
        }

        public int Columns
        {
            get { return _array.GetLength(1); }
        }

        public string Name { get; set; } = "NONE";

        private int[,] _array;
        public int[,] Array
        {
            get { return _array; }
        }

        public int Length
        {
            get { return _array.Length; }
        }

        public Matrix(int[,] array, string name = default)
        {
            _array = array;
            Name = name == default ? Name : name;
        }

        public Matrix(int row, int column, string name = default)
        {
            _array = new int[row, column];
            Name = name == default ? Name : name;
        }

        public Matrix(int row, int column, Func<int, int, int> func, string name = default)
        {
            _array = new int[row, column];
            Name = name == default ? Name : name;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    this[i, j] = func(i, j);
                }
            }
        }

        public int this[int row, int column]
        {
            get { return _array[row, column]; }
            set { _array[row, column] = value; }
        }


        public static implicit operator int[,](Matrix matrix)
        {
            return matrix.Array;
        }

        public static implicit operator Matrix(int[,] array)
        {
            return new Matrix(array);
        }

        public static Matrix operator -(Matrix matrix)
        {
            var result = new Matrix(matrix.Rows, matrix.Rows, string.Concat("-", matrix.Name));

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

            var result = new Matrix(matrix_1.Rows, matrix_1.Rows, 
                string.Concat("(", matrix_1.Name, " + ", matrix_2.Name, ")"));

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

            var result = new Matrix(matrix_1.Rows, matrix_2.Columns,
                string.Concat("(", matrix_1.Name, " * ", matrix_2.Name, ")"));

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

        public static Matrix operator * (int x, Matrix matrix)
        {
            var result = new Matrix(matrix.Rows, matrix.Rows, 
                string.Concat(x, " * ", matrix.Name));


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
                    string main = $"{this[i, j]}";
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
            int[,] newArray = new int[Columns, Rows];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    newArray[j, i] = _array[i, j];
                }
            }

            return new Matrix(newArray, string.Concat(Name, "^T"));
        }

        // TODO: finish
        public (double, Matrix) Inverse()
        {
            if (Rows != Columns)
                throw new ArgumentException("The matrix must be square");

            var result = new Matrix(this, string.Concat(Name, "^(-1)"));

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
                    return new Matrix(this, string.Concat(Name, "^M"));
                case 2:
                    return new Matrix(new int[,]
                    {
                        { _array[1, 1], _array[1, 0] },
                        { _array[0, 1], _array[0, 0] }
                    }, string.Concat(Name, "^M"));
                default:
                    var result = new Matrix(Columns, Columns, string.Concat(Name, "^M"));

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

        public int Determinant()
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
                    int result = 0;

                    for (int i = 0; i < Columns; i++)
                    {
                        int value = _array[0, i];
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
    }
}
