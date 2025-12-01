public class Puzzle01 : Puzzle
{
    public Puzzle01() : base("01") { }

    public override void Part1()
    {
        int dialPos = 50;
        int zeroCount = 0;
        foreach (string move in inputs)
        {
            dialPos = (((dialPos + int.Parse(move[1..]) * (move[0] == 'L' ? -1 : 1)) % 100) + 100) % 100;
            if (dialPos == 0) zeroCount++;
        }
        Console.WriteLine(zeroCount);
    }

    public override void Part2()
    {
        int dialPos = 50;
        int zeroCount = 0;
        foreach (string move in inputs)
        {
            int newDialPos = dialPos + int.Parse(move[1..]) * (move[0] == 'L' ? -1 : 1);
            if (newDialPos >= 100)
            {
                zeroCount += newDialPos / 100;
                dialPos = newDialPos % 100;
            }
            else if (newDialPos <= 0)
            {
                zeroCount += (newDialPos / -100) + 1;
                if (dialPos == 0) zeroCount--;
                dialPos = ((newDialPos % 100) + 100) % 100;
            }
            else
            {
                dialPos = newDialPos;
            }
        }
        Console.WriteLine(zeroCount);
    }
}