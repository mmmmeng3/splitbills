using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using splitbill;
using System.Collections.Generic;

namespace splitbill.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void cal_costTest()
        {
            Program p = new Program();
            List<decimal> cost = new List<decimal>();
            List<decimal> actual = new List<decimal>();
            List<decimal> expect= new List<decimal>();

            cost.Add(decimal.Parse("30.00")); 
            cost.Add(decimal.Parse("36.02")); 
            cost.Add(decimal.Parse("18.00"));
            expect.Add(decimal.Parse("-1.99"));
            expect.Add(decimal.Parse("-8.01"));
            expect.Add(decimal.Parse("10.01"));

            actual = p.cal_cost(cost);

            CollectionAssert.AreEqual(actual, expect);
        }
    }
}

