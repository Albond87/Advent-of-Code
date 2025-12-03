public class Puzzle03 : Puzzle
{
    readonly List<int>[] banks;

    public Puzzle03() : base("03")
    {
        banks = inputs.Select(i => i.ToList().Select(c=>(int)char.GetNumericValue(c)).ToList()).ToArray();
    }

    static long GetLargestJoltage(List<int> bank, int numDigits)
    {
        long joltage = 0;
        int startIndex = 0;
        for (int d=numDigits-1; d>=0; d--)
        {
            joltage *= 10;
            int digit = bank[startIndex..(bank.Count-d)].Max();
            joltage += digit;
            startIndex = bank.IndexOf(digit, startIndex) + 1;
        }
        return joltage;
    }

    public override void Part1()
    {
        long sum = banks.Select(b => GetLargestJoltage(b, 2)).Sum();
        Console.WriteLine(sum);
    }

    public override void Part2()
    {
        long sum = banks.Select(b => GetLargestJoltage(b, 12)).Sum();
        Console.WriteLine(sum);
    }
}