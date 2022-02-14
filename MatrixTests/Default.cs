//using MatrixLab;
using MatrixF;
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
                { 19, 22},
                { 43, 50}
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

            matrix.Transpose();

            Assert.Equal<int[,]>(new int[,]
            {
                { 0, 1, 1 },
                { 1, 2, 2 },
                { 9, 3, 3 },
            }, matrix);
        }
    }
}