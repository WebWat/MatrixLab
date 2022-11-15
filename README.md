## About this project
In my free time I decided to create a simple library for working with matrices, which greatly helped in solving all kinds of equations.

The main project here will be the console application `MatrixLab` (which also contains the Matrix class with all the functions), and the other projects are tests and benchmarks.

### Note
The `MaftrixF` project is the same as `MatrixLab`, only in F#. **It is currently still under development**.

## Usage

``` csharp
var a = new Matrix(new double[,] { 
  { 1, 1, 1, 0 }, 
  { 0, 1, 0, 4 }, 
  { 1, 0, 6, 0 }, 
  { 0, 2, 0, 3 } 
});

var result = a.Transpose().PowN(2);

Console.WriteLine(result);

Console.WriteLine(result.Determinant());
```

### Output
![Output](https://github.com/WebWat/MatrixLab/blob/master/Images/output.png)
