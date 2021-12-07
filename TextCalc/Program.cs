using System;
using System.Collections.Generic;

namespace TextCalc {

    class Program {

        enum Operators {
            Add,
            Multiply,
            Subtract,
            Divide
        }

        struct Operation {

            public readonly Operators Operator;
            public readonly Double Value; 

            public Operation(Operators pnOperator, Double pnValue)  {
                Operator = pnOperator;
                Value = pnValue;
            }

        }

        private static Double _nMem;
        private static readonly Queue<Double> _oPartials = new Queue<Double>();

        static void Main(String[] _) {
            Boolean lbContinue = true;
            while (lbContinue) {
                var lsLine = Console.ReadLine();
                if (lsLine == "EXIT")
                    lbContinue = false;
                else
                    try {
                        _ParseLine(lsLine);
                    } catch (Exception loException) {
                        Console.WriteLine(loException.Message);
                    }
            }
        }

        private static void _ParseLine(String psLine) {
            if (psLine.StartsWith("MULTIPLY BY"))
                _ApplyOperation(new Operation(Operators.Multiply, _ParseValue(psLine, "MULTIPLY BY".Length)));
            else if (psLine.StartsWith("SUBTRACT"))
                _ApplyOperation(new Operation(Operators.Subtract, _ParseValue(psLine, 8)));
            else if (psLine.StartsWith("ADD"))
                _ApplyOperation(new Operation(Operators.Add, _ParseValue(psLine, 3)));
            else if (psLine.StartsWith("DIVIDE BY"))
                _ApplyOperation(new Operation(Operators.Divide, _ParseValue(psLine, 9)));
            else if (psLine.StartsWith("DISPLAY"))
                _ApplyCommand("DISPLAY");
        }

        private static Double _ParseValue(String psLine, Int32 pnTokenLength) {
            try {
                return Convert.ToDouble(psLine.Substring(pnTokenLength));
            } catch {
                throw new Exception("ERROR: while parsing value");
            }
        }

        private static void _ApplyOperation(Operation poOperation) {
            switch (poOperation.Operator) {
                case Operators.Add:
                    _nMem += poOperation.Value;
                    break;
                case Operators.Subtract:
                    _nMem -= poOperation.Value;
                    break;
                case Operators.Multiply:
                    _nMem *= poOperation.Value;
                    break;
                case Operators.Divide:
                    _nMem /= poOperation.Value;
                    break;
            }

            _oPartials.Enqueue(_nMem);
        }

        private static void _ApplyCommand(String psCommand) {
            switch (psCommand) {
                case "DISPLAY":
                    while (_oPartials.Count > 0) {
                        _nMem = _oPartials.Dequeue();
                        Console.WriteLine($"< {_nMem}");
                    }
                    break;
            }
        }

    }

}
