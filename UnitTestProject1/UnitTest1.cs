using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using TextCalcLib;

namespace UnitTestProject1 {

    [TestClass]
    public class UnitTest1 {
    
        [TestMethod]
        public void TestMethod1() {
            var loCalc = new TextCalculator();
            StringWriter loWriter = new StringWriter();
            StringBuilder loExpected = new StringBuilder();

            loCalc.ParseLine("> ADD 5", loWriter);
            loCalc.ParseLine("> MULTIPLY BY 3", loWriter);
            loCalc.ParseLine("> SUBTRACT 3", loWriter);
            loCalc.ParseLine("> DISPLAY", loWriter);

            loExpected.AppendLine("< 5");
            loExpected.AppendLine("< 15");
            loExpected.AppendLine("< 12");
            Assert.AreEqual(loExpected.ToString(), loWriter.ToString());

            loCalc.ParseLine("> DIVIDE BY 6", loWriter);
            loCalc.ParseLine("> DISPLAY", loWriter);
            loExpected.AppendLine("< 2");
            Assert.AreEqual(loExpected.ToString(), loWriter.ToString());
        }

    }

}
