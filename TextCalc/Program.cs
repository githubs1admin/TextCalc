using System;
using System.Collections.Generic;

namespace TextCalc {

    class Program {

        private static String[] _oOperatorTokens = new String[] {
            "> ADD",
            "> SUBTRACT",
            "> MULTIPLY BY",
            "> DIVIDE BY"
        };

        struct Operation {

            public readonly String Token;
            public readonly Double Value;

            public Operation(String psToken, Double pnValue) {
                Token = psToken;
                Value = pnValue;
            }

        }

        private static Double _nMem;
        private static readonly Queue<Double> _oPartials = new Queue<Double>();

        static void Main(String[] _) {
            Boolean lbContinue = true;
            while (lbContinue) {
                Console.Write("> ");
                var lsLine = $"> {Console.ReadLine()}";
                try {
                    if (!_ApplyOperation(lsLine))
                        lbContinue = _ApplyCommand(lsLine);
                } catch (Exception loException) {
                    Console.WriteLine(loException.Message);
                }
            }
        }

        private static Boolean _ApplyOperation(String psLine) {
            foreach (var lsToken in _oOperatorTokens)
                if (psLine.StartsWith(lsToken))
                    return _ApplyOperation(new Operation(lsToken, _ParseValue(psLine, lsToken.Length)));

            return false;
        }

        private static Double _ParseValue(String psLine, Int32 pnTokenLength) {
            try {
                return Convert.ToDouble(psLine.Substring(pnTokenLength));
            } catch {
                throw new Exception("ERROR: Incorrect syntax");
            }
        }

        private static Boolean _ApplyOperation(Operation poOperation) {
            try { 
                switch (poOperation.Token) {
                    case "> ADD":
                        _nMem += poOperation.Value;
                        break;
                    case "> SUBTRACT":
                        _nMem -= poOperation.Value;
                        break;
                    case "> MULTIPLY BY":
                        _nMem *= poOperation.Value;
                        break;
                    case "> DIVIDE BY":
                        _nMem /= poOperation.Value;
                        break;
                }

                _oPartials.Enqueue(_nMem);

                return true;
            } catch {
                throw new Exception("ERROR: while applying operation");
            }
        }

        private static Boolean _ApplyCommand(String psCommand) {
            switch (psCommand) {
                case "> DISPLAY":
                    if (_oPartials.Count == 0)
                        Console.WriteLine($"< {_nMem}");
                    else 
                        while (_oPartials.Count > 0) {
                            _nMem = _oPartials.Dequeue();
                            Console.WriteLine($"< {_nMem}");
                        }
                    return true;
                case "> CLEAR":
                    _nMem = 0;
                    _oPartials.Clear();
                    return true;
                case "> EXIT":
                    return false;
                default:
                    Console.WriteLine("Not a valid line");
                    return true;

            }
        }

    }

}
