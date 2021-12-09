using System;
using TextCalcLib;

namespace TextCalc {

    class Program {

        static void Main(String[] _) {
            var loCalc = new TextCalculator();
            Boolean lbContinue = true;
            while (lbContinue) {
                Console.Write("> ");
                var lsLine = $"> {Console.ReadLine()}";
                try {
                    lbContinue = loCalc.ParseLine(lsLine, null);
                } catch (Exception loException) {
                    Console.WriteLine(loException.Message);
                }
            }
        }

    }

}
