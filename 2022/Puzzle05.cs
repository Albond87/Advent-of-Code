public static class Puzzle05
{
    public static string Part1(string input)
    {
        string[] parts = input.Split("\n\n");
        string[] stacksRaw = parts[0].Split("\n");
        string[] moves = parts[1].Split("\n");
        stacksRaw = stacksRaw.Take(stacksRaw.Length-1).ToArray();
        List<Stack<string>> stacks = new List<Stack<string>>();
        for (int i=0; i<(stacksRaw[0].Length+1)/4; i++) {
            stacks.Add(new Stack<string>());
        }
        foreach (string s in stacksRaw.Reverse()) {
            for (int i=1; i<s.Length; i+=4) {
                string item = s.Substring(i,1);
                if (item!=" ") stacks[i/4].Push(s.Substring(i,1));
            }
        }
        foreach (string m in moves) {
            string[] words = m.Split(" ");
            int n = int.Parse(words[1]);
            int from = int.Parse(words[3])-1;
            int to = int.Parse(words[5])-1;
            for (int i=0; i<n; i++)
                stacks[to].Push(stacks[from].Pop());
        }
        string message = "";
        foreach (var s in stacks) {
            message += s.Pop();
        }
        return message;
    }

    public static string Part2(string input)
    {
        string[] parts = input.Split("\n\n");
        string[] stacksRaw = parts[0].Split("\n");
        string[] moves = parts[1].Split("\n");
        stacksRaw = stacksRaw.Take(stacksRaw.Length-1).ToArray();
        List<Stack<string>> stacks = new List<Stack<string>>();
        for (int i=0; i<(stacksRaw[0].Length+1)/4; i++) {
            stacks.Add(new Stack<string>());
        }
        foreach (string s in stacksRaw.Reverse()) {
            for (int i=1; i<s.Length; i+=4) {
                string item = s.Substring(i,1);
                if (item!=" ") stacks[i/4].Push(s.Substring(i,1));
            }
        }
        foreach (string m in moves) {
            string[] words = m.Split(" ");
            int n = int.Parse(words[1]);
            int from = int.Parse(words[3])-1;
            int to = int.Parse(words[5])-1;
            List<string> items = new List<string>();
            for (int i=0; i<n; i++)
                items.Add(stacks[from].Pop());
            items.Reverse();
            foreach (string i in items) {
                stacks[to].Push(i);
            }
        }
        string message = "";
        foreach (var s in stacks) {
            message += s.Pop();
        }
        return message;
    }
}