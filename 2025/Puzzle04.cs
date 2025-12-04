public class Puzzle04 : Puzzle
{
    readonly HashSet<(int, int)> paper = [];

    public Puzzle04() : base("04")
    {
        for (int y=0; y<inputs.Length; y++)
        {
            for (int x=0; x<inputs[y].Length; x++)
            {
                if (inputs[y][x] == '@') paper.Add((x,y));
            }
        }
    }

    List<(int,int)> GetAccessiblePaper()
    {
        (int,int)[] directions = [(-1,-1), (0,-1), (1,-1), (-1,0), (1,0), (-1,1), (0,1), (1,1)];
        List<(int,int)> accessible = [];
        foreach (var roll in paper)
        {
            int count = 0;
            foreach (var d in directions)
            {
                if (paper.Contains((roll.Item1+d.Item1, roll.Item2+d.Item2))) 
                {
                    count++;
                    if (count==4) break;
                }
            }
            if (count < 4) accessible.Add(roll);
        }
        return accessible;
    }

    public override void Part1()
    {
        Console.WriteLine(GetAccessiblePaper().Count);
    }

    public override void Part2()
    {
        int removed = 0;
        List<(int,int)> toRemove;

        do
        {
            toRemove = GetAccessiblePaper();
            foreach (var roll in toRemove) paper.Remove(roll);
            removed += toRemove.Count;
        } while (toRemove.Count>0);

        Console.WriteLine(removed);
    }
}