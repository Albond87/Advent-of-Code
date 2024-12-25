public class Puzzle24 : Puzzle
{
    Dictionary<string,bool> wires = [];

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
        Console.WriteLine(output);
    }

    public override void Part2()
    {
        Console.WriteLine("Solved by hand");
    }
}