namespace MatrixF

open System
open System.Text

type public Matrix(array: int[,]) =

    new (rows: int, columns: int) = 
        Matrix( Array2D.zeroCreate rows columns )

    new (rows: int, columns: int, func: int -> int -> int) = 
        Matrix( Array2D.init rows columns func )

    member _.Item
        with get (row, column) = array[row, column]
        and set (row, column) value = array[row, column] <- value

    member _.Rows = array.GetLength 0

    member _.Columns = array.GetLength 0

    member _.Array = 
        array

    member _.Length = array.Length

    static member (~-) (matrix: Matrix) =
        let result = Matrix(matrix.Array)

        for i = 0 to matrix.Rows - 1 do
            for j = 0 to matrix.Columns - 1 do
                result[i, j] <- -result[i, j]

        result

    static member (+) (m1: Matrix, m2: Matrix) =
        if m1.Columns <> m2.Columns || m1.Rows <> m2.Rows then
            raise (ArgumentException("Matrices sizes must be equals"))

        let result = Matrix(m1.Rows, m1.Rows)

        for i = 0 to m1.Rows - 1 do
            for j = 0 to m1.Columns - 1 do
                result[1, 1] <- m1[i, j] + m2[i, j]

        result
        
    static member (*) (m1: Matrix, m2: Matrix) =
        if m1.Columns <> m2.Rows then
            raise (ArgumentException("Error"))

        let result = Matrix(m1.Rows, m2.Columns)

        let rec loop i row  =
            if row < m2.Columns then
                let mutable memory = 0
                for j = 0 to m1.Columns - 1 do
                    memory <- memory + m1[i, j] * m2[j, row]
                result[i, row] <- memory
                loop 0 (row + 1)
                

        for i = 0 to m1.Rows - 1 do
            loop i 0

        result

    static member CreateEmpty() =
        Matrix(0, 0)

    member this.Transpose() =
        let newArray = Array2D.zeroCreate this.Columns this.Rows

        for i = 0 to this.Rows - 1 do
            for j = 0 to this.Columns - 1 do
                newArray[j, i] <- array[i, j]
                
        newArray

    (*
    public static Matrix operator * (Matrix matrix_1, Matrix matrix_2)
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
    }*)
