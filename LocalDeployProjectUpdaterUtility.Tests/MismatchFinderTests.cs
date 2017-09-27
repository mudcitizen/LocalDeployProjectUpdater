using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalDeployProjectUpdaterUtility.Tests
{
    [TestClass]
    public class MismatchFinderTests
    {
        IList<String> _Set1;
        IMismatchFinder _MismatchFinder;

        [TestInitialize]
        public void Setup()
        {
            _Set1 = new List<String>() { "one", "two", "three" };
            _MismatchFinder = new MismatchFinder();
        }

        [TestMethod]
        public void TestSameSet()
        {
            Assert.IsTrue(_MismatchFinder.GetMismatches(_Set1, _Set1).Count() == 0);
        }

        [TestMethod]
        public void TestEqualSets()
        {
            IList<String> set2 = _Set1.ToList();
            Assert.IsTrue(_MismatchFinder.GetMismatches(_Set1, set2).Count() == 0);
        }

        [TestMethod]
        public void TestDifferentCase()
        {
            IList<String> set2 = _Set1.Select(s => s.ToUpper()).ToList();
            Assert.IsTrue(_MismatchFinder.GetMismatches(_Set1, set2).Count() == 0);
        }

        [TestMethod]
        public void TestExtraItemInSet2()
        {
            const String additionalItem = "four";
            IList<String> set2 = _Set1.Select(s => s.ToUpper()).ToList();
            set2.Add(additionalItem);
            IEnumerable<String> actual = _MismatchFinder.GetMismatches(set2, _Set1);

            Assert.AreEqual(actual.Count(), 1);
            Assert.IsTrue(actual.Contains(additionalItem));

        }
    }
}
