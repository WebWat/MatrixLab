using System.Text;

namespace MatrixLab
{
    public sealed class Matrix
    {
        private const int Spaces = 20;
        private const int SpacesForToString = 4;
        private const int SpacesForMultiply = 35;

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
            StringBuilder output = new();

            var result = new Matrix(matrix.Rows, matrix.Rows, string.Concat("-", matrix.Name));

            string operation = $"-{matrix.Name} = ";
            output.Append(operation);

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = -matrix[i, j];

                    string main = $"{matrix[i, j]} * (-1) = {result[i, j]};";
                    if (j == 0)
                        output.Append($"{new string(' ', i == 0 && j == 0 ? 0 : operation.Length)}");
                    output.Append($"{main}" +
                        $"{new string(' ', Spaces - main.Length)}");
                }
                output.Append("\n");
            }

            Console.WriteLine(output);

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

            StringBuilder output = new();

            string operation = $"{matrix_1.Name} + {matrix_2.Name} = ";
            output.Append(operation);

            var result = new Matrix(matrix_1.Rows, matrix_1.Rows, 
                string.Concat("(", matrix_1.Name, " + ", matrix_2.Name, ")"));

            for (int i = 0; i < matrix_1.Rows; i++)
            {
                for (int j = 0; j < matrix_1.Columns; j++)
                {
                    result[i, j] = matrix_1[i, j] + matrix_2[i, j];

                    string main = $"{matrix_1[i, j]} + {matrix_2[i, j]} = {result[i, j]};";
                    if (j == 0)
                        output.Append($"{new string(' ', i == 0 && j == 0 ? 0 : operation.Length)}");
                    output.Append($"{main}" +
                        $"{new string(' ', Spaces - main.Length)}");
                }
                output.Append("\n");
            }

            Console.WriteLine(output);

            return result;
        }

        public static Matrix operator * (Matrix matrix_1, Matrix matrix_2)
        {
            if (matrix_1.Columns != matrix_2.Rows)
                throw new ArgumentException("Error");

            var result = new Matrix(matrix_1.Rows, matrix_2.Columns,
                string.Concat("(", matrix_1.Name, " * ", matrix_2.Name, ")"));

            StringBuilder output = new();

            string operation = $"{matrix_1.Name} * {matrix_2.Name} = ";
            output.Append(operation);

            for (int i = 0; i < matrix_1.Rows; i++)
            {
                int row = 0;

                while (row < matrix_2.Columns)
                {
                    StringBuilder main = new();

                    for (int j = 0; j < matrix_1.Columns; j++)
                    {
                        result[i, row] += matrix_1[i, j] * matrix_2[j, row];
                        if (j == 0)
                            main.Append($"{matrix_1[i, j]} * {matrix_2[j, row]}");
                        else
                            main.Append($" + {matrix_1[i, j]} * {matrix_2[j, row]}");
                    }

                    main.Append($" = {result[i, row]};");

                    if (row == 0)
                        output.Append($"{new string(' ', i == 0 && row == 0 ? 0 : operation.Length)}");
                    output.Append($"{main}" +
                        $"{new string(' ', SpacesForMultiply - main.Length)}");

                    row++;
                }

                output.Append('\n');
            }

            Console.WriteLine(output);

            return result;
        }

        public static Matrix operator * (int x, Matrix matrix)
        {
            var result = new Matrix(matrix.Rows, matrix.Rows, 
                string.Concat(x, " * ", matrix.Name));

            StringBuilder output = new();

            string operation = $"{x} * {matrix.Name} = ";
            output.Append(operation);

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = x * matrix[i, j];

                    string main = $"{x} * {matrix[i, j]} = {result[i, j]};";
                    if (j == 0)
                        output.Append($"{new string(' ', i == 0 && j == 0 ? 0 : operation.Length)}");
                    output.Append($"{main}" +
                        $"{new string(' ', Spaces - main.Length)}");
                }
                output.Append('\n');
            }

            Console.WriteLine(output);

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

            StringBuilder output = new();

            string operation = $"{Name}^T = ";
            output.Append(operation);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    newArray[j, i] = _array[i, j];

                    string main = $"{_array[j, i]} = {_array[i, j]};";
                    if (j == 0)
                        output.Append($"{new string(' ', i == 0 && j == 0 ? 0 : operation.Length)}");
                    output.Append($"{main}" +
                        $"{new string(' ', Spaces - main.Length)}");
                }
                output.Append('\n');
            }
            Console.WriteLine(output);

            return new Matrix(newArray, string.Concat(Name, "^T"));
        }
    }
}
