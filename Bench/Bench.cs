using BenchmarkDotNet.Attributes;
using MatrixLab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bench;

[MemoryDiagnoser]
[ThreadingDiagnoser]
public class Benchh
{
    void Test()
    {
        for (int i = 0; i < 100; i++)
        {
            Thread thread = new Thread(new ThreadStart(() => Console.WriteLine(i)));
            thread.Start();
        }
    }
    //[Benchmark]
    //public double Determinant()
    //{
    //    var a = new Matrix(new double[,] { { 1.2, -2.23, 12.3, 3.4, -2.3, 33.03, 0.2 }, { 1.2, -2.23, 12.3, 3.4, -2.3, 33.03, 0.2 }, { 1.2, -2.23, 12.3, 3.4, -2.3, 33.03, 0.2 }, { 1.2, -2.23, 333, 222.4, -2.3, 333.03, 0.2 }, { 1.2, -2.23, 12.3, 0, -2.3, 33.03, 0.2 }, { 1.2, -2.23, 12.3, 3.4, -2.3, -33.03, 0.2 }, { 1.2, -2.23, 12.3, 3.4, -2.3, -33.03, 0.2 }, },"A");

    //    return a.Determinant();
    //}
    [Benchmark]
    public void Determinant()
    {

        Test();
    }

    //[Benchmark]
    //public double DeterminantNew()
    //{
    //    var a = new Matrix(new double[,] { { 1.2, -2.23, 12.3, 3.4, -2.3, 33.03, 0.2 }, { 1.2, -2.23, 12.3, 3.4, -2.3, 33.03, 0.2 }, { 1.2, -2.23, 12.3, 3.4, -2.3, 33.03, 0.2 }, { 1.2, -2.23, 333, 222.4, -2.3, 333.03, 0.2 }, { 1.2, -2.23, 12.3, 0, -2.3, 33.03, 0.2 }, { 1.2, -2.23, 12.3, 3.4, -2.3, -33.03, 0.2 }, { 1.2, -2.23, 12.3, 3.4, -2.3, -33.03, 0.2 }, }, "A");

    //    return a.DeterminantNew();
    //}
}
