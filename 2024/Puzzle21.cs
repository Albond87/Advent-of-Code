public class Puzzle21 : Puzzle
{
    readonly Dictionary<char, int[]> keypads = new()
    {
        {'0',[1,1]},
        {'1',[0,2]},
        {'2',[1,2]},
        {'3',[2,2]},
        {'4',[0,3]},
        {'5',[1,3]},
        {'6',[2,3]},
        {'7',[0,4]},
        {'8',[1,4]},
        {'9',[2,4]},
        {'A',[2,1]},
        {'^',[1,1]},
        {'<',[0,0]},
        {'v',[1,0]},
        {'>',[2,0]},
    };
    Dictionary<(char,char),string> directioncache = [];
    Dictionary<(string,int),long> expandcache = [];

    public Puzzle21() : base("21") { }

    string ShortestPath(char key1, char key2) {
        if (directioncache.TryGetValue((key1, key2), out string? result))
        {
            return result;
        }
        int[] pos1 = keypads[key1];
        int[] pos2 = keypads[key2];
        int xdiff = pos2[0] - pos1[0];
        int ydiff = pos2[1] - pos1[1];
        string horizontal = "";
        string vertical = "";
        if (xdiff < 0) horizontal += new string('<', xdiff*-1);
        else if (xdiff > 0) horizontal += new string('>', xdiff);
        if (ydiff < 0) vertical += new string('v', ydiff*-1);
        else if (ydiff > 0) vertical += new string('^', ydiff);

        if (pos1[1] == 1 && pos1[0] + xdiff == 0) {
            // If you can't move horizontally first
            result = vertical + horizontal;
        }
        else if (pos1[0] == 0 && pos1[1] + ydiff == 1) {
            // If you can't move vertically first
            result = horizontal + vertical;
        }
        else if (xdiff < 0) {
            // Left movement before up/down is faster
            result = horizontal + vertical;
        }
        else {
            // Down movement before right is faster (order doesn't matter for up and right)
            result = vertical + horizontal;
        }
        directioncache.Add((key1,key2),result);
        return result;
    }

    // Get the length of the sequence of button presses at the given level
    long GetButtonPressLength(string sequence, int level) {
        if (level == 0) {
            return sequence.Length;
        }
        if (expandcache.TryGetValue((sequence, level), out long result)) {
            return result;
        }
        long length = 0;
        char prevkey = 'A';
        foreach (char key in sequence) {
            length += GetButtonPressLength(ShortestPath(prevkey, key) + 'A', level-1);
            prevkey = key;
        }
        expandcache.Add((sequence, level), length);
        return length;
    }

    public override void Part1()
    {
        long sum = 0;
        foreach (string code in inputs) {
            sum += GetButtonPressLength(code, 3) * int.Parse(code[..(code.Length-1)]);
        }
        Console.WriteLine(sum);
    }

    public override void Part2()
    {
        long sum = 0;
        foreach (string code in inputs) {
            sum += GetButtonPressLength(code, 26) * int.Parse(code[..(code.Length-1)]);
        }
        Console.WriteLine(sum);
    }
}