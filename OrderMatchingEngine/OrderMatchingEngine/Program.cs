using System;

namespace OrderMatchingEngine {

    public class Program {

        private static void Main(string[] args) {
            Start(RunExample1());
            Start(RunExample2());
            Start(RunExample3());
            Start(RunExample4());
            Start(RunExample5());
            Start(RunExample6());
            Start(RunExample7());
            Start(RunExample8());
            Start(RunExample9());
            Start(RunExample10());
            Start(RunExample11());
            Start(RunExample12());
            Start(RunExample13());
            Start(RunExample14());
        }

        public static void Start(string[] args) {
            var engine = new MatchingEngine();
            foreach (string arg in args) {
                string[] row = arg.Split(' ');
                switch (row[0]) {
                    case MatchingEngine.ORDERSIDE_BUY:
                    case MatchingEngine.ORDERSIDE_SELL:
                        engine.Add(row[4], row[0], row[1], int.Parse(row[2]), int.Parse(row[3])); break;
                    case MatchingEngine.OPERATION_MODIFY:
                        engine.Modify(row[1], row[2], int.Parse(row[3]), int.Parse(row[4])); break;
                    case MatchingEngine.OPERATION_CANCEL:
                        engine.Cancel(row[1]); break;
                    case MatchingEngine.OPERATION_PRINT:
                        engine.Print(); break;
                    default:
                        Console.WriteLine("Testing..."); break;
                }
            }
            Console.WriteLine(Environment.NewLine + "*****************************" + Environment.NewLine);
        }

        public static string[] RunExample1() {
            Console.WriteLine("Run Commands for Example 1:");
            string command0 = "BUY GFD 1000 10 order1";
            string command1 = "PRINT";
            string[] commands = new string[] { command0, command1 };
            foreach (var command in commands) {
                Console.WriteLine(command);
            }
            Console.WriteLine(Environment.NewLine + "Expected:");
            Console.WriteLine("SELL:");
            Console.WriteLine("BUY:");
            Console.WriteLine("1000 10");
            Console.WriteLine(Environment.NewLine + "Actual:");
            return commands;
        }

        public static string[] RunExample2() {
            Console.WriteLine("Run Commands for Example 2:");
            string command0 = "BUY GFD 1000 10 order1";
            string command1 = "BUY GFD 1000 20 order2";
            string command2 = "PRINT";
            string[] commands = new string[] { command0, command1, command2 };
            foreach (var command in commands) {
                Console.WriteLine(command);
            }
            Console.WriteLine(Environment.NewLine + "Expected:");
            Console.WriteLine("SELL:");
            Console.WriteLine("BUY:");
            Console.WriteLine("1000 30");
            Console.WriteLine(Environment.NewLine + "Actual:");
            return commands;
        }

        public static string[] RunExample3() {
            Console.WriteLine("Run Commands for Example 3:");
            string command0 = "BUY GFD 1000 10 order1";
            string command1 = "BUY GFD 1001 20 order2";
            string command2 = "PRINT";
            string[] commands = new string[] { command0, command1, command2 };
            foreach (var command in commands) {
                Console.WriteLine(command);
            }
            Console.WriteLine(Environment.NewLine + "Expected:");
            Console.WriteLine("SELL:");
            Console.WriteLine("BUY:");
            Console.WriteLine("1001 20");
            Console.WriteLine("1000 10");
            Console.WriteLine(Environment.NewLine + "Actual:");
            return commands;
        }

        public static string[] RunExample4() {
            Console.WriteLine("Run Commands for Example 4:");
            string command0 = "BUY GFD 1000 10 order1";
            string command1 = "SELL GFD 900 20 order2";
            string command2 = "PRINT";
            string[] commands = new string[] { command0, command1, command2 };
            foreach (var command in commands) {
                Console.WriteLine(command);
            }
            Console.WriteLine(Environment.NewLine + "Expected:");
            Console.WriteLine("TRADE order1 1000 10 order2 900 10");
            Console.WriteLine("SELL:");
            Console.WriteLine("900 10");
            Console.WriteLine("BUY:");
            Console.WriteLine(Environment.NewLine + "Actual:");
            return commands;
        }

        public static string[] RunExample5() {
            Console.WriteLine("Run Commands for Example 5:");
            string command0 = "BUY GFD 1000 10 order1";
            string command1 = "BUY GFD 1000 10 order2";
            string command2 = "SELL GFD 1000 15 ORDER3";
            string[] commands = new string[] { command0, command1, command2 };
            foreach (var command in commands) {
                Console.WriteLine(command);
            }
            Console.WriteLine(Environment.NewLine + "Expected:");
            Console.WriteLine("TRADE ORDER2 1010 10 ORDER3 1000 10");
            Console.WriteLine("TRADE ORDER1 1000 5 ORDER3 1000 5");
            Console.WriteLine(Environment.NewLine + "Actual:");
            return commands;
        }

        public static string[] RunExample6() {
            Console.WriteLine("Run Commands for Example 6:");
            string command0 = "BUY GFD 1000 10 order1";
            string command1 = "BUY GFD 1000 10 order2";
            string command2 = "MODIFY order1 BUY 1000 20";
            string command3 = "SELL GFD 900 20 order3";
            string[] commands = new string[] { command0, command1, command2, command3 };
            foreach (var command in commands) {
                Console.WriteLine(command);
            }
            Console.WriteLine(Environment.NewLine + "Expected:");
            Console.WriteLine("TRADE order2 1000 10 order3 900 10");
            Console.WriteLine("TRADE order1 1000 10 order3 900 10");
            Console.WriteLine(Environment.NewLine + "Actual:");
            return commands;
        }

        public static string[] RunExample7() {
            Console.WriteLine("Run Commands for Example 7:");
            string command0 = "SELL GFD 100 50 ORDER1";
            string command1 = "BUY GFD 120 70 ORDER2";
            string command2 = "PRINT";
            string[] commands = new string[] { command0, command1, command2 };
            foreach (var command in commands) {
                Console.WriteLine(command);
            }
            Console.WriteLine(Environment.NewLine + "Expected:");
            Console.WriteLine("TRADE ORDER1 100 50 ORDER2 120 50");
            Console.WriteLine("SELL:");
            Console.WriteLine("BUY:");
            Console.WriteLine("120 20");
            Console.WriteLine(Environment.NewLine + "Actual:");
            return commands;
        }

        public static string[] RunExample8() {
            Console.WriteLine("Run Commands for Example 8:");
            string command0 = "SELL GFD 100 90 ORDER1";
            string command1 = "BUY GFD 120 70 ORDER2";
            string command2 = "PRINT";
            string[] commands = new string[] { command0, command1, command2 };
            foreach (var command in commands) {
                Console.WriteLine(command);
            }
            Console.WriteLine(Environment.NewLine + "Expected:");
            Console.WriteLine("TRADE ORDER1 100 70 ORDER2 120 70");
            Console.WriteLine("SELL:");
            Console.WriteLine("100 20");
            Console.WriteLine("BUY:");
            Console.WriteLine(Environment.NewLine + "Actual:");
            return commands;
        }

        public static string[] RunExample9() {
            Console.WriteLine("Run Commands for Example 9:");
            string command0 = "BUY GFD 120 50 ORDER1";
            string command1 = "SELL GFD 100 70 ORDER2";
            string command2 = "PRINT";
            string[] commands = new string[] { command0, command1, command2 };
            foreach (var command in commands) {
                Console.WriteLine(command);
            }
            Console.WriteLine(Environment.NewLine + "Expected:");
            Console.WriteLine("TRADE ORDER1 120 50 ORDER2 100 50");
            Console.WriteLine("SELL:");
            Console.WriteLine("100 20");
            Console.WriteLine("BUY:");
            Console.WriteLine(Environment.NewLine + "Actual:");
            return commands;
        }

        public static string[] RunExample10() {
            Console.WriteLine("Run Commands for Example 10:");
            string command0 = "BUY GFD 120 90 ORDER1";
            string command1 = "SELL GFD 100 70 ORDER2";
            string command2 = "PRINT";
            string[] commands = new string[] { command0, command1, command2 };
            foreach (var command in commands) {
                Console.WriteLine(command);
            }
            Console.WriteLine(Environment.NewLine + "Expected:");
            Console.WriteLine("TRADE ORDER1 120 70 ORDER2 100 70");
            Console.WriteLine("SELL:");
            Console.WriteLine("BUY:");
            Console.WriteLine("120 20");
            Console.WriteLine(Environment.NewLine + "Actual:");
            return commands;
        }

        public static string[] RunExample11() {
            Console.WriteLine("Run Commands for Example 11:");
            string command0 = "BUY GFD 120 70 ORDER1";
            string command1 = "SELL IOC 100 90 ORDER2";
            string command2 = "PRINT";
            string[] commands = new string[] { command0, command1, command2 };
            foreach (var command in commands) {
                Console.WriteLine(command);
            }
            Console.WriteLine(Environment.NewLine + "Expected:");
            Console.WriteLine("TRADE ORDER1 120 70 ORDER2 100 70");
            Console.WriteLine("SELL:");
            Console.WriteLine("BUY:");
            Console.WriteLine(Environment.NewLine + "Actual:");
            return commands;
        }

        public static string[] RunExample12() {
            Console.WriteLine("Run Commands for Example 12:");
            string command0 = "BUY IOC 120 70 ORDER1";
            string command1 = "SELL GFD 100 90 ORDER2";
            string command2 = "PRINT";
            string[] commands = new string[] { command0, command1, command2 };
            foreach (var command in commands) {
                Console.WriteLine(command);
            }
            Console.WriteLine(Environment.NewLine + "Expected:");
            Console.WriteLine("SELL:");
            Console.WriteLine("100 90");
            Console.WriteLine("BUY:");
            Console.WriteLine(Environment.NewLine + "Actual:");
            return commands;
        }

        public static string[] RunExample13() {
            Console.WriteLine("Run Commands for Example 13:");
            string command0 = "SELL GFD 100 90 ORDER1";
            string command1 = "MODIFY ORDER1 BUY 80 40";
            string command2 = "PRINT";
            string[] commands = new string[] { command0, command1, command2 };
            foreach (var command in commands) {
                Console.WriteLine(command);
            }
            Console.WriteLine(Environment.NewLine + "Expected:");
            Console.WriteLine("SELL:");
            Console.WriteLine("BUY:");
            Console.WriteLine("80 40");
            Console.WriteLine(Environment.NewLine + "Actual:");
            return commands;
        }

        public static string[] RunExample14() {
            Console.WriteLine("Run Commands for Example 14:");
            string command0 = "SELL GFD 100 90 ORDER1";
            string command1 = "CANCEL ORDER1";
            string command2 = "PRINT";
            string[] commands = new string[] { command0, command1, command2 };
            foreach (var command in commands) {
                Console.WriteLine(command);
            }
            Console.WriteLine(Environment.NewLine + "Expected:");
            Console.WriteLine("SELL:");
            Console.WriteLine("BUY:");
            Console.WriteLine(Environment.NewLine + "Actual:");
            return commands;
        }
    }
}
