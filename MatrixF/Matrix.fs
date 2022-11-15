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

    member _.Array = array

    member _.Length = array.Length

    static member (~-) (matrix: Matrix) =
        Matrix(Array2D.map (fun x -> -x) matrix.Array)

    static member (+) (m1: Matrix, m2: Matrix) =
        if m1.Columns <> m2.Columns || m1.Rows <> m2.Rows then
            raise (ArgumentException("Matrices sizes must be equals"))

        let result = Matrix (m1.Rows, m1.Rows)

        for i = 0 to m1.Rows - 1 do
            for j = 0 to m1.Columns - 1 do
                result[i, j] <- m1[i, j] + m2[i, j]

        result
        
    static member (*) (m1: Matrix, m2: Matrix) =
        if m1.Columns <> m2.Rows then
            raise (ArgumentException("Error"))

        let result = Matrix(m1.Rows, m2.Columns)

        let rec loop i row  =
            if row < m2.Columns then
                for j = 0 to m1.Columns - 1 do
                    result[i, row] <- result[i, row] + m1[i, j] * m2[j, row]
                loop i (row + 1)
                
        for i = 0 to m1.Rows - 1 do
            loop i 0

        result

    static member (*) (num: int, matrix: Matrix) =
        Matrix (Array2D.map (fun x -> x * num) matrix.Array)

    static member CreateEmpty() =
        Matrix(0, 0)

    member this.Transpose() =
        let newArray = Array2D.zeroCreate this.Columns this.Rows

        for i = 0 to this.Rows - 1 do
            for j = 0 to this.Columns - 1 do
                newArray[j, i] <- array[i, j]
                
        Matrix newArray

//double result = 0;

//                    for (int i = 0; i < Columns; i++)
//                    {
//                        double value = _array[0, i];
//                        var matrix = new Matrix(Columns - 1, Columns - 1);
//                        int column = 0;

//                        for (int a = 1; a < Rows; a++)
//                        {
//                            for (int b = 0; b < Columns; b++)
//                            {
//                                if (b != i)
//                                {
//                                    matrix[a - 1, column] = _array[a, b];
//                                    column++;
//                                }
//                            }
//                            column = 0;
//                        }

//                        result += (i % 2 == 0 ? 1 : (-1)) * value * matrix.Determinant();
//                    }

//                    return result; 
    member this.Determiner() =
        if this.Columns <> this.Rows then
            raise (ArgumentException("Error"))
        let rec func x =
            match x with
                | (2, 2) -> array[0, 0] * array[1, 1] - array[0, 1] * array[1, 0]
                | (3, 3) -> array[0, 0] * array[1, 1] * array[2, 2] + 
                            array[2, 0] * array[0, 1] * array[1, 2] +
                            array[1, 0] * array[0, 2] * array[2, 1] - 
                            array[2, 0] * array[1, 1] * array[2, 0] - 
                            array[0, 1] * array[1, 0] * array[2, 2] - 
                            array[0, 0] * array[2, 1] * array[1, 2]
                | x -> 0
                
        ()

    override this.ToString() =
        let result = new StringBuilder()

        for i = 0 to this.Rows - 1 do
            for j = 0 to this.Columns - 1 do
                result.Append($"{array[i, j]}\t") |> ignore
            result.Append '\n' |> ignore

        result.ToString()
