public class Puzzle24 : Puzzle
{
    Dictionary<string,bool> wires = [];
    Dictionary<(string,string,string),string> gateoutputs = [];
    Dictionary<string,(string,string,string)> gateinputs = [];
    List<string> swaps = [];

    public Puzzle24() : base("24") {
        wires = inputs.TakeWhile(x => x != "").ToDictionary(x => x[..3], x => x[5] == '1');
    }

    long BitsToNumber(string wire) {
        int bit = 0;
        long columnval = 1;
        long num = 0;
        while (true) {
            if (wires.TryGetValue(wire + ("0" + bit.ToString())[^2..], out bool val)) {
                if (val) {
                    num += columnval;
                }
                bit++;
                columnval *= 2;
            }
            else {
                return num;
            }
        }
    }

    public override void Part1()
    {
        List<string[]> gates = inputs.SkipWhile(x => x != "").Skip(1).Select(x => x.Split([" ", "->"], StringSplitOptions.RemoveEmptyEntries).ToArray()).ToList();
        while (gates.Count > 0) {
            for (int i = 0; i < gates.Count; i++) {
                var gate = gates[i];
                if (!wires.TryGetValue(gate[0], out bool input1)) continue;
                if (!wires.TryGetValue(gate[2], out bool input2)) continue;
                if (gate[1] == "AND") {
                    wires[gate[3]] = input1 && input2;
                }
                else if (gate[1] == "OR") {
                    wires[gate[3]] = input1 || input2;
                }
                else if (gate[1] == "XOR") {
                    wires[gate[3]] = input1 ^ input2;
                }
                gates.RemoveAt(i);
            }
        }
        long output = BitsToNumber("z");
        // long x = BitsToNumber("x");
        // long y = BitsToNumber("y");
        // Console.WriteLine(x);
        // Console.WriteLine(y);
        Console.WriteLine(output);
        // Console.WriteLine(x+y);
        // Console.WriteLine((x+y)-output);
    }

    void SwapOutputs(string output1, string output2) {
        var inputs1 = gateinputs[output1];
        var inputs2 = gateinputs[output2];
        gateoutputs[inputs1] = output2;
        gateinputs[output2] = inputs1;
        gateoutputs[inputs2] = output1;
        gateinputs[output1] = inputs2;
        swaps.Add(output1);
        swaps.Add(output2);
    }

    string CheckGate(string input1, string operation, string input2, string? expectedoutput = null) {
        if (!gateoutputs.TryGetValue((input1, operation, input2), out var output) && !gateoutputs.TryGetValue((input2, operation, input1), out output)) {
            if (expectedoutput != null) {
                Console.WriteLine("Expected gate " + input1 + " " + operation + " " + input2 + " not present in input.");
                Environment.Exit(1);
                // return null;
            }
            return "";
        }
        if (expectedoutput != null) {
            // Expected output will always be zXX
            if (output == expectedoutput) { 
                return output;
            }
            // Find the actual inputs for the expected output and swap the outputs
            SwapOutputs(output, expectedoutput);
            // var actualinputs = gateinputs[expectedoutput];
            // gateoutputs[(input1, operation, input2)] = expectedoutput;
            // gateinputs[expectedoutput] = (input1, operation, input2);
            // gateoutputs[actualinputs] = output;
            // gateinputs[output] = actualinputs;
            // swaps.Add(expectedoutput);
            // swaps.Add(output);
            return expectedoutput;
        }
        else {
            return output;
        }
    }

    public override void Part2()
    {
        // Find the pairs of gates which have swapped outputs
        // This code makes a lot of assumptions about the input - it should be a chain of gates which make z = x + y
        // If the gates aren't (mostly) structured in the expected way, this won't work

        var gatesraw = inputs.SkipWhile(x => x != "").Skip(1).Select(x => x.Split([" ", "->"], StringSplitOptions.RemoveEmptyEntries).ToArray());
        gateoutputs = gatesraw.ToDictionary(g => (g[0],g[1],g[2]), g => g[3]);
        gateinputs = gatesraw.ToDictionary(g => g[3], g => (g[0],g[1],g[2]));
        int outputbits = gateinputs.Where(g => g.Key.StartsWith('z')).Select(g => int.Parse(g.Key[1..])).Max();

        // It is expected that there will be no swaps for the first 2 output bits, z00 and z01
        // Expected: z00 = x00 XOR y00 (in either order)
        CheckGate("x00", "XOR", "y00");
        string[] previnputs = ["x00", "y00"];
        // Expected: a01 = x00 AND y00
        //           a02 = x01 XOR y01
        //           z01 = a01 XOR a02
        //           (a01 and a02 can be called anything not starting with z but are named aXX here for clarity)
        previnputs[0] = CheckGate("x00", "AND", "y00");
        previnputs[1] = CheckGate("x01", "XOR", "y01");
        string bitoutput = CheckGate(previnputs[0], "XOR", previnputs[1], "z01");
        // Each of the output bits from z02 onwards except the final one should follow the same structure:
        // Given z(n-1) = aaa XOR bbb:
        //         z(n) = ((aaa AND bbb) OR (x(n-1) AND y(n-1))) XOR (x(n) XOR y(n))
        // The following code covers several possible ways in which an output bit doesn't follow this structure,
        // and attempts to find the appropriate swaps to rectify it
        // Note: not all cases are covered
        for (int i = 2; i < outputbits-1; i++) {
            string bitnumber = ("0" + i.ToString())[^2..];
            string prevbitnumber = ("0" + (i-1).ToString())[^2..];
            string inner1 = CheckGate(previnputs[0], "AND", previnputs[1]);
            string inner2 = CheckGate("x" + prevbitnumber, "AND", "y" + prevbitnumber);
            string outer1;
            string outer2 = CheckGate("x" + bitnumber, "XOR", "y" + bitnumber);
            if (inner1.StartsWith('z') || inner2.StartsWith('z')) {
                var gate = gateoutputs.First(g => g.Key.Item2 == "XOR" && (g.Key.Item1 == outer2 || g.Key.Item3 == outer2));
                outer1 = gate.Key.Item1 == outer2 ? gate.Key.Item3 : gate.Key.Item1;
                if (outer1.StartsWith('z')) {
                    Console.WriteLine("Input malformed more than expected, unsure of swaps");
                    Environment.Exit(1);
                }
                bitoutput = gate.Value;
                if (bitoutput == "z" + bitnumber) {
                    Console.WriteLine("Input malformed more than expected, unsure of swaps");
                    Environment.Exit(1);
                }
                else {
                    SwapOutputs(bitoutput, "z" + bitnumber);
                }
            }
            else {
                outer1 = CheckGate(inner1, "OR", inner2);
                if (outer1.StartsWith('z') || outer2.StartsWith('z')) {
                    KeyValuePair<(string, string, string),string> gate;
                    if (outer1.StartsWith('z')) {
                        gate = gateoutputs.First(g => g.Key.Item2 == "XOR" && (g.Key.Item1 == outer2 || g.Key.Item3 == outer2));
                    }
                    else {
                        gate = gateoutputs.First(g => g.Key.Item2 == "XOR" && (g.Key.Item1 == outer1 || g.Key.Item3 == outer1));
                    }
                    bitoutput = gate.Value;
                    if (bitoutput == "z" + bitnumber) {
                        Console.WriteLine("Input malformed more than expected, unsure of swaps");
                        Environment.Exit(1);
                    }
                    else {
                        SwapOutputs(bitoutput, "z" + bitnumber);
                        if (outer1.StartsWith('z')) {
                            outer1 = bitoutput;
                        }
                        else {
                            outer2 = bitoutput;
                        }
                    }
                }
                else if (outer1 == "") {
                    outer1 = CheckGate(inner1, "OR", outer2);
                    if (outer1 != "") {
                        SwapOutputs(inner2, outer2);
                        outer2 = inner2;
                    }
                    else {
                        outer1 = CheckGate(inner2, "OR", outer2);
                        if (outer1 != "") {
                            SwapOutputs(inner1, outer2);
                            outer2 = inner1;
                        }
                        else {
                            Console.WriteLine("Input malformed more than expected, unsure of swaps");
                            Environment.Exit(1);
                        }
                    }
                }
                else {
                    bitoutput = CheckGate(outer1, "XOR", outer2);
                    if (bitoutput == "") {
                        var bitinputs = gateinputs["z" + bitnumber];
                        if (bitinputs.Item1 != outer1 && bitinputs.Item3 != outer1) {
                            if (bitinputs.Item1 == outer2) {
                                SwapOutputs(bitinputs.Item3, outer1);
                                outer1 = bitinputs.Item3;
                            }
                            else {
                                SwapOutputs(bitinputs.Item1, outer1);
                                outer1 = bitinputs.Item1;
                            }
                        }
                        else {
                            if (bitinputs.Item1 == outer1) {
                                SwapOutputs(bitinputs.Item3, outer2);
                                outer2 = bitinputs.Item3;
                            }
                            else {
                                SwapOutputs(bitinputs.Item1, outer2);
                                outer2 = bitinputs.Item1;
                            }
                        }
                    }
                    else if (!bitoutput.StartsWith('z')) {
                        SwapOutputs(bitoutput, "z" + bitnumber);
                    }
                }
            }
            previnputs = [outer1, outer2];
        }
        Console.WriteLine(string.Join(",",swaps.Order()));
    }
}