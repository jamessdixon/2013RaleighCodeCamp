using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tff.ClaimAdjudicator;

namespace Tff.ClaimAdjudicator.Tests
{
    [TestClass]
    public class ClaimCalculatorTests
    {
        [TestMethod]
        public void AdjustClaimReducesCorrectly()
        {
            ClaimCalculator calcualtor = new ClaimCalculator();
            Claim claim = new Claim(1, DateTime.Now, 10.0);

            double expected = 5.0F;
            double actual = calcualtor.AdjustClaim(claim);
            Assert.AreEqual(expected, actual);
        }
    }
}
