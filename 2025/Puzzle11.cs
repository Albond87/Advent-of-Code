public class Puzzle11 : Puzzle
{
    readonly Dictionary<string, string[]> devices;
    readonly Dictionary<string, int> cache;
    readonly Dictionary<(string, bool, bool), long> cacheDACFFT;

    public Puzzle11() : base("11")
    {
        devices = [];
        foreach (var i in inputs)
        {
            devices[i[..3]] = i[5..].Split(' ').ToArray();
        }
        cache = [];
        cacheDACFFT = [];
    }

    int CountPaths(string device)
    {
        if (device.Equals("out")) return 1;
        if (cache.TryGetValue(device, out int count))
        {
            return count;
        }
        foreach (string output in devices[device])
        {
            count += CountPaths(output);
        }
        cache[device] = count;
        return count;
    }

    long CountPathsWithDACFFT(string device, bool dac=false, bool fft=false)
    {
        if (device.Equals("out")) return dac&&fft ? 1 : 0;
        if (device.Equals("dac")) dac=true;
        else if (device.Equals("fft")) fft=true;
        if (cacheDACFFT.TryGetValue((device, dac, fft), out long count))
        {
            return count;
        }
        foreach (string output in devices[device])
        {
            count += CountPathsWithDACFFT(output, dac, fft);
        }
        cacheDACFFT[(device, dac, fft)] = count;
        return count;
    }

    public override void Part1()
    {
        Console.WriteLine(CountPaths("you"));
    }

    public override void Part2()
    {
        Console.WriteLine(CountPathsWithDACFFT("svr"));
    }
}