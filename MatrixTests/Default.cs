using MatrixLab;
using System;
using Xunit;

namespace MatrixTests
{
    public class Default
    {
        [Fact]
        public void GenerateFunc()
        {
            var matrix = new Matrix(2, 2, (i, j) => i + j);
            Assert.Equal<int[,]>(new int[,]
            {
                { 0, 1 },
                { 1, 2 }
            }, matrix);
        }

        [Fact]
        public void SumSquare()
        {
            var matrix_1 = new Matrix(new int[,]
            {
                { 0, 1 },
                { 1, 2 }
            });

            var matrix_2 = new Matrix(new int[,]
            {
                { 0, 1 },
                { 1, 2 }
            });

            Assert.Equal<int[,]>(new int[,]
            {
                { 0, 2 },
                { 2, 4 }
            }, matrix_1 + matrix_2);
        }

        [Fact]
        public void SumThrow()
        {
            var matrix_1 = new Matrix(new int[,]
            {
                { 0, 1, 9 },
                { 1, 2, 0 }
            });

            var matrix_2 = new Matrix(new int[,]
            {
                { 0, 1 },
                { 1, 2 }
            });

            Assert.Throws<ArgumentException>(() => matrix_1 + matrix_2);
        }


        [Fact]
        public void MultiplyFirstWide()
        {
            var matrix_1 = new Matrix(new int[,]
            {
                { 1, 2, 1 },
                { 0, 1, 2 }
            });

            var matrix_2 = new Matrix(new int[,]
            {
                { 1, 0 },
                { 0, 1 },
                { 1, 1 }
            });

            Assert.Equal<int[,]>(new int[,]
            {
                { 2, 3 },
                { 2, 3 }
            }, matrix_1 * matrix_2);
        }


        [Fact]
        public void MultiplyFirstTall()
        {
            var matrix_1 = new Matrix(new int[,]
            {
                { 1, 0 },
                { 0, 1 },
                { 1, 1 }
            });

            var matrix_2 = new Matrix(new int[,]
            {
                { 1, 2, 1 },
                { 0, 1, 2 }
            });

            Assert.Equal<int[,]>(new int[,]
            {
                { 1, 2, 1 },
                { 0, 1, 2 },
                { 1, 3, 3 }
            }, matrix_1 * matrix_2);
        }


        [Fact]
        public void MultiplySquare()
        {
            var matrix_1 = new Matrix(new int[,]
            {
                { 1, 2 },
                { 3, 4 },
            });

            var matrix_2 = new Matrix(new int[,]
            {
                { 5, 6 },
                { 7, 8 }
            });

            Assert.Equal<int[,]>(new int[,]
            {
                { 19, 22 },
                { 43, 50 }
            }, matrix_1 * matrix_2);
        }

        [Fact]
        public void MultiplyThrow()
        {
            var matrix_1 = new Matrix(new int[,]
            {
                { 0, 1, 9 },
                { 1, 2, 0 }
            });

            var matrix_2 = new Matrix(new int[,]
            {
                { 0, 1 },
                { 1, 2 }
            });

            Assert.Throws<ArgumentException>(() => matrix_1 * matrix_2);
        }


        [Fact]
        public void MinusOut()
        {
            var matrix = new Matrix(new int[,]
            {
                { 0, 1 },
                { 1, -2 }
            });

            var result = -matrix;

            Assert.Equal<int[,]>(new int[,]
            {
                { 0, -1 },
                { -1, 2, }
            }, result);
        }



        [Fact]
        public void MultiplyNumber()
        {
            var matrix = new Matrix(new int[,]
            {
                { 0, 1 },
                { 1, -2 }
            });

            var result = 3 * matrix;

            Assert.Equal<int[,]>(new int[,]
            {
                { 0, 3 },
                { 3, -6, }
            }, result);
        }

        [Fact]
        public void Transpose()
        {
            var matrix = new Matrix(new int[,]
            {
                { 0, 1, 9 },
                { 1, 2, 3 },
                { 1, 2, 3 },
            });

            matrix = matrix.Transpose();

            Assert.Equal<int[,]>(new int[,]
            {
                { 0, 1, 1 },
                { 1, 2, 2 },
                { 9, 3, 3 },
            }, matrix);
        }

        [Fact]
        public void DeterminantRank1()
        {
            var matrix = new Matrix(new int[,]
            {
                { 1 },
            });

            var result = matrix.Determinant();

            Assert.Equal(1, result);
        }

        [Fact]
        public void DeterminantRank2()
        {
            var matrix = new Matrix(new int[,]
            {
                { 1, 3 },
                { 4, 2 }
            });

            var result = matrix.Determinant();

            Assert.Equal(-10, result);
        }

        [Fact]
        public void DeterminantRank3()
        {
            var matrix = new Matrix(new int[,]
            {
                { 1, 3, 9 },
                { 4, 2, 0 },
                { 7, 7, 7 }
            });

            var result = matrix.Determinant();

            Assert.Equal(56, result);
        }

        [Fact]
        public void DeterminantRank4()
        {
            var matrix = new Matrix(new int[,]
            {
                { 3, -7, 1, 4 },
                { 6, 2, -1, 8 },
                { -9, 5, 7, -3 },
                { -2, -6, -5, 0 }
            });

            var result = matrix.Determinant();

            Assert.Equal(3632, result);
        }

        [Fact]
        public void DeterminantRank5()
        {
            var matrix = new Matrix(new int[,]
            {
                { 2, 3, 7, 10, 13 },
                { 1, 2, 3, 4, 5 },
                { 3, 5, 11, 16, 21 },
                { 2, -7, 7, 7, 2 },
                { 1, 4, 5, 3, 10 }
            });

            var result = matrix.Determinant();

            Assert.Equal(-52, result);
        }

        [Fact]
        public void DeterminantThrow()
        {
            var matrix = new Matrix(new int[,]
            {
                { 2, 3, 7, 10, 13 },
                { 1, 2, 3, 4, 5 },
                { 3, 5, 11, 16, 21 }
            });

            Assert.Throws<ArgumentException>(() => matrix.Determinant());
        }

        [Fact]
        public void InverseRank2()
        {
            var matrix = new Matrix(new int[,]
            {
                { 1, 2 },
                { 3, 4 }
            });

            var result = matrix.Inverse();

            Assert.Equal(-0.5, result.Item1);
            Assert.Equal<int[,]>(new int[,] { { 4, -2 }, { -3, 1 } }, result.Item2);
        }

        [Fact]
        public void InverseRank3()
        {
            var matrix = new Matrix(new int[,]
            {
                { 2, 5, 7 },
                { 6, 3, 4 },
                { 5, -2, -3 }
            });

            var result = matrix.Inverse();

            Assert.Equal(-1, result.Item1);
            Assert.Equal<int[,]>(new int[,] { { -1, 1, -1 }, { 38, -41, 34 }, { -27, 29, -24} }, result.Item2);
        }

        [Fact]
        public void InverseThrow()
        {
            var matrix = new Matrix(new int[,]
            {
                { 2, 3, 7, 10, 13 },
                { 1, 2, 3, 4, 5 },
                { 3, 5, 11, 16, 21 }
            });

            Assert.Throws<ArgumentException>(() => matrix.Inverse());
        }

        [Fact]
        public void MinorRank2()
        {
            var matrix = new Matrix(new int[,]
            {
                { 1, 2 },
                { 3, 4 }
            });

            var result = matrix.Minor();

            Assert.Equal<int[,]>(new int[,] { { 4, 3 }, { 2, 1 } }, result);
        }

        [Fact]
        public void MinorRank3()
        {
            var matrix = new Matrix(new int[,]
            {
                { 2, 5, 7 },
                { 6, 3, 4 },
                { 5, -2, -3 }
            });

            var result = matrix.Minor();

            Assert.Equal<int[,]>(new int[,] { { -1, -38, -27 }, { -1, -41, -29 }, { -1, -34, -24} }, result);
        }

        [Fact]
        public void MinorThrow()
        {
            var matrix = new Matrix(new int[,]
            {
                { 2, 3, 7, 10, 13 },
                { 1, 2, 3, 4, 5 },
                { 3, 5, 11, 16, 21 }
            });

            Assert.Throws<ArgumentException>(() => matrix.Minor());
        }
    }
}