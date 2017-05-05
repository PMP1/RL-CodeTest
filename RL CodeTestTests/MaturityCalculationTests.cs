using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RL_CodeTest;
using System.Collections.Generic;

namespace RL_CodeTestTests
{
    [TestClass]
    public class MaturityCalculationTests
    {
        [TestMethod]
        public void TestPolicyA_Bonus()
        {
            var data = new List<PolicyData>()
            {
                new PolicyData
                {
                    PolicyNumber = "A1",
                    PolicyStartDate = new DateTime(1980, 1,1),
                    Premiums = 10000,
                    Membership = true,
                    DiscretionaryBonus = 1000,
                    UpliftPercentage = 40,
                    PolicyType = "A"
                }
            };

            var calc = new MaturityCalculation();

            calc.CalculateMaturity(data);

            Assert.AreEqual(14980, data[0].MaturityValue);

        }

        [TestMethod]
        public void TestPolicyA_No_Bonus()
        {
            var data = new List<PolicyData>()
            {
                new PolicyData
                {
                    PolicyNumber = "A1",
                    PolicyStartDate = new DateTime(1990, 1,1),
                    Premiums = 10000,
                    Membership = true,
                    DiscretionaryBonus = 1000,
                    UpliftPercentage = 40,
                    PolicyType = "A"
                }
            };

            var calc = new MaturityCalculation();

            calc.CalculateMaturity(data);

            Assert.AreEqual(13580, data[0].MaturityValue);

        }

        [TestMethod]
        public void TestPolicyB_Bonus()
        {
            var data = new List<PolicyData>()
            {
                new PolicyData
                {
                    PolicyNumber = "B1",
                    PolicyStartDate = new DateTime(1990, 1,1),
                    Premiums = 10000,
                    Membership = true,
                    DiscretionaryBonus = 1000,
                    UpliftPercentage = 40,
                    PolicyType = "B"
                }
            };

            var calc = new MaturityCalculation();

            calc.CalculateMaturity(data);

            Assert.AreEqual(14700, data[0].MaturityValue);
        }

        [TestMethod]
        public void TestPolicyB_No_Bonus()
        {
            var data = new List<PolicyData>()
            {
                new PolicyData
                {
                    PolicyNumber = "B1",
                    PolicyStartDate = new DateTime(1990, 1,1),
                    Premiums = 10000,
                    Membership = false,
                    DiscretionaryBonus = 1000,
                    UpliftPercentage = 40,
                    PolicyType = "B"
                }
            };

            var calc = new MaturityCalculation();

            calc.CalculateMaturity(data);

            Assert.AreEqual(13300, data[0].MaturityValue);
        }

        [TestMethod]
        public void TestPolicyC_Bonus()
        {
            var data = new List<PolicyData>()
            {
                new PolicyData
                {
                    PolicyNumber = "B1",
                    PolicyStartDate = new DateTime(1990, 1,1),
                    Premiums = 10000,
                    Membership = true,
                    DiscretionaryBonus = 1000,
                    UpliftPercentage = 40,
                    PolicyType = "C"
                }
            };

            var calc = new MaturityCalculation();

            calc.CalculateMaturity(data);

            Assert.AreEqual(14420, data[0].MaturityValue);
        }

        [TestMethod]
        public void TestPolicyC_No_Bonus()
        {
            var data = new List<PolicyData>()
            {
                new PolicyData
                {
                    PolicyNumber = "B1",
                    PolicyStartDate = new DateTime(1990, 1,1),
                    Premiums = 10000,
                    Membership = false,
                    DiscretionaryBonus = 1000,
                    UpliftPercentage = 40,
                    PolicyType = "C"
                }
            };

            var calc = new MaturityCalculation();

            calc.CalculateMaturity(data);

            Assert.AreEqual(13020, data[0].MaturityValue);
        }
    }
}
