using BenchmarkDotNet.Attributes;
using MatrixLab;

namespace Bench;

[MemoryDiagnoser]
[ThreadingDiagnoser]
public class Benchh
{
    [Benchmark]
    public double Determinant()
    {
        var a = new Matrix(new double[,] { { 1.2, -2.23, 12.3, 3.4, -2.3, 33.03, 0.2 }, { 1.2, -2.23, 12.3, 3.4, -2.3, 33.03, 0.2 }, { 1.2, -2.23, 12.3, 3.4, -2.3, 33.03, 0.2 }, { 1.2, -2.23, 333, 222.4, -2.3, 333.03, 0.2 }, { 1.2, -2.23, 12.3, 0, -2.3, 33.03, 0.2 }, { 1.2, -2.23, 12.3, 3.4, -2.3, -33.03, 0.2 }, { 1.2, -2.23, 12.3, 3.4, -2.3, -33.03, 0.2 }, }, "A");

        return a.Determinant();
    }
}
