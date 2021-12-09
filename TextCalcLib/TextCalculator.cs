using System;
using System.Collections.Generic;
using System.IO;

namespace TextCalcLib {

    public class TextCalculator {

        public Boolean ParseLine(String psLine, StringWriter psOut) {
            Boolean lbContinue = _ApplyOperation(psLine);

            if (psOut != null)
                Console.SetOut(psOut);

            if (!lbContinue)
                lbContinue = _ApplyCommand(psLine);

            return lbContinue;
        }

        #region IMPLEMENTATION

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

        private Double _nMem;
        private readonly Queue<Double> _oPartials = new Queue<Double>();

        private Boolean _ApplyOperation(String psLine) {
            foreach (var lsToken in _oOperatorTokens)
                if (psLine.StartsWith(lsToken))
                    return _ApplyOperation(new Operation(lsToken, _ParseValue(psLine, lsToken.Length)));

            return false;
        }

        private Boolean _ApplyCommand(String psCommand) {
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

        private Double _ParseValue(String psLine, Int32 pnTokenLength) {
            try {
                return Convert.ToDouble(psLine.Substring(pnTokenLength));
            } catch {
                throw new Exception("ERROR: Incorrect syntax");
            }
        }

        private Boolean _ApplyOperation(Operation poOperation) {
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

        #endregion IMPLEMENTATION

    }

}
