public class Puzzle22 : Puzzle
{
    public Puzzle22() : base("22") { }

    public override void Part1()
    {
        long sum = 0;
        foreach (var input in inputs) {
            long number = long.Parse(input);
            for (int i = 0; i < 2000; i++) {
                number = (number ^ (number*64)) % 16777216;
                number = (number ^ (int)(number/32)) % 16777216;
                number = (number ^ (number*2048)) % 16777216;
            }
            sum += number;
        }
        Console.WriteLine(sum);
    }

    public override void Part2()
    {
        Dictionary<(int,int,int,int),int> sequencetotals = [];
        foreach (var input in inputs) {
            long number = long.Parse(input);
            int prevprice = 0;
            int delta, delta1 = 0, delta2 = 0, delta3 = 0;
            HashSet<(int,int,int,int)> seensequences = [];
            for (int i = 0; i < 1999; i++) {
                number = (number ^ (number*64)) % 16777216;
                number = (number ^ (int)(number/32)) % 16777216;
                number = (number ^ (number*2048)) % 16777216;
                int price = (int)number%10;
                delta = price-prevprice;
                if (i > 2 && !seensequences.Contains((delta3,delta2,delta1,delta))) {
                    int total = sequencetotals.GetValueOrDefault((delta3,delta2,delta1,delta), 0);
                    sequencetotals[(delta3,delta2,delta1,delta)] = total+price;
                    seensequences.Add((delta3,delta2,delta1,delta));
                }
                delta3 = delta2;
                delta2 = delta1;
                delta1 = delta;
                prevprice = price;
            }
        }
        Console.WriteLine(sequencetotals.Values.Max());
    }
}