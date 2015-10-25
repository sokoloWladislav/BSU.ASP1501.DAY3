using System;
using System.Diagnostics;
using Polynom;
using NUnit.Framework;

namespace Polynom.UnitTests
{
    [TestFixture]
    public class PolynomTests
    {
        [Test]
        public void Equals_2PolynomsWithSameCoefs_ReturnedTrue()
        {
            double[] array = {0, 1, 2, 3};
            double[] brray = {0, 1, 2, 3};
            Polynom p = new Polynom(array);
            Polynom q = new Polynom(brray);
            Assert.IsTrue(p.Equals(q));
        }

        [Test]
        public void Test_OperatorPlus_TwoOperands()
        {
            Polynom p = new Polynom(0, 1, 2, 3, 4);
            Polynom q = new Polynom(0, 1, 2, 3, 4);
            Polynom r = new Polynom(0, 2, 4, 6, 8);
            Assert.IsTrue(p + q == r);
        }

        [Test]
        public void Test_OperatorMines_TwoOperands()
        {
            Polynom p = new Polynom(0, 1, 2, 3, 4);
            Polynom q = new Polynom(0, 1, 2, 3, 4);
            Polynom r = new Polynom(0);
            Assert.IsTrue(p - q == r);
        }

        [Test]
        public void Test_OperatorMultiply_TwoOperands()
        {
            Polynom p = new Polynom(0, 1, 2, 3);
            Polynom q = new Polynom(0, 1);
            Polynom r = new Polynom(0, 0, 1, 2, 3);
            Assert.IsTrue(p * q == r);
        }
    }
}