public class Puzzle01 : Puzzle
{
    public Puzzle01() : base("01") { }

    public override void Part1()
    {
        string digits = "0123456789";
        int sum = inputs.Select(i=>int.Parse(i.First(c=>digits.Contains(c)).ToString()+i.Last(c=>digits.Contains(c)))).Sum();
        Console.WriteLine(sum);
    }

    public override void Part2()
    {
        // My original solution

        // string[] digits = ["0","1","2","3","4","5","6","7","8","9","one","two","three","four","five","six","seven","eight","nine"];
        // int sum = 0;
        // foreach (string input in inputs) {
        //     int indexOfFirst = input.Length;
        //     int indexOfLast = -1;
        //     int first = 0;
        //     int last = 0;
        //     for (int d = 0; d<19; d++) {
        //         int index = input.IndexOf(digits[d]);
        //         if (index > -1 && index < indexOfFirst) {
        //             indexOfFirst = index;
        //             first = d > 9 ? d - 9 : d;
        //         }
        //         index = input.LastIndexOf(digits[d]);
        //         if (index > indexOfLast) {
        //             indexOfLast = index;
        //             last = d > 9 ? d - 9 : d;
        //         }
        //     }
        //     sum += int.Parse(first.ToString()+last.ToString());
        // }
        // Console.WriteLine(sum);
        

        // Second solution after realising I could use Replace

        string digits = "0123456789";
        int sum = inputs.Select(i=>i.Replace("one","o1e").Replace("two","t2o").Replace("three","t3e").Replace("four","4").Replace("five","5e").Replace("six","6").Replace("seven","7n").Replace("eight","e8t").Replace("nine","n9e")).Select(i=>int.Parse(i.First(c=>digits.Contains(c)).ToString()+i.Last(c=>digits.Contains(c)))).Sum();
        Console.WriteLine(sum);
    }
}