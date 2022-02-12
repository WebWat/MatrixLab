using System.Text;

namespace MatrixLab
{
    public sealed class Matrix : IDisposable
    {
        public int Rows
        {
            get { return _array.GetLength(0); }
        }

        public int Columns
        {
            get { return _array.GetLength(1); }
        }

        private int[,] _array;
        public int[,] Array
        {
            get { return _array; }
        }

        public int Length
        {
            get { return _array.Length; }
        }

        public Matrix(int[,] array)
        {
            _array = array;
        }

        public Matrix(int row, int column)
        {
            _array = new int[row, column];
        }

        public Matrix(int row, int column, Func<int, int, int> func)
        {
            _array = new int[row, column];

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
            var result = new Matrix(matrix.Rows, matrix.Rows);

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = -matrix[i, j];
                }
            }

            return result;
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

        public static Matrix operator *(Matrix matrix_1, Matrix matrix_2)
        {
            if (matrix_1.Columns != matrix_2.Rows)
                throw new ArgumentException("Error");

            var result = new Matrix(matrix_1.Rows, matrix_2.Columns);

            for (int i = 0; i < matrix_1.Rows; i++)
            {
                int row = 0;

                while (row < matrix_2.Columns)
                {
                    int memory = 0;

                    for (int j = 0; j < matrix_1.Columns; j++)
                    {
                        memory += matrix_1[i, j] * matrix_2[j, row];
                    }

                    result[i, row] = memory;

                    row++;
                }
            }

            return result;
        }

        public static Matrix operator *(int x, Matrix matrix)
        {
            var result = new Matrix(matrix.Rows, matrix.Rows);

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

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (j == 0)
                    {
                        result.Append("| ");
                    }
                    else if (j + 1 == Columns)
                    {
                        result.Append($"{this[i, j],-1} |");

                        break;
                    }

                    result.Append($"{this[i, j],-10}");
                }

                result.Append('\n');
            }

            return result.ToString();
        }

        public void Transpose()
        {
            int[,] newArray = new int[Columns, Rows];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    newArray[j, i] = _array[i, j];
                }
            }

            _array = newArray;
        }

        public void Dispose()
        {
            _array = new int[0, 0];

            GC.Collect();
        }
    }
}
