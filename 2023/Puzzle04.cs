public class Puzzle04 : Puzzle
{
    readonly IEnumerable<string[]> numbers;
    readonly IEnumerable<HashSet<int>> winning;
    readonly IEnumerable<HashSet<int>> owned;

    public Puzzle04() : base("04") { 
        numbers = inputs.Select(i => i.Split(": ")[1].Split(" | "));
        winning = numbers.Select(i => new HashSet<int>(i[0].Split(" ").Where(n => n != "").Select(n => int.Parse(n.Trim()))));
        owned = numbers.Select(i => new HashSet<int>(i[1].Split(" ").Where(n => n != "").Select(n => int.Parse(n.Trim()))));
    }

    public override void Part1()
    {
        double sum = 0;
        for (int i=0; i<inputs.Length; i++) {
            int wins = winning.ElementAt(i).Intersect(owned.ElementAt(i)).Count();
            if (wins > 0) {
                sum += Math.Pow(2,wins-1);
            }
        }
        Console.WriteLine(sum);
    }

    public override void Part2()
    {
        int[] cardCounts = Enumerable.Repeat(1,inputs.Length).ToArray();
        for (int i=0; i<inputs.Length; i++) {
            int wins = winning.ElementAt(i).Intersect(owned.ElementAt(i)).Count();
            for (int j=0; j<wins; j++) {
                cardCounts[i+j+1] += cardCounts[i];
            }
        }
        Console.WriteLine(cardCounts.Sum());
    }
}