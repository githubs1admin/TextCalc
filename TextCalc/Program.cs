using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCalc {

    class Program {

        enum Operators {
            Add,
            Multiply,
            Subtract,
            Divide
        }

        struct Operation {
            Operators Operator;
            Double Value;
        }

        private static Double _nMem;
        private static Queue<Double> _oPartials = new Queue<Double>();

        static void Main(String[] _) {
            Boolean lbContinue = true;

            while (lbContinue) {
                var lsLine = Console.ReadLine();
                if (lsLine.ToLower() == "exit")
                    lbContinue = false;
                else 
                    _Parse(lsLine);
            }
        }
       
        private static void _Parse(String psLine) {
            Double lnValue;
            //Operators lnOperator;
            if (psLine.ToLower().StartsWith("multiply by")) {
                //lnOperator = Operators.Multiply;
                lnValue = Convert.ToDouble(psLine.Substring(11));
                _nMem *= lnValue;
                _oPartials.Enqueue(_nMem);
            } else if (psLine.ToLower().StartsWith("subtract")) {
                //lnOperator = Operators.Subtract;
                lnValue = Convert.ToDouble(psLine.Substring(8));
                _nMem -= lnValue;
                _oPartials.Enqueue(_nMem);
            } else if (psLine.ToLower().StartsWith("add")) {
                //lnOperator = Operators.Add;
                lnValue = Convert.ToDouble(psLine.Substring(3));
                _nMem += lnValue;
                _oPartials.Enqueue(_nMem);
            } else if (psLine.ToLower().StartsWith("divide by")) {
                //lnOperator = Operators.Divide;
                lnValue = Convert.ToDouble(psLine.Substring(9));
                _nMem /= lnValue;
                _oPartials.Enqueue(_nMem);
            } else if (psLine.ToLower().StartsWith("display"))
                while (_oPartials.Count > 0) {
                    _nMem = _oPartials.Dequeue();
                    Console.WriteLine($"< {_nMem}");
                }
        }

    }

}
